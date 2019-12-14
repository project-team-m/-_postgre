using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ИзменениеГруппы : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public ИзменениеГруппы()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update groupstud set groupname = '" 
                + textBox1.Text + "', IDTrainingDirection = '" 
                + textBox3.Text + "', Course  = '" 
                + textBox2.Text + "' where idgroup = " + ТаблицаГруппы.s + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно изменено!");
        }

        private void ИзменениеГруппы_Load(object sender, EventArgs e)
        {
            string s = ТаблицаГруппы.s;
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * from groupstud", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (s == dbDataRecord[0].ToString())
                {
                    textBox1.Text = dbDataRecord[1].ToString();
                    textBox2.Text = dbDataRecord[2].ToString();
                    textBox3.Text = dbDataRecord[3].ToString();
                    break;
                }
            }
            npgSqlConnection.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
    }
}
