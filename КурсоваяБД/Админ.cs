using System;
using System.Windows.Forms;

namespace КурсоваяБД
{
    public partial class Админ : Form
    {
        public Админ()
        {
            InitializeComponent();
        }

        private void Админ_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].HeaderText = "Название таблицы";
            dataGridView1[0, 0].Value = "Студент";
            dataGridView1[0, 1].Value = "Человек";
            dataGridView1[0, 2].Value = "Адреса";
            dataGridView1[0, 3].Value = "Группы";
            dataGridView1[0, 4].Value = "Нагрузка";
            dataGridView1[0, 5].Value = "Направление подготовки";
            dataGridView1[0, 6].Value = "Оценки";
            dataGridView1[0, 7].Value = "Предметы";
            dataGridView1[0, 8].Value = "Преподаватели";
            dataGridView1[0, 9].Value = "Родители";
        }

        private void OpenTable()
        {
            switch (dataGridView1.CurrentCell.Value.ToString())
            {
                case "Студент":
                    var f = new ТаблицаСтудент();
                    f.Show();
                    break;
                case "Человек":
                    var f1 = new ТаблицаЧеловек();
                    f1.Show();
                    break;
                case "Адреса":
                    var f2 = new ТаблицаАдреса();
                    f2.Show();
                    break;
                case "Группы":
                    var f3 = new ТаблицаГруппы();
                    f3.Show();
                    break;
                case "Нагрузка":
                    var f4 = new ТаблицаНагрузка();
                    f4.Show();
                    break;
                case "Направление подготовки":
                    var f5 = new ТаблицаНаправлениеПодготовки();
                    f5.Show();
                    break;
                case "Оценки":
                    var f6 = new ТаблицаОценки();
                    f6.Show();
                    break;
                case "Предметы":
                    var f7 = new ТаблицаПредметы();
                    f7.Show();
                    break;
                case "Преподаватели":
                    var f8 = new ТаблицаПреподаватели();
                    f8.Show();
                    break;
                case "Родители":
                    var f9 = new ТаблицаРодители();
                    f9.Show();
                    break;
                default:
                    //MessageBox.Show("В разработке");
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenTable();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            OpenTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new Запросник();
            f.Show();
        }

        private void студентаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new Откат();
            f.Show();
        }
    }
}