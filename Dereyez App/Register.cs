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
    public partial class RegisterForm : Form
    {
        string myConnection = "datasource=localhost;port=3306;username=root;password=";
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            DateTime dateCreated = DateTime.Now;
            string date = dateCreated.ToString("yyyy-MM-dd H:mm:ss");

            string Query = "insert into dereyez.users (userId,username,password,phone,address,dateCreated,lastSeen,userLevel) values('','" + this.Username.Text + "','" + this.Password.Text + "','" + this.Phone.Text + "','" + this.Address.Text + "','" + date + "','','customer');";
            MySqlConnection myConn = new MySqlConnection(myConnection);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, myConn);
            MySqlDataReader myReader;

            try
            {
                myConn.Open();
                myReader = cmdDatabase.ExecuteReader();
                this.Hide();
                LoginForm ins = new LoginForm();
                ins.Closed += (s, args) => this.Close();
                ins.Show();
                myConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm ins = new LoginForm();
            ins.Closed += (s, args) => this.Close();
            ins.Show();
        }

        private void Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Register.PerformClick();
            }
        }
    }
}
