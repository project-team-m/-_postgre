using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ИзменениеАдреса : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public ИзменениеАдреса()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update address set city = '" + textBox1.Text + "', street = '" +
                textBox2.Text + "', house = '" + textBox3.Text + "', room = '" + textBox4.Text + "', indexaddress = '" +
                textBox5.Text + "' where idaddress = " + ТаблицаАдреса.s + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно изменено!");
        }

        private void ИзменениеАдреса_Load(object sender, EventArgs e)
        {
            string s = ТаблицаАдреса.s;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM address", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    if (s == dbDataRecord[0].ToString())
                    {
                        textBox1.Text = dbDataRecord[1].ToString();
                        textBox2.Text = dbDataRecord[2].ToString();
                        textBox3.Text = dbDataRecord[3].ToString();
                        textBox4.Text = dbDataRecord[4].ToString();
                        textBox5.Text = dbDataRecord[5].ToString();
                        break;
                    }
                }
            npgSqlConnection.Close();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
