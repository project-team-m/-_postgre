using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class ТаблицаАдреса : Form
    {
        public static string s;
        public static string tmp = "address";

        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
        
        public ТаблицаАдреса()
        {
            InitializeComponent();
        }

        public void UpdateDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].HeaderText = "Код";
            dataGridView1.Columns[1].HeaderText = "Город";
            dataGridView1.Columns[2].HeaderText = "Улица";
            dataGridView1.Columns[3].HeaderText = "Дом";
            dataGridView1.Columns[4].HeaderText = "Квартира";
            dataGridView1.Columns[5].HeaderText = "Индекс";
            /*DataTable table = new DataTable();
            table.Load(npgSqlCommand.ExecuteReader());
            dataGridView1.DataSource = table;*/

            int i = 0;
            
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();

            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM address", npgSqlConnection);
            NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();


            foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
            {
                dataGridView1.RowCount += 1;
                dataGridView1[0, i].Value = dbDataRecord[0];
                dataGridView1[1, i].Value = dbDataRecord[1];
                dataGridView1[2, i].Value = dbDataRecord[2];
                dataGridView1[3, i].Value = dbDataRecord[3];
                dataGridView1[4, i].Value = dbDataRecord[4];
                dataGridView1[5, i].Value = dbDataRecord[5];
                i++;
            }
            npgSqlConnection.Close();
        }

        private void ТаблицаАдреса_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new ДобавлениеАдреса();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new ИзменениеАдреса();
            f.Show();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            s = dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();   
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM address WHERE \"idaddress\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);
            npgSqlCommand.ExecuteNonQuery();
            npgSqlConnection.Close();
            MessageBox.Show("Успешно удалено!", dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString());
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.RowIndex.ToString());
            /*string query = "INSERT INTO Адреса (Город, Улица, Дом, Квартира, Индекс) VALUES ('" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '" + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '" + dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '" + dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "')";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();*/
            MessageBox.Show("ADD", dataGridView1.CurrentCell.RowIndex.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new Откат();
            f.In("address");
            f.Show();
            UpdateDataGrid();
        }
    }
}
