using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ТаблицаЧеловек : Form
    {
        public static string s;
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        public ТаблицаЧеловек()
        {
            InitializeComponent();
        }

        public void UpdateDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Фамилия";
            dataGridView1.Columns[2].HeaderText = "Имя";
            dataGridView1.Columns[3].HeaderText = "Отчество";
            dataGridView1.Columns[4].HeaderText = "Пол";
            dataGridView1.Columns[5].HeaderText = "Номер телефона";
            dataGridView1.Columns[6].HeaderText = "Код адреса";

            int i = 0;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM people", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();
            
            if (npgSqlDataReader.HasRows)
            {
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1[0, i].Value = dbDataRecord[0];
                    dataGridView1[1, i].Value = dbDataRecord[1];
                    dataGridView1[2, i].Value = dbDataRecord[2];
                    dataGridView1[3, i].Value = dbDataRecord[3];
                    dataGridView1[4, i].Value = dbDataRecord[4];
                    dataGridView1[5, i].Value = dbDataRecord[5];
                    dataGridView1[5, i].Value = dbDataRecord[6];
                    i++;
                }
            }
            npgSqlConnection.Close();
        }

        private void ТаблицаЧеловек_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new ДобавлениеЧеловека();
            f.Show();
            UpdateDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new ИзменениеЧеловека();
            f.Show();
            UpdateDataGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            s = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM people WHERE \"idpeople\" = " + dataGridView1.CurrentCell.Value.ToString() + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();
            npgSqlConnection.Close();
            MessageBox.Show("Успешно удалено!");
        }
    }
}