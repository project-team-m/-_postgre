using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ИзменениеОценок : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public ИзменениеОценок()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox2.Text) > 5 || Convert.ToInt32(textBox2.Text) < 2) MessageBox.Show("Оценка может быть от 2 до 5 даллов");
            else
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update rating set rating = '" + textBox2.Text + "' where id = " + ТаблицаОценки.s + ";", npgSqlConnection);
                npgSqlCommand.ExecuteNonQuery();

                MessageBox.Show("Успешно изменено!");
            }
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void ИзменениеОценок_Load(object sender, EventArgs e)
        {
            string s = ТаблицаОценки.s;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM rating", npgSqlConnection);
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
    }
}
