using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class Преподаватель : Form
    {
        string nZach;
        string fam;
        int idSubject;
        int StudID;

        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public Преподаватель()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                if (dbDataRecord[3].ToString() == textBox1.Text)
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == (dataGridView1.CurrentRow.Index + 1).ToString())
                {
                    label2.Text = "Студент:\n" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\n" + dataGridView1.Rows[i].Cells[3].Value.ToString();
                    nZach = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    fam = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    break;
                }
            }
        }
        
        private void GetStudID()
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select student.nzach, student.idstudent from student", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (dbDataRecord[0].ToString() == nZach)
                {
                    StudID = Convert.ToInt32(dbDataRecord[1].ToString());
                    break;
                }
            }
            npgSqlConnection.Close();
            //MessageBox.Show(StudID.ToString()+ "StudID");
        }

        private void GetSubjectID()
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select * from subject", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (comboBox1.SelectedItem.ToString() == dbDataRecord[1].ToString())
                {
                    idSubject = Convert.ToInt32(dbDataRecord[0].ToString());
                    break;
                }
            }
            npgSqlConnection.Close();
            //MessageBox.Show(idSubject.ToString()+ "idSubject");
        }

        private void Преподаватель_Load(object sender, EventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select subject.subjectname FROM subject", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            //заполнение combobox предметами
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader) comboBox1.Items.Add(dbDataRecord[0]);
            
            npgSqlConnection.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") MessageBox.Show("Введите оценку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Convert.ToInt32(textBox2.Text) > 5 || Convert.ToInt32(textBox2.Text) < 2) MessageBox.Show("Оценка может быть от 2 до 5 даллов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                GetStudID();
                GetSubjectID();
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO rating (idsubject, rating, idstudent) VALUES ('"
                     + idSubject + "', '" + Convert.ToInt32(textBox2.Text) + "', '" + StudID + "')", npgSqlConnection);
                npgSqlCommand.ExecuteNonQuery();
                npgSqlConnection.Close();
                MessageBox.Show("Успешно добавлено!");
                textBox2.Text = "";
                RatingShow();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") button1.Enabled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        public void RatingShow()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.ColumnCount = 4;
            dataGridView2.RowCount = 1;
            dataGridView2.Columns[0].HeaderText = "Фамилия";
            dataGridView2.Columns[1].HeaderText = "Имя";
            dataGridView2.Columns[2].HeaderText = "Предмет";
            dataGridView2.Columns[3].HeaderText = "Среднеий бал";
            
            int j = 4;

            //средний бал
            double ball = 0;
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("select student.idstudent,  subject.id, subject.subjectname, rating.rating, rating.idstudent, people.family, people.name, groupstud.groupname "+
                                                            "from student, subject, rating, people, groupstud "+
                                                            "where people.idpeople = student.idpeople and rating.idsubject = "+ idSubject + " and people.family = '"
                                                            + fam + "' and rating.idstudent = '"+ StudID+"'", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (dbDataRecord[2].ToString() == comboBox1.SelectedItem.ToString() && (idSubject) == Convert.ToInt32(dbDataRecord[1].ToString()) && (StudID) == Convert.ToInt32(dbDataRecord[0].ToString()))
                {
                    dataGridView2[0, 0].Value = dbDataRecord[4].ToString();
                    dataGridView2[1, 0].Value = dbDataRecord[5].ToString();
                    dataGridView2[2, 0].Value = dbDataRecord[6].ToString();
                    if (dataGridView2.ColumnCount == j) dataGridView2.ColumnCount += 1;
                    dataGridView2[j, 0].Value += dbDataRecord[3].ToString();
                    j++;
                    ball += Convert.ToInt32(dbDataRecord[3].ToString());
                    dataGridView2[3, 0].Value = ball / (j - 4); ;
                }
            }
            npgSqlConnection.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            GetStudID();
            GetSubjectID();
            RatingShow();
        }
    }
}