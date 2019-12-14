using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ТаблицаПредметы : Form
    {
        public static string s;
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        
        public ТаблицаПредметы()
        {
            InitializeComponent();
        }

        public void UpdateDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Предмет";

            int i = 0;

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM subject", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                dataGridView1.RowCount += 1;
                dataGridView1[0, i].Value = dbDataRecord[0];
                dataGridView1[1, i].Value = dbDataRecord[1];
                i++;
            }
            npgSqlConnection.Close();
        }


        private void ТаблицаПредметы_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            s = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new ДобавлениеПредмета();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new ИзменениеПредмета();
            f.Show();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM subject WHERE \"id\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
            npgSqlCommand.ExecuteNonQuery();
            npgSqlConnection.Close();
            MessageBox.Show("Успешно удалено!", dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString());
        }
    }
}
