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
    public partial class UpdatePass :Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecurityDB_Despizhek;Integrated Security=True");
        
        int ID;
        public UpdatePass (int id)
        {
            InitializeComponent( );
            ID = id;
        }
        private void UpdatePass_FormClosing (object sender, FormClosingEventArgs e)
        {
            /*Authorization_frm f1 = new Authorization_frm( );
            f1.ShowDialog( );*/
        }

        private void button1_Click (object sender, EventArgs e)
        {
            connect.Open( );
            string oldpass, pass, passCheck;
            oldpass = oldPassText.Text;
            pass = newpassText.Text;
            passCheck = newpassCheckText.Text;
            DateTime date = DateTime.Now;
            SqlCommand CheckOldPass = new SqlCommand($"select Password from dbo.User_tbl where ID = '{ID}'", connect);
            string checkold = CheckOldPass.ExecuteScalar( ).ToString();
            if ( oldpass == "" || pass == "" || passCheck == "" )
            {
                MessageBox.Show("Не все поля ввода данных заполнены", "Внимание");
                connect.Close( );
                return;
            }
            if ( pass != passCheck )
            {
                MessageBox.Show("Пароли не совпадают", "Внимание");
                connect.Close( );
                return;
            }
            if ( oldpass != checkold )
            {
                MessageBox.Show("Cтарый пароль не совпадает", "Внимание");
                connect.Close( );
                return;
            }
            SqlCommand UpdatePass = new SqlCommand($"Update User_tbl set Password = '{passCheck}' where ID = '{ID}'", connect);
            SqlCommand UpdateDateChange = new SqlCommand($"Update User_tbl set DateChange = '{date}' where ID = '{ID}'", connect);
            UpdatePass.ExecuteNonQuery( );
            UpdateDateChange.ExecuteNonQuery( );
            MessageBox.Show("Пароль изменен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connect.Close( );
            this.Close( );
        }
    }
}
