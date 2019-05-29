using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;
using System.IO;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;


namespace GadgeteerApp4
{

    //public class Korteles
    //{
    //    public string vardas { get; set; }
    //    public string kodas { get; set; }
    //    public void Korteles()
    //    { }
        //public string getvardas()
        //{
        //    return vardas;
        //}
        //public string getkodas()
        //{
        //    return kodas;
        //}
        
    //}

    public partial class Program
    {
     
        #region kintamuju aprasymas


        public static Hashtable rfidHash= new Hashtable();
        public Hashtable getDuom()
        {
            return rfidHash;
        }
        private GT.Timer denyTimer = new GT.Timer(2000);
        private GT.Timer unlockTimer = new GT.Timer(3000);

        private GT.Timer timer = new GT.Timer(1000);
      
        private Font font = Resources.GetFont(Resources.FontResources.consolas_72);
        private Font font2 = Resources.GetFont(Resources.FontResources.consolas_24);
        private Font font3 = Resources.GetFont(Resources.FontResources.Arial_18);
        private Window window;
        private Canvas canvas;
        private Text antraste;
        private Text antraste2;
        int i = 6;
        //Korteles[] duom = new Korteles[100];
        //string vardas = "test";
        
        //public Korteles getDuom(int i)
        // {
        //        return duom[i];
        // }

        
     
        #endregion
        
        void ProgramStarted()
        {
            Debug.Print("Program Started");
            
            SetupWindow();
            //registracija();
        }
       
        void SetupWindow()
        {
            #region Lango elementu konfiguravimas
            window = displayT43.WPFWindow;
            canvas = new Canvas();
            window.Child = canvas;
            antraste = new Text(font2, "Laikmatis");
            antraste.ForeColor = GT.Color.Black;
            antraste2 = new Text(font3, "");
            antraste.ForeColor = GT.Color.Black;
            canvas.Children.Add(antraste);
            canvas.Children.Add(antraste2);
            Canvas.SetLeft(antraste, 170);
            Canvas.SetTop(antraste, 110);
            Canvas.SetLeft(antraste2, 150);
            Canvas.SetTop(antraste2, 40);
            #endregion
        


            antraste.TextContent = "Ready";
            antraste2.TextContent = "";

            button.ButtonPressed += button_ButtonPressed;
            button.Mode = Button.LedMode.ToggleWhenPressed;

            button2.ButtonPressed += button_ButtonPressed2;
            button2.Mode = Button.LedMode.ToggleWhenPressed;

            unlockTimer.Tick += unlockTimer_Tick;
            denyTimer.Tick += denyTimer_Tick;

            rfidReader.IdReceived += rfidReader_IdReceived;
            
            lockDoor();
            
           
        }
       
        private void lockDoor()
        {
            // relayX1.TurnOff();
            led7C.SetColor(LED7C.Color.Blue);
            antraste.TextContent = "Ready";
            antraste2.TextContent = "";
            //displayT43.Clear();
            // antraste = new Text("Ready");
            //characterDisplay.Print("Ready");
        }
        //void registracija()
        //{

        //    using (StreamReader reader = new StreamReader("/./.vardai.txt")) 
        //    {
        //        string v;
        //        while ((v = reader.ReadLine()) != null)
        //        {
        //            registruoti[i++] = v;
        //        }

        //    }
        //}

        void rfidReader_IdReceived(RFIDReader sender, string e)
        {
            if (unlockTimer.IsRunning || denyTimer.IsRunning)
                return;

            //characterDisplay.Clear();
           // label.TextContent = (e);
            antraste.TextContent = (e);
            //characterDisplay.SetCursorPosition(1, 0);

            if (button.IsLedOn)
            {
                if (!rfidHash.Contains(e))
                {
                    
                    rfidHash.Add(e, i++);
                    antraste.TextContent = "Addingasdasfasfsd";
                    antraste2.TextContent = "";

                    bandymas.Database db = new bandymas.Database();
                    db.insertAsmuo(rfidHash[e].ToString());
                    

                }
                else
                {
                    rfidHash.Remove(e);
                    antraste.TextContent = "Removing";
                    antraste2.TextContent = "";
                    //     antraste = new Text("Removing");
                }
            }
            else
            {
                openDoor(rfidHash.Contains(e),e);
            }
        }

        private void openDoor(bool open, string e)
        {
            if (open)
            {
               antraste.TextContent = "Enter";
               antraste2.TextContent = "";
                //  antraste = new Text("Enter");
                tunes.Play(1000, 500);
                led7C.SetColor(LED7C.Color.Green);
                // relayX1.TurnOn();
                unlockTimer.Start();
                antraste2.TextContent =  rfidHash[e].ToString();
                //bandymas.Database db = new bandymas.Database();
                //db.insertAsmuo("6");
                antraste.TextContent = rfidHash[e].ToString();
            }
            else
            {
              antraste.TextContent = "Denied";
              antraste2.TextContent = "";
                //  antraste = new Text("Denied");
                tunes.Play(200, 1000);
                led7C.SetColor(LED7C.Color.Red);
                //relayX1.TurnOff();
                denyTimer.Start();
            }
        }

        void denyTimer_Tick(GT.Timer timer)
        {
            denyTimer.Stop();
            lockDoor();
        }

        void unlockTimer_Tick(GT.Timer timer)
        {
            unlockTimer.Stop();
            lockDoor();
        }

        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            //characterDisplay.Clear();

            if (button.IsLedOn)
            {
                led7C.SetColor(LED7C.Color.White);
              antraste.TextContent = "Learning Mode";
              antraste2.TextContent = "";
                // antraste = new Text("Learning Mode");
            }
            else
            {
                lockDoor();
            }
        }
        void button_ButtonPressed2(Button sender, Button.ButtonState state)
        {
            //characterDisplay.Clear();
            string ats = "test";
            antraste2.TextContent = rfidHash.ToString();
           //antraste.TextContent=rfidHash
            
        }
    }
}
