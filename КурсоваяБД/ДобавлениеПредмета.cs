using System;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ДобавлениеПредмета : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public ДобавлениеПредмета()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Поля не могут быть пустыми!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else 
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO subject (subjectname) VALUES ('" + textBox1.Text + "')", npgSqlConnection);
                npgSqlCommand.ExecuteNonQuery();
                npgSqlConnection.Close();
                MessageBox.Show("Успешно добавлено!");
            }
        }
    }
}
