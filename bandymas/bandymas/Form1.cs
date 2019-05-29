using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bandymas
{
    public partial class Form1 : Form
    {
        Database db;
        object key, key2,key3, patalpa;
        object keyid, keyid2, keyid3;
        public Form1()
        {
            InitializeComponent();
            db = new Database();
            panel3.BringToFront();
            // dataGridView1.Hide();
            comboBox1.Hide();
            comboBox2.Hide();
            button5.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            textBox1.Hide();
            label5.Hide();
            comboBox3.Hide();

            db.getAsmuoList2(comboBox1);
            db.getPatalpaList(comboBox2);
            db.getImoneList(comboBox3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form2 f = new Form2();
            f.ShowDialog();
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            comboBox1.Hide();
            comboBox2.Hide();
            button5.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            textBox1.Hide();
            label5.Hide();
            comboBox3.Hide();

            DataTable Asmuo = new DataTable();

            Asmuo.Columns.Add("ID");
            Asmuo.Columns.Add("Vardas");
            Asmuo.Columns.Add("Pavardė");
            Asmuo.Columns.Add("Asmens kodas");
            Asmuo.Columns.Add("Įėjimo lygis");
            dataGridView1.DataSource = Asmuo;

            db.getAsmuo(Asmuo);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            comboBox1.Hide();
            comboBox2.Hide();
            button5.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            textBox1.Hide();
            label5.Hide();
            comboBox3.Hide();

            DataTable Patalpa = new DataTable();

            Patalpa.Columns.Add("ID");
            Patalpa.Columns.Add("Kabineto numeris");
            Patalpa.Columns.Add("Data");
            Patalpa.Columns.Add("Įėjimo laikas");
            Patalpa.Columns.Add("Patalpos įėjimo lygis");
            Patalpa.Columns.Add("Asmens vardas");
            Patalpa.Columns.Add("Asmens įėjimo lygis");
            Patalpa.Columns.Add("Ar buvo įleistas");

            dataGridView1.DataSource = Patalpa;

            db.getPatalpa(Patalpa);
        }
      

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            comboBox1.Show();
            comboBox2.Show();
            button5.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            textBox1.Show();
            label5.Show();
            comboBox3.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            key = (comboBox1.SelectedItem as Database.ComboboxItem).Value;
            keyid = (comboBox1.SelectedItem as Database.ComboboxItem).Valueid;
            // MessageBox.Show(key.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            key2 = (comboBox2.SelectedItem as Database.ComboboxItem).Value;
            keyid2 = (comboBox2.SelectedItem as Database.ComboboxItem).Valueid;
            patalpa = (comboBox2.SelectedItem as Database.ComboboxItem).Value2;
            //MessageBox.Show(key2.ToString());
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            key3 = (comboBox3.SelectedItem as Database.ComboboxItem).Value;
            keyid3 = (comboBox3.SelectedItem as Database.ComboboxItem).Valueid;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertPatalpa();
        }

        public void InsertPatalpa()
        {

            db.sqlConnect.Open();


            string query;

                query = string.Format("INSERT INTO Patalpa (Kabineto_numeris, Data, Iejimo_laikas, Isejimo_laikas, Patalpos_iejimo_lygis," +
                    "fk_Imoneid, fk_Asmuoid) VALUES ('{0}',CURDATE(),TIME(CONVERT_TZ(NOW(),'+10:00','+11:00')),'{1}','{2}','{3}','{4}')",
                key2.ToString(),textBox1.Text,patalpa.ToString(),keyid3.ToString(),keyid.ToString());

                MySqlCommand cmd = new MySqlCommand(query, db.sqlConnect);

                cmd.ExecuteNonQuery();

                 MessageBox.Show("Priskirimas atliktas teisingai");
            

            db.sqlConnect.Close();

        }
    }
}
