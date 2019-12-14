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
    public partial class Главная : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Студенты.mdb;";
        private OleDbConnection myConnection;
        public Главная()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
        }

        private void Главная_Load(object sender, EventArgs e)
        {
            
        }

        private void Главная_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                
            }

            if (radioButton2.Checked == true)
            {
                

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            string query = "SELECT Фамилия, Имя, Отчество, Название_группы FROM Список_студентов";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Фамилия";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Отчество";
            dataGridView1.Columns[3].HeaderText = "Группа";
            int i = 0;

            while (reader.Read())
            {
                dataGridView1.RowCount += 1;
                dataGridView1[0, i].Value = reader[0].ToString();
                dataGridView1[1, i].Value = reader[1].ToString();
                dataGridView1[2, i].Value = reader[2].ToString();
                dataGridView1[3, i].Value = reader[3].ToString();
                i++;
            }

            reader.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            string query = "SELECT Фамилия, Имя, Предмет, Оценка FROM СтудентЗапросОценки";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Фамилия";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Предмет";
            dataGridView1.Columns[3].HeaderText = "Среднеий бал";
            int i = 0;
            int j = 4;
            while (reader.Read())
            {
                dataGridView1.RowCount += 1;
                dataGridView1[0, i].Value = reader[0].ToString();
                dataGridView1[1, i].Value = reader[1].ToString();
                dataGridView1[2, i].Value = reader[2].ToString();
                //dataGridView1.ColumnCount += 1;
                dataGridView1[3, i].Value = reader[3].ToString();
                i++;
                //j++;
            }

            reader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT Фамилия, Название_группы FROM СтудентГруппа";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                if ((reader[0].ToString() == textBox1.Text) && (reader[1].ToString() == textBox2.Text))
                {

                    string query1 = "SELECT Фамилия, Имя, Отчество, Пол, Номер_телефона, Название_группы, Курс, Наименование, Шифр, Город, Улица, Дом, Квартира, Индекс FROM ИнформацияОСтуденте";
                    OleDbCommand command1 = new OleDbCommand(query1, myConnection);
                    OleDbDataReader reader1 = command1.ExecuteReader();

                    while(reader1.Read())
                    {
                        if ((reader1[0].ToString() == textBox1.Text) && (reader1[5].ToString() == textBox2.Text))
                        {
                            label1.Text = "Информация о студенте:\n" +
                                "Фамилия: " + reader1[0].ToString() +
                                "\nИмя: " + reader1[1].ToString() +
                                "\nОтчество: " + reader1[2].ToString() +
                                "\nПол: " + reader1[3].ToString() +
                                "\nНомер телефона: " + reader1[4].ToString() +
                                "\n" + reader1[5].ToString() +
                                "\n" + reader1[6].ToString() +
                                "\n" + reader1[7].ToString() +
                                "\n" + reader1[8].ToString() +
                                "\n" + reader1[9].ToString() +
                                "\n" + reader1[10].ToString() +
                                "\n" + reader1[11].ToString() +
                                "\n" + reader1[12].ToString() +
                                "\n" + reader1[13].ToString();
                            break;
                            MessageBox.Show("Студент найден", "");
                        }
                    }
                }
            }
            reader.Close();
        }
    }
}
