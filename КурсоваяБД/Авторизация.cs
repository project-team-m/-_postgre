using System;
using System.Windows.Forms;

namespace КурсоваяБД
{
    public partial class Авторизация : Form
    {
        public Авторизация()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] loginMas = new string[] { "Студент", "Преподаватель", "Админ" };

            if(login.Text == loginMas[0])
            {
                var f = new Студент();
                f.Show();
            }

            if(login.Text == loginMas[1])
            {
                if (password.Text == loginMas[1])
                {
                    var f = new Преподаватель();
                    f.Show();
                }else { MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (login.Text == loginMas[2] )
            {
                if (password.Text == loginMas[2])
                {
                    var f = new Админ();
                    f.Show();
                }else { MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            else
            {
                int count = 0;
                foreach (var i in loginMas)
                {
                    if (login.Text != i) count++;
                }
                if(count == loginMas.Length) MessageBox.Show("Логин не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
