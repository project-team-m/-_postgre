using System;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ДобавлениеПреподавателя : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        
        public ДобавлениеПреподавателя()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") MessageBox.Show("Поля не могут быть пустыми!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (Convert.ToInt32(textBox2.Text) < 0) MessageBox.Show("Стаж не может быть отрицательным!");
                else
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();

                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO teacher (idpeople, experience) VALUES ('"
                        + textBox1.Text + "', '" + textBox2.Text + "')", npgSqlConnection);
                    npgSqlCommand.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show("Успешно добавлено!");
                }
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
    }
}
