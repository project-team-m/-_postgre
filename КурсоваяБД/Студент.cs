using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class Студент : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        public Студент()
        {
            InitializeComponent();
        }

        //список группы
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].HeaderText = "№ студента";
            dataGridView1.Columns[1].HeaderText = "№ зачетки";
            dataGridView1.Columns[2].HeaderText = "Фамилия";
            dataGridView1.Columns[3].HeaderText = "Имя";
            dataGridView1.Columns[4].HeaderText = "Отчество";
            dataGridView1.Columns[5].HeaderText = "Группа";
            int i = 0;
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select people.family, people.name, people.patronymic, groupstud.groupname, student.nzach " +
                                                            "from student, people, groupstud " +
                                                            "where student.idpeople = people.idpeople", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (dbDataRecord[3].ToString() == textBox2.Text)
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1[0, i].Value = (i + 1);
                    dataGridView1[1, i].Value = dbDataRecord[4];
                    dataGridView1[2, i].Value = dbDataRecord[0];
                    dataGridView1[3, i].Value = dbDataRecord[1];
                    dataGridView1[4, i].Value = dbDataRecord[2];
                    dataGridView1[5, i].Value = dbDataRecord[3];
                    i++;
                }
            }
            npgSqlConnection.Close();
        }

        //показ оценок
        public void RatingShow()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                int j = 4;
                
                //средний бал
                double ball = 0;
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select people.family, people.name, subject.subjectname, groupstud.groupname, rating.rating "+
                                                                "from people, subject, rating, student, groupstud "+
                                                                "where people.idpeople = student.idpeople", npgSqlConnection);
                NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    if (dbDataRecord[2].ToString() == dataGridView1[2, i].Value.ToString())
                    {
                        if (dataGridView1.ColumnCount == j) dataGridView1.ColumnCount += 1;
                        dataGridView1[j, i].Value += dbDataRecord[4].ToString();
                        j++;
                        ball += Convert.ToInt32(dbDataRecord[4].ToString());
                        dataGridView1[3, i].Value = ball / (j - 4); ;
                    }
                }
                npgSqlConnection.Close();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "Фамилия";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Предмет";
            dataGridView1.Columns[3].HeaderText = "Среднеий бал";
            
            int i = 0;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select people.family, people.name, groupstud.groupname, subject.subjectname " +
                                                            "from people, subject, rating, student, groupstud "+
                                                            "where people.idpeople = student.idpeople "+
                                                            "group by subject.subjectname, people.family, people.name, subject.subjectname, groupstud.groupname; ", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (dbDataRecord[0].ToString() == textBox1.Text && dbDataRecord[2].ToString() == textBox2.Text)
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1[0, i].Value = dbDataRecord[0].ToString();
                    dataGridView1[1, i].Value = dbDataRecord[1].ToString();
                    dataGridView1[2, i].Value = dbDataRecord[3].ToString();
                    i++;
                }
            }
            npgSqlConnection.Close();

            RatingShow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*string query = "SELECT Фамилия, Название_группы FROM СтудентГруппа";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((reader[0].ToString() == textBox1.Text) && (reader[1].ToString() == textBox2.Text))
                {
                    string query1 = "SELECT Фамилия, Имя, Отчество, Пол, Номер_телефона, Название_группы, Курс, Наименование, Шифр, Город, Улица, Дом, Квартира, Индекс FROM ИнформацияОСтуденте";
                    OleDbCommand command1 = new OleDbCommand(query1, myConnection);
                    OleDbDataReader reader1 = command1.ExecuteReader();

                    while (reader1.Read())
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
                            MessageBox.Show("Студент найден", "");
                            break;
                        }
                    }
                }
            }
            reader.Close();*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") button2.Enabled = true;
            else { button2.Enabled = false; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") button2.Enabled = true;
            else { button2.Enabled = false; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
    }
}