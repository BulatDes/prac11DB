using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace WorkDB_Despizhek
{
    public partial class Authorization_frm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=PC325L11\SQLEXPRESS;Initial Catalog=SecurityDB_Despizhek;Integrated Security=True");
        SqlDataAdapter adptr;
        DataTable table;
        UserApp userForm;
        AdminApp adminForm;
        UpdatePass passForm;
        public string Login;
        public Authorization_frm()
        {
            InitializeComponent();
        }

        private void Authorization_frm_Load(object sender, EventArgs e)
        {
            Reload( );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect.Open();
            string login, pass, passCheck;
            login = loginText.Text;
            pass = passText.Text;
            passCheck = passCheckText.Text;
            DateTime date = DateTime.Now;

            if(login=="" || pass == "" || passCheck == "")
            {
                MessageBox.Show("Не все поля ввода данных заполнены", "Внимание");
                connect.Close( );
                return;
            }
            if (pass != passCheck)
            {
                MessageBox.Show("Пароли не совпадают", "Внимание");
                connect.Close( );
                return;
            }
            SqlCommand comm = new SqlCommand($"select count(*) from dbo.User_tbl where Login='{login}'", connect);
            if ( (int) comm.ExecuteScalar( ) == 0 )
            {
                string query = $"INSERT INTO dbo.User_tbl   (Login,  Password,  Count,  Date,  Active,   Role,DateChange)     VALUES    ('{login}' ,'{pass}'  ,0  ,'{date}'  ,'True' ,'User','{date}')";
                SqlCommand command = new SqlCommand(query, connect);
                command.ExecuteNonQuery( );
                MessageBox.Show("Новый пользователь добавлен","Информация",MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginText.Text = "";
                passText.Text = "";
                passCheckText.Text = "";
            }
            else
            {
                MessageBox.Show("Такой логин уже есть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connect.Close( );
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
        private void button2_Click(object sender, EventArgs e)
        {
            Reload( );
        }

        private void button3_Click (object sender, EventArgs e)
        {
            connect.Open( );
            string login, pass;
            login = SignLogin.Text;
            pass = SignPassword.Text;
            DateTime date = DateTime.Now;
            string datenow = date.ToString("yyyy-MM-dd");
            DateTime dateCheck = date.AddDays(-30);
            DateTime dateCheck14 = date.AddDays(-14);
            DateTime datepass;
            DateTime datepass14;
            int id;

            if ( login == "" || pass == "" )
            {
                MessageBox.Show("Не все поля ввода данных заполнены", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect.Close( );
                return;
            }
            SqlCommand checkLoginPass = new SqlCommand($"select * from dbo.User_tbl where Password='{pass}' and Login = '{login}'", connect);
            SqlCommand CheckLogin = new SqlCommand($"select * from dbo.User_tbl where Login = '{login}'", connect);
            SqlCommand checkRole = new SqlCommand($"select Role from dbo.User_tbl where Password='{pass}' and Login = '{login}'", connect);
            SqlCommand checkCount = new SqlCommand($"select Count from dbo.User_tbl where Login = '{login}'", connect);
            SqlCommand countPlus = new SqlCommand($"Update User_tbl set Count = Count + 1 where Login = '{login}'", connect);
            SqlCommand ActiveBlock = new SqlCommand($"Update User_tbl set Active = 0 where Login = '{login}'", connect);
            SqlCommand CheckActive = new SqlCommand($"select Active from dbo.User_tbl where Login = '{login}'", connect);
            SqlCommand CountNull = new SqlCommand($"Update User_tbl set Count = 0 where Login = '{login}'", connect);
            SqlCommand CheckDays = new SqlCommand($"select Date from dbo.User_tbl where Login = '{login}'", connect);
            SqlCommand DaysNow = new SqlCommand($"Update User_tbl set Date = '{datenow}' where Login = '{login}'", connect);
            SqlCommand CheckDays14 = new SqlCommand($"select DateChange from dbo.User_tbl where Login = '{login}'", connect);
            SqlCommand IdSave = new SqlCommand($"select ID from dbo.User_tbl where Login = '{login}'", connect);
            datepass = (DateTime)CheckDays.ExecuteScalar( );
            datepass14 = (DateTime) CheckDays14.ExecuteScalar( );
            if ( datepass < dateCheck )
            {
                ActiveBlock.ExecuteNonQuery( );
                MessageBox.Show("Вы заблокированы. Так как вы давно не заходили (30 дней) ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect.Close( );
                return;

            }
            if ( CheckLogin.ExecuteScalar( ) != null )
            {
                if ( (bool) CheckActive.ExecuteScalar( ) == true )
                {
                    if ( checkLoginPass.ExecuteScalar( ) != null )
                    {
                        if ( checkRole.ExecuteScalar( ).ToString( ) == "User" )
                        {
                            if ( datepass14 < dateCheck14 )
                            {
                                MessageBox.Show("Вы не меняли пароль в течение 14 дней\nМы переносим вас в форму для изменения пароля", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CountNull.ExecuteNonQuery( );
                                id = (int)IdSave.ExecuteScalar( );
                                passForm = new UpdatePass(id);
                                passForm.ShowDialog( );
                            }
                            else
                            {
                                id = (int) IdSave.ExecuteScalar( );
                                userForm = new UserApp(id);
                                CountNull.ExecuteNonQuery( );
                                DaysNow.ExecuteNonQuery( );
                                userForm.ShowDialog( );
                            }
                        }
                        else
                        {
                            id = (int) IdSave.ExecuteScalar( );
                            adminForm = new AdminApp(id);
                            DaysNow.ExecuteNonQuery( );
                            CountNull.ExecuteNonQuery( );
                            adminForm.ShowDialog( );
                        }
                    }
                    else
                    {
                        if ( (int) checkCount.ExecuteScalar( ) < 3 )
                        {
                            MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            countPlus.ExecuteNonQuery( );
                        }
                        else
                        {
                            MessageBox.Show("Вы заблокированы, Превышен лимит допустимых ошибок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ActiveBlock.ExecuteNonQuery( );
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Такой логин не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connect.Close( );
        }
    }
}
