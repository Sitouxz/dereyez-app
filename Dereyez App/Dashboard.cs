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
    public partial class Dashboard : Form
    {
        string myConnection = "datasource=localhost; port=3306; username=root; password=";
        public int connectedUserId;
        public Dashboard()
        {
            InitializeComponent();
            LoadRecentOrders();
            LoadRecentUser();
            LoadNewUser();
        }

        private void LoadRecentOrders()
        {
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand("select * from dereyez.orderdetails;", myConn);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dereyez.orderdetails", myConn);
            myConn.Open();
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            myConn.Close();
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                Recent_Order.DataSource = bSource;
                sda.Update(dbdataset);
                Recent_Order.Columns["timeCreated"].DefaultCellStyle.Format = "yyyy-MM-dd H:mm:ss";
                Total_RecentOrder.Text = Convert.ToString(count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadRecentUser()
        {
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand("select * from dereyez.users;", myConn);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dereyez.users", myConn);
            myConn.Open();
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            myConn.Close();
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                Recent_User.DataSource = bSource;
                sda.Update(dbdataset);
                Recent_User.Columns["lastSeen"].DefaultCellStyle.Format = "yyyy-MM-dd H:mm:ss";
                Total_RecentUser.Text = Convert.ToString(count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadNewUser()
        {

            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand("select * from dereyez.users;", myConn);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM dereyez.users", myConn);
            myConn.Open();
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            myConn.Close();
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                New_User.DataSource = bSource;
                sda.Update(dbdataset);
                New_User.Columns["dateCreated"].DefaultCellStyle.Format = "yyyy-MM-dd H:mm:ss";
                Total_NewUser.Text = Convert.ToString(count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
