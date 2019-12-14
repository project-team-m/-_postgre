using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ИзменениеПредмета : Form
    {
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public ИзменениеПредмета()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update subject set subjectname = '" + textBox1.Text + "' where id = " + ТаблицаПредметы.s + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно изменено!");
        }

        private void ИзменениеПредмета_Load(object sender, EventArgs e)
        {
            string s = ТаблицаПредметы.s;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM subject", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                if (s == dbDataRecord[0].ToString())
                {
                    textBox1.Text = dbDataRecord[1].ToString();
                    break;
                }
            }
            npgSqlConnection.Close();
        }
    }
}
