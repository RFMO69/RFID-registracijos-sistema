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
    public partial class Form2 : Form
    {
        Database db;
        object key;
        public Form2()
        {
            InitializeComponent();
            db = new Database();
           


            db.getAsmuoList(comboBox1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void selectAsmuo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            key = (comboBox1.SelectedItem as Database.ComboboxItem).Value;
        }
        public void UpdateAsmuo()
        {
            
            db.sqlConnect.Open();

            try
            {
                string query;
                int a,a2;
                bool result = Int32.TryParse(textBox1.Text,out a);
                bool result2 = Int32.TryParse(textBox2.Text, out a2);

                if (result == true || result2 == true)
                {
                    MessageBox.Show("Blogai įvesti (vardas / pavardė)");
                }
                else
                {
                    query = string.Format("UPDATE Asmuo SET Vardas='{0}',Pavarde='{1}',Asmens_kodas='{2}',Iejimo_lygis='{3}' WHERE Asmuo.id ='{4}'",
                       textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text), key.ToString());

                    MySqlCommand cmd = new MySqlCommand(query, db.sqlConnect);

                    cmd.ExecuteNonQuery();

                    Close();
                }

            }
            catch (FormatException e)
            {
                MessageBox.Show("Blogai įvesti (asmens kodas / iėjimo lygis)");
            }

            db.sqlConnect.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAsmuo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
