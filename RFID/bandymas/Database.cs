using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace bandymas
{
    public class Database
    {
    public MySqlConnection sqlConnect = new MySqlConnection();
    private MySqlCommand cmd;
    private MySqlDataReader dataReader;

    MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
    //Korteles[] duom = new Korteles[100];
    
    

    
    public Database()
    {
        conn_string.Server = "remotemysql.com";
        conn_string.UserID = "NwJnrtfKfo";
        conn_string.Password = "DHVIrmCVcC";
        conn_string.Database = "NwJnrtfKfo";
        conn_string.Port = 3306;

        sqlConnect.ConnectionString = conn_string.ToString();
         //Hashtable duomenys = new Hashtable();
         //duomenys = GadgeteerApp4.Program.rfidHash;
    }
    public bool connected()
    {
        if (sqlConnect != null) return true;
        return false;
    }

    public void getAsmuo(DataTable asmuo)
    {
        sqlConnect.Open();
        asmuo.Clear();


            string query = "SELECT Asmuo.id,Asmuo.Vardas,Asmuo.Pavarde, " +
                "Asmuo.Asmens_kodas, Asmuo.Iejimo_lygis FROM Asmuo ";
        cmd = new MySqlCommand(query, sqlConnect);

        dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {

            string id = dataReader["id"].ToString();
            string vard = dataReader["Vardas"].ToString();
            string pav = dataReader["Pavarde"].ToString();
            string asmens_kodas = dataReader["Asmens_kodas"].ToString();
            string iejimo_lygis = dataReader["Iejimo_lygis"].ToString();

            asmuo.Rows.Add(new object[] { id, vard, pav, asmens_kodas,iejimo_lygis });
        }
        sqlConnect.Close();

    }
        public void getPatalpa(DataTable patalpa)
        {
            sqlConnect.Open();
            patalpa.Clear();


            string query = "SELECT Patalpa.id,Patalpa.Kabineto_numeris,Patalpa.Data,Patalpa.Iejimo_laikas, Patalpa.Patalpos_iejimo_lygis, Asmuo.Vardas, Asmuo.Iejimo_lygis," +
                     "IF(Patalpa.Patalpos_iejimo_lygis <= Asmuo.Iejimo_lygis, 'Įleista', 'Neįleista') AS leidimas FROM Patalpa INNER JOIN Asmuo ON Patalpa.fk_Asmuoid = Asmuo.id";
            cmd = new MySqlCommand(query, sqlConnect);

            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {

                string id = dataReader["id"].ToString();
                string kab_nr = dataReader["Kabineto_numeris"].ToString();
                string data = dataReader["Data"].ToString();
                string iejimo_laikas = dataReader["Iejimo_laikas"].ToString();
                string iejimo_lygis = dataReader["Patalpos_Iejimo_lygis"].ToString();
                string vard = dataReader["Vardas"].ToString();
                string iejimo_lygis2 = dataReader["Iejimo_lygis"].ToString();
                string leidimas = dataReader["leidimas"].ToString();

                
                patalpa.Rows.Add(new object[] { id, kab_nr, data, iejimo_laikas, iejimo_lygis, vard, iejimo_lygis2,leidimas});



                // patalpa.Rows.Add(new object[] { id, vard, pav, asmens_kodas, iejimo_lygis });
            }
            sqlConnect.Close();

        }


        public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object Value2 { get; set; }
        public object Valueid { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
    public void getAsmuoList(ComboBox Asmuo)
    {
        sqlConnect.Open();
        Asmuo.Items.Clear();

        string query = "SELECT Asmuo.id FROM Asmuo WHERE Asmuo.Vardas IS NULL";

        cmd = new MySqlCommand(query, sqlConnect);

        dataReader = cmd.ExecuteReader();


        while (dataReader.Read())
        {
            ComboboxItem item = new ComboboxItem();
                string id;
            id = dataReader["id"].ToString();
             
                item.Value = id;
                item.Text = ""+id;

            Asmuo.Items.Add(item);
        }
        sqlConnect.Close();
    }
        public void getAsmuoList2(ComboBox Asmuo)
        {
            sqlConnect.Open();
            Asmuo.Items.Clear();

            string query = "SELECT Asmuo.id, Asmuo.Vardas FROM Asmuo WHERE Asmuo.Vardas IS NOT NULL AND Asmuo.Pavarde IS NOT NULL";

            cmd = new MySqlCommand(query, sqlConnect);

            dataReader = cmd.ExecuteReader();


            while (dataReader.Read())
            {
                ComboboxItem item = new ComboboxItem();
                string id, vard;
                id = dataReader["id"].ToString();
                vard = dataReader["Vardas"].ToString();

                item.Valueid = id;
                item.Value = vard;
                item.Text = "" +vard;

                Asmuo.Items.Add(item);
            }
            sqlConnect.Close();
        }
        public void getPatalpaList(ComboBox Patalpa)
        {
            sqlConnect.Open();
            Patalpa.Items.Clear();

            string query = "SELECT Patalpa.id, Patalpa.Kabineto_numeris,Patalpa.Patalpos_iejimo_lygis FROM Patalpa";

            cmd = new MySqlCommand(query, sqlConnect);

            dataReader = cmd.ExecuteReader();


            while (dataReader.Read())
            {
                ComboboxItem item = new ComboboxItem();
                string id, vard, patalpa;
                id = dataReader["id"].ToString();
                vard = dataReader["Kabineto_numeris"].ToString();
                patalpa = dataReader["Patalpos_iejimo_lygis"].ToString();

                item.Valueid = id;
                item.Value = vard;
                item.Value2 = patalpa;
                item.Text = "" + vard;

                Patalpa.Items.Add(item);
            }
            sqlConnect.Close();
        }
        public void getImoneList(ComboBox Imone)
        {
            sqlConnect.Open();
            Imone.Items.Clear();

            string query = "SELECT Imone.id, Imone.Pavadinimas FROM Imone";

            cmd = new MySqlCommand(query, sqlConnect);

            dataReader = cmd.ExecuteReader();


            while (dataReader.Read())
            {
                ComboboxItem item = new ComboboxItem();
                string id, vard;
                id = dataReader["id"].ToString();
                vard = dataReader["Pavadinimas"].ToString();
                

                item.Valueid = id;
                item.Value = vard;
                item.Text = "" + vard;

                Imone.Items.Add(item);
            }
            sqlConnect.Close();
        }
        public void insertAsmuo(string id)
        {
            sqlConnect.Open();

            string query;

            query = string.Format("INSERT INTO Asmuo (id) VALUES ('{0}')", id);

            MySqlCommand cmd = new MySqlCommand(query, sqlConnect);

            cmd.ExecuteNonQuery();

            sqlConnect.Close();
        }
        
    }

}
