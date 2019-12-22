using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;



namespace SH
{
    public partial class Form1 : Form
    {

        //-------------------------------------------------------------------------------
        /*
Constants in Windows API
0x84 = WM_NCHITTEST - Mouse Capture Test
0x1 = HTCLIENT - Application Client Area
0x2 = HTCAPTION - Application Title Bar

This function intercepts all the commands sent to the application. 
It checks to see of the message is a mouse click in the application. 
It passes the action to the base action by default. It reassigns 
the action to the title bar if it occured in the client area
to allow the drag and move behavior.
*/

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        //source: https://stackoverflow.com/questions/23966253/moving-form-without-title-bar




        //-------------------------------------------------------------------------------





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                connection = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;database=shadiDB");
                



                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    label1.ForeColor = Color.Green;
                    label1.Text = "Connected";
                }
                else
                {
                    label1.ForeColor = Color.Red;
                    label1.Text = "Not Connected";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            textBox2.PasswordChar = '*';
            textBox2.ForeColor = Color.White;
            string password = textBox2.Text;
        }

        
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            string username = textBox1.Text;
        }

        MySqlConnection connection;
        
        private void button1_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username.Length != 0 || password.Length != 0)
            {
                MySqlCommand sqlcmd = new MySqlCommand("select * from users where username=@username AND PASSWORD=@password", connection);
                sqlcmd.Parameters.AddWithValue("@username", username);
                sqlcmd.Parameters.AddWithValue("@password", password);
                MySqlDataReader mysqlrd = sqlcmd.ExecuteReader();
                bool access = false;
                while (mysqlrd.Read())
                {
                    access = true;

                   

                    break;
                }
                if (access)
                {
                    
                    this.Hide();
                    MainMenu aMainmenu = new MainMenu();
                    aMainmenu.ShowDialog();
                    
                    
                }
                else { MessageBox.Show("wrong username/password"); }
                mysqlrd.Close();

            }
            else
                MessageBox.Show("ERROR");
            






            //DataTable table = new DataTable();
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            //MySqlCommand command = new MySqlCommand();






            
            //Form1 aForm = new Form1();
            //aForm.Close();

            //this.Hide();
            //MainMenu aMainmenu = new MainMenu();
            //aMainmenu.ShowDialog();
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null) tb.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null) tb.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
