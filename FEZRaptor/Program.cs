using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace FEZRaptor
{
    public partial class Program
    {
        private Hashtable rfidHash = new Hashtable();
        private GT.Timer denyTimer = new GT.Timer(2000);
        private GT.Timer unlockTimer = new GT.Timer(3000);

        void ProgramStarted() 
        {
            Debug.Print("Program Started");

            button.ButtonPressed += button_ButtonPressed;
            button.Mode = Button.LedMode.ToggleWhenPressed;

            unlockTimer.Tick += unlockTimer_Tick;
            denyTimer.Tick += denyTimer_Tick;

            rfidReader.IdReceived += rfidReader_IdReceived;

            lockDoor();
            
                
            
        }

        private void lockDoor()
        {
            relayX1.TurnOff();
            led7C.SetColor(LED7C.Color.Blue);
            characterDisplay.Clear();
            characterDisplay.Print("Ready");
        }

        void rfidReader_IdReceived(RFIDReader sender, string e)
        {
            if (unlockTimer.IsRunning || denyTimer.IsRunning)
                return;

            characterDisplay.Clear();
            characterDisplay.Print(e);
            characterDisplay.SetCursorPosition(1, 0);

            if (button.IsLedOn)
            {
                if (!rfidHash.Contains(e))
                {
                    rfidHash.Add(e, 1);
                    characterDisplay.Print("Adding");
                }
                else
                {
                    rfidHash.Remove(e);
                    characterDisplay.Print("Removing");
                }
            }
            else
            {
                openDoor(rfidHash.Contains(e));
            }
        }

        private void openDoor(bool open)
        {
            if (open)
            {
                characterDisplay.Print("Enter");
                tunes.Play(1000, 500);
                led7C.SetColor(LED7C.Color.Green);
                relayX1.TurnOn();
                unlockTimer.Start();
            }
            else
            {
                characterDisplay.Print("Denied");
                tunes.Play(200, 1000);
                led7C.SetColor(LED7C.Color.Red);
                relayX1.TurnOff();
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
            characterDisplay.Clear();

            if (button.IsLedOn)
            {
                led7C.SetColor(LED7C.Color.White);
                characterDisplay.Print("Learning mMde");
            }
            else
            {
                lockDoor();
            }
        }
    }
}
