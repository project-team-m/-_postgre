using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;


namespace КурсоваяБД
{
    public partial class ИзменениеНаправленияПодготовки : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        public ИзменениеНаправленияПодготовки()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update trainingdirection set name = '" + textBox1.Text + "', cipher = '" +
                textBox2.Text + "' where idtrainingdirection = " + ТаблицаНаправлениеПодготовки.s + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно изменено!");
        }

        private void ИзменениеНаправленияПодготовки_Load(object sender, EventArgs e)
        {
            string s = ТаблицаНаправлениеПодготовки.s;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM trainingdirection", npgSqlConnection);
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
    }
}