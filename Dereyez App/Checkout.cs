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
    public partial class CheckoutForm : Form
    {
        string myConnection = "datasource=localhost; port=3306; username=root; password=;database=dereyez";
        public CheckoutForm()
        {
            InitializeComponent();
            LoadTable();
        }
        private void LoadTable()
        {
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT orders.orderId, orders.brief ,(items.price * orders.quantity) AS total FROM orders INNER JOIN items ON orders.itemId=items.itemId WHERE orders.paymentStatus=0;", myConn);
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

        private void Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.Table.Rows[e.RowIndex];

            orderId.Text = row.Cells["orderId"].Value.ToString();
            Price.Text = row.Cells["total"].Value.ToString();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            string Query = "update dereyez.orders set paymentType= '" + this.PaymentType.Text + "',paymentStatus='1' where orderId='" + this.orderId.Text + "';";
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, myConn);
            MySqlDataReader myReader;

            try
            {
                myConn.Open();
                myReader = cmdDatabase.ExecuteReader();
                MessageBox.Show("Payment Success");
                myConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadTable();
        }
    }
}
