using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Management_System
{
    public partial class Form1 : Form
    {

        SqlConnection connect1 = new SqlConnection(@"Data Source=Kavindi\SQLEXPRESS01;Initial Catalog=Hospital;Persist Security Info=True;Integrated Security=true;");

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) 
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                txtUsername.Clear();
                txtPassword.Clear();
            }

            txtUsername.Focus();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (connect1.State != ConnectionState.Open)
            {
                try
                {
                    connect1.Open();
                    string selectData = "SELECT * FROM Employee WHERE username = @username COLLATE SQL_Latin1_General_CP1_CS_AS AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect1))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count >= 1)
                        {
                            if (IsPasswordComplex(txtPassword.Text))
                            {
                                frmLogin sForm = new frmLogin();
                                sForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Login credentials, please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Login credentials, please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect1.Close();
                }
                bool IsPasswordComplex(string password)
                {

                    return password.Any(char.IsUpper) && password.Any(char.IsLower);
                }
            }

            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
