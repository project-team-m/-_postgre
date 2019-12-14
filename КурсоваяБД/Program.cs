using System;
using System.Data.Common;
using System.Windows.Forms;
using Npgsql;

namespace КурсоваяБД
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        //обнуление h таблиц
        static void Clear()
        {
            string connectionString = "Server=localhost;Port=5432;User ID=postgres;Password=0;Database=Student;";
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            npgSqlConnection.Open();
            NpgsqlCommand npgSqlCommand = new NpgsqlCommand("TRUNCATE addressh, groupstudh, loadingh, parentsh, peopleh, ratingh, studenth, subjecth, teacherh, trainingdirectionh", npgSqlConnection);// ;
            npgSqlCommand.ExecuteNonQuery();
            npgSqlConnection.Close();
        }
        
        static void Main()
        {
            Clear();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Админ());
        }
    }
}
