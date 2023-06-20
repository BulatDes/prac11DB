using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkDB_Despizhek
{
    public partial class UserApp :Form
    {
        int ID;
        public UserApp (int id)
        {
            InitializeComponent( );
            ID = id;
        }

        private void UserApp_FormClosing (object sender, FormClosingEventArgs e)
        {
            Application.Exit( );
        }
    }
}
