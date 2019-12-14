using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КурсоваяБД
{
    public partial class ИзменениеЧеловека : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Студенты.mdb;";
        private OleDbConnection myConnection;
        public ИзменениеЧеловека()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Человек SET Фамилия = '" + textBox1.Text + "'WHERE Код = " + ТаблицаЧеловек.s + ";";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();

            string query1 = "UPDATE Человек SET Имя = '" + textBox2.Text + "'WHERE Код = " + ТаблицаЧеловек.s + ";";
            OleDbCommand command1 = new OleDbCommand(query1, myConnection);
            command1.ExecuteNonQuery();

            string query2 = "UPDATE Человек SET Отчество = '" + textBox3.Text + "'WHERE Код = " + ТаблицаЧеловек.s + ";";
            OleDbCommand command2 = new OleDbCommand(query2, myConnection);
            command2.ExecuteNonQuery();

            string query3 = "UPDATE Человек SET Пол = '" + textBox4.Text + "'WHERE Код = " + ТаблицаЧеловек.s + ";";
            OleDbCommand command3 = new OleDbCommand(query3, myConnection);
            command3.ExecuteNonQuery();

            string query4 = "UPDATE Человек SET Номер_телефона = '" + textBox5.Text + "'WHERE Код = " + ТаблицаЧеловек.s + ";";
            OleDbCommand command4 = new OleDbCommand(query4, myConnection);
            command4.ExecuteNonQuery();

            string query5 = "UPDATE Человек SET Код_адреса = '" + Convert.ToInt32(textBox6.Text) + "'WHERE Код = " + ТаблицаЧеловек.s + ";";
            OleDbCommand command5 = new OleDbCommand(query5, myConnection);
            command5.ExecuteNonQuery();

            MessageBox.Show("Успешно изменено!");
        }

        private void ИзменениеЧеловека_Load(object sender, EventArgs e)
        {
            string s = ТаблицаЧеловек.s;
            string query = "SELECT Код, Фамилия, Имя, Отчество, Пол, Номер_телефона, Код_адреса FROM Человек";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (s == reader[0].ToString())
                {
                    textBox1.Text = reader[1].ToString();
                    textBox2.Text = reader[2].ToString();
                    textBox3.Text = reader[3].ToString();
                    textBox4.Text = reader[4].ToString();
                    textBox5.Text = reader[5].ToString();
                    textBox6.Text = reader[6].ToString();
                    break;
                }
            }
            reader.Close();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
