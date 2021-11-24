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
    public partial class LoginForm : Form
    {
        string myConnection = "datasource=localhost; port=3306; username=root; password=";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime lastSeen = DateTime.Now;
                string date = lastSeen.ToString("yyyy-MM-dd H:mm:ss");
                MySqlConnection myConn = new MySqlConnection(myConnection);
                MySqlCommand SelectCommand = new MySqlCommand("select * from dereyez.users where username= '" + this.Username.Text + "' and password='" + this.Password.Text + "'; update dereyez.users set lastSeen = '" + date + "' where username = '" + this.Username.Text + "' and password = '" + this.Password.Text + "'", myConn);
                MySqlDataReader myReader;
                myConn.Open();

                myReader = SelectCommand.ExecuteReader();
                int count = 0;
                bool isAdmin = false;
                while (myReader.Read())
                {
                    count = count + 1;
                    if (myReader.GetString("userLevel") == "admin")
                    {
                        isAdmin = true;
                    }
                }
                if (isAdmin == true)
                {
                    Main ins = new Main();
                    ins.connectedUserId = Convert.ToInt32(myReader.GetString("userId"));
                    ins.
                    ins.Closed += (s, args) => this.Close();
                    ins.Show();

                }
                else if (count == 1)
                {
                    this.Hide();
                    Main ins = new Main();
                    ins.Closed += (s, args) => this.Close();
                    ins.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
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

        private void Register_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm ins = new RegisterForm();
            ins.Closed += (s, args) => this.Close();
            ins.Show();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login.PerformClick();
            }
        }
    }
}
