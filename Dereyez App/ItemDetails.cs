using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Dereyez_App
{
    public partial class ItemDetails : Form
    {
        string myConnection = "datasource=localhost; port=3306; username=root; password=";
        public ItemDetails()
        {
            InitializeComponent();
        }

        private void ItemDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
