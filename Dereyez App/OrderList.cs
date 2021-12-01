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
    public partial class OrderListForm : Form
    {
        string myConnection = "datasource=localhost; port=3306; username=root; password=;database=dereyez";
        public OrderListForm()
        {
            InitializeComponent();
            LoadTable();
        }
        private void LoadTable()
        {
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT orders.orderId, users.username, orders.dueDate, orders.paymentType FROM orders INNER JOIN users ON orders.userId=users.userId WHERE orders.paymentStatus=1 AND orders.finish=0;", myConn);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                Table.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Finish_Click(object sender, EventArgs e)
        {
            string Query = "update dereyez.orders set finish='1' where orderId='" + this.orderId.Text + "';";
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, myConn);
            MySqlDataReader myReader;

            try
            {
                myConn.Open();
                myReader = cmdDatabase.ExecuteReader();
                MessageBox.Show("Order Finished");
                myConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadTable();
        }

        private void Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.Table.Rows[e.RowIndex];

            orderId.Text = row.Cells["orderId"].Value.ToString();
            Username.Text = row.Cells["username"].Value.ToString();
            Date.Text = row.Cells["dueDate"].Value.ToString();
            PaymentType.Text = row.Cells["paymentType"].Value.ToString();
        }
    }
}
