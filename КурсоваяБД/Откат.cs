using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    public partial class Откат : Form
    {
        public static string s;
        string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";

        public Откат()
        {
            InitializeComponent();
        }

        public void In(string ss)
        {
            s = ss;
        }

        private void Откат_Load(object sender, EventArgs e)
        {
            if (s == "address")
            {

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 8;
                dataGridView1.Columns[0].HeaderText = "Код";
                dataGridView1.Columns[1].HeaderText = "Город";
                dataGridView1.Columns[2].HeaderText = "Улица";
                dataGridView1.Columns[3].HeaderText = "Дом";
                dataGridView1.Columns[4].HeaderText = "Квартира";
                dataGridView1.Columns[5].HeaderText = "Индекс";
                dataGridView1.Columns[6].HeaderText = "Действие";
                dataGridView1.Columns[7].HeaderText = "Время";

                int i = 0;

                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM addressh", npgSqlConnection);
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
                    dataGridView1[6, i].Value = dbDataRecord[6];
                    dataGridView1[7, i].Value = dbDataRecord[7];
                    i++;
                }
                npgSqlConnection.Close();

            }

            if (s == "group")
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 6;
                dataGridView1.Columns[0].HeaderText = "Код";
                dataGridView1.Columns[1].HeaderText = "Название группы";
                dataGridView1.Columns[2].HeaderText = "Курс";
                dataGridView1.Columns[3].HeaderText = "Код направления подготовки";
                dataGridView1.Columns[4].HeaderText = "Действие";
                dataGridView1.Columns[5].HeaderText = "Время";

                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                NpgsqlCommand npgSqlCommand = new NpgsqlCommand("SELECT * FROM groupstudh", npgSqlConnection);
                NpgsqlDataReader npgSqlDataReader = npgSqlCommand.ExecuteReader();

                int i = 0;
                foreach (DbDataRecord dbDataRecord in npgSqlDataReader)
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1[0, i].Value = dbDataRecord[0];
                    dataGridView1[1, i].Value = dbDataRecord[1];
                    dataGridView1[2, i].Value = dbDataRecord[3];
                    dataGridView1[3, i].Value = dbDataRecord[2];
                    dataGridView1[4, i].Value = dbDataRecord[4];
                    dataGridView1[5, i].Value = dbDataRecord[5];
                    i++;
                }
                npgSqlConnection.Close();
            }
        }

        private void Click_()
        {
            if (s == "address")
            {
                if (dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString() == "DELETE")
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO address (city, street, house, room, indexaddress) VALUES ('"
                        + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '"
                        + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '"
                        + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '"
                        + dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '"
                        + dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "')", npgSqlConnection);

                    NpgsqlCommand npgSqlCommand1 = new NpgsqlCommand("DELETE FROM addressh WHERE \"idaddress\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand1.ExecuteNonQuery();

                    npgSqlCommand.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Успешно!");
                }

                if (dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString() == "INSERT")
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();

                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM addressh WHERE \"idaddress\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand.ExecuteNonQuery();

                    NpgsqlCommand npgSqlCommand1 = new NpgsqlCommand("DELETE FROM address WHERE \"idaddress\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand1.ExecuteNonQuery();

                    npgSqlCommand.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Успешно!");
                }

                if (dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString() == "UPDATE")
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();

                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update address set city = '"
                        + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', street = '"
                        + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', house = '"
                        + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', room = '"
                        + dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', indexaddress = '"
                        + dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' where idaddress = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);
                    npgSqlCommand.ExecuteNonQuery();

                    NpgsqlCommand npgSqlCommand1 = new NpgsqlCommand("DELETE FROM addressh WHERE \"idaddress\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand1.ExecuteNonQuery();

                    npgSqlConnection.Close();
                    MessageBox.Show("Успешно!");

                }
            }

            if (s == "group")
            {
                if (dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() == "DELETE")
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("INSERT INTO groupstud (groupname, course, idtrainingdirection) VALUES ('"
                        + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '"
                        + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', '"
                        + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "')", npgSqlConnection);
                    npgSqlCommand.ExecuteNonQuery();
                    NpgsqlCommand npgSqlCommand1 = new NpgsqlCommand("DELETE FROM groupstudh WHERE \"idgroup\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand1.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Успешно!");
                }

                if (dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() == "INSERT")
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("DELETE FROM groupstudh WHERE \"idgroup\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand.ExecuteNonQuery();
                    NpgsqlCommand npgSqlCommand1 = new NpgsqlCommand("DELETE FROM groupstud WHERE \"idgroup\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);// ;
                    npgSqlCommand1.ExecuteNonQuery();
                    npgSqlCommand.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Успешно!");
                }

                if (dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() == "UPDATE")
                {
                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();

                    NpgsqlCommand npgSqlCommand = new NpgsqlCommand("update groupstud set groupname = '"
                        + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', IDTrainingDirection = '"
                        + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "', Course  = '"
                        + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' where idgroup = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);
                    NpgsqlCommand npgSqlCommand1 = new NpgsqlCommand("DELETE FROM groupstudh WHERE \"idgroup\" = " + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + ";", npgSqlConnection);
                    npgSqlCommand.ExecuteNonQuery();
                    npgSqlCommand1.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Успешно!");
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Click_();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Click_();
        }
    } 
}
