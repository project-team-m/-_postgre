using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ТаблицаПреподаватели : Form
    {
        public static string s;
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public ТаблицаПреподаватели()
        {
            InitializeComponent();
        }

        public void UpdateDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Код человека";
            dataGridView1.Columns[2].HeaderText = "Стаж";

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM teacher", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            int i = 0;

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                dataGridView1.RowCount += 1;
                dataGridView1[0, i].Value = dbDataRecord[0];
                dataGridView1[1, i].Value = dbDataRecord[1];
                dataGridView1[2, i].Value = dbDataRecord[2];
                i++;
            }
            npgSqlConnection.Close();

        }
        private void ТаблицаПреподаватели_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            s = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new ДобавлениеПреподавателя();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new ИзменениеПреподавателя();
            f.Show();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM teacher WHERE \"idteacher\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
            npgSqlCommand.ExecuteNonQuery();
            npgSqlConnection.Close();
            MessageBox.Show("Успешно удалено!", dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString());
        }
    }
}
