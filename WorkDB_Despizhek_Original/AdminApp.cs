using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkDB_Despizhek
{
    public partial class AdminApp :Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecurityDB_Despizhek;Integrated Security=True");
        SqlDataAdapter adptr;
        DataTable table;
        int ID;
        public AdminApp (int id)
        {
            InitializeComponent( );
            ID = id;
        }

        private void AdminApp_FormClosing (object sender, FormClosingEventArgs e)
        {
            Application.Exit( );
        }

        private void AdminApp_Load (object sender, EventArgs e)
        {
            Reload( );
        }
        public void Reload ()
        {
            connect.Open( );
            adptr = new SqlDataAdapter("select * from User_tbl", connect);
            table = new DataTable( );
            adptr.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close( );
        }

        private void button2_Click (object sender, EventArgs e)
        {
            Reload( );
        }

        private void button1_Click (object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            string login=loginText.Text;
            connect.Open( );
            if ( login == "" )
            {
                MessageBox.Show("Поле ввода пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect.Close( );
                return;
            }
            SqlCommand comm = new SqlCommand($"select count(*) from dbo.User_tbl where Login='{login}'", connect);
            if ( (int) comm.ExecuteScalar( ) != 0 )
            {
                SqlCommand CountNull = new SqlCommand($"Update User_tbl set Count = 0 where Login = '{login}'", connect);
                SqlCommand DaysNow = new SqlCommand($"Update User_tbl set Date = '{datenow}' where Login = '{login}'", connect);
                SqlCommand Unlock = new SqlCommand($"Update User_tbl set Active = 'true' where Login = '{login}'", connect);
                Unlock.ExecuteNonQuery( );
                DaysNow.ExecuteNonQuery( );
                CountNull.ExecuteNonQuery( );
                MessageBox.Show("Пользователь разблокирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginText.Text = "";
            }
            else
            {
                MessageBox.Show("Такой логин не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connect.Close( );
        }

        private void button3_Click (object sender, EventArgs e)
        {
            string login = loginText.Text;
            connect.Open( );
            if ( login == "" )
            {
                MessageBox.Show("Поле ввода пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect.Close( );
                return;
            }
            SqlCommand comm = new SqlCommand($"select count(*) from dbo.User_tbl where Login='{login}'", connect);
            if ( (int) comm.ExecuteScalar( ) != 0 )
            {
                SqlCommand Lock = new SqlCommand($"Update User_tbl set Active = 'false' where Login = '{login}'", connect);
                Lock.ExecuteNonQuery( );
                MessageBox.Show("Пользователь заблокирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginText.Text = "";
            }
            else
            {
                MessageBox.Show("Такой логин не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connect.Close( );
        }
    }
}
