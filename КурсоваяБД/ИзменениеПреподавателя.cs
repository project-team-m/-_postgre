using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ИзменениеПреподавателя : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        
        public ИзменениеПреподавателя()
        {
            InitializeComponent();
        }

        private void ИзменениеПреподавателя_Load(object sender, EventArgs e)
        {
            string s = ТаблицаПреподаватели.s;
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM teacher", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (s == dbDataRecord[0].ToString())
                {
                    textBox1.Text = dbDataRecord[1].ToString();
                    textBox2.Text = dbDataRecord[2].ToString();
                    break;
                }
            }
            npgSqlConnection.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update teacher set idpeople = '" + textBox1.Text + "', experience = '" +
                textBox2.Text + "' where idteacher = " + ТаблицаПреподаватели.s + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно изменено!");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
