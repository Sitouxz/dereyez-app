using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Dereyez_App
{
    public partial class Main : Form
    {
        string myConnection = "datasource=localhost; port=3306; username=root; password=";
        public int connectedUserId;
        public Main()
        {
            InitializeComponent();
            LoadTable();
        }

        private void LoadTable()
        {
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand("select * from dereyez.items;", myConn);
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

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ins = new LoginForm();
            ins.Closed += (s, args) => this.Close();
            ins.Show();
        }

        public Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Right;
            Panel_ChildForm.Controls.Add(childForm);
            Panel_ChildForm.Dock = DockStyle.Right;
            Panel_ChildForm.Controls.Add(childForm);
            Panel_ChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            if (Role.Text == "admin")
            {
                openChildForm(new Dashboard());
            }
            else if (activeForm != null)
            {
                activeForm.Close();
            }

        }

        private void Orders_Click(object sender, EventArgs e)
        {
            if (Orders.Text == "Order List")
            {
                openChildForm(new OrderListForm());
            }
            else
            {
                openChildForm(new OrdersForm());
            }
        }

        private void Checkout_Click(object sender, EventArgs e)
        {
            if (Checkout.Text == "Order History")
            {
                openChildForm(new OrderHistoryForm());
            }
            else
            {
                openChildForm(new CheckoutForm());

            }
        }

        string file;
        private void Profile_Picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png;)|*.jpg; *.jpeg; *.gif; *.bmp; *.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                file = open.FileName;
                Profile_Picture.Image = new Bitmap(open.FileName);
                File.Copy(file, Path.Combine(@"C:\Users\user\source\repos\Dereyez App\Dereyez App\profile-pictures\", Path.GetFileName(file)), true);

                string Query = "update dereyez.users set picture= '" + Convert.ToString(Path.GetFileName(file)) + "' where userId = '" + connectedUserId + "';";
                MySqlConnection myConn = new MySqlConnection(myConnection);
                MySqlCommand cmdDatabase = new MySqlCommand(Query, myConn);
                MySqlDataReader myReader;

                try
                {
                    myConn.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    myConn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Role.Text == "admin")
            {
                openChildForm(new Dashboard());
            }
        }

        string desc;
        private void Select_Click(object sender, EventArgs e)
        {
            ItemDetails sendItemId = new ItemDetails();
            sendItemId.Item_ID.Text = Selected_ID.Text;
            sendItemId.Item_Name.Text = Selected_Name.Text;
            sendItemId.Item_Desc.Text = desc;
            if (activeForm != null)
                activeForm.Close();
            activeForm = sendItemId;
            sendItemId.TopLevel = false;
            sendItemId.FormBorderStyle = FormBorderStyle.None;
            sendItemId.Dock = DockStyle.Right;
            Panel_ChildForm.Controls.Add(sendItemId);
            Panel_ChildForm.Dock = DockStyle.Right;
            Panel_ChildForm.Controls.Add(sendItemId);
            Panel_ChildForm.Tag = sendItemId;
            sendItemId.BringToFront();
            sendItemId.Show();
        }

        private void Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.Table.Rows[e.RowIndex];

            Selected_ID.Text = row.Cells["itemId"].Value.ToString();
            Selected_Name.Text = row.Cells["name"].Value.ToString();
            desc = row.Cells["description"].Value.ToString();
        }
    }
}
