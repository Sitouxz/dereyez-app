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
        public int itemId;
        public int userId;
        public ItemDetails()
        {
            InitializeComponent();
        }

        private void Quantity_TextChange(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(Quantity.Text, out parsedValue))
            {
                Quantity.Text = "";
                MessageBox.Show("This is a Number only field");
                return;
            }
        }

        private void Order_Click(object sender, EventArgs e)
        {
            DateTime timeCreated = DateTime.Now;
            string date = timeCreated.ToString("yyyy-MM-dd H:mm:ss");
            string Query = "insert into dereyez.orders (orderId,dueDate,brief,quantity,timeCreated,paymentType,paymentStatus,finish,itemId,userId) values('','" + this.DueDate.Text + "','" + this.Brief.Text + "','" + this.Quantity.Text + "','" + timeCreated + "','','0','0','" + itemId + "','" + userId + "');";
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, myConn);
            MySqlDataReader myReader;

            try
            {
                myConn.Open();
                myReader = cmdDatabase.ExecuteReader();
                MessageBox.Show("Order Created");
                myConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
