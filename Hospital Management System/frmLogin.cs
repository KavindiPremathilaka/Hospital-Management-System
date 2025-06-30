using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hospital_Management_System
{
    public partial class frmLogin : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=Kavindi\SQLEXPRESS01;Initial Catalog=Hospital;Persist Security Info=True;Integrated Security=true;");
        public frmLogin()
        {
            InitializeComponent();
            LoadDataIntoComboBox();

        }

        private void LoadDataIntoComboBox()
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT id FROM PatientRegistration";
                SqlCommand cmd = new SqlCommand(query, connect);

                SqlDataReader reader = cmd.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["id"].ToString());
                    comboBox2.Items.Add(reader["id"].ToString());
                    comboBox7.Items.Add(reader["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }


        private void LoadDataIntoComboBoxMedicalRecords(String patientId)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM MedicalRecords WHERE patientid = " + patientId + "";
                SqlCommand cmd = new SqlCommand(query, connect);

                SqlDataReader reader = cmd.ExecuteReader();

                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["id"].ToString());
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel3.Visible = false;
            flowLayoutPanel4.Visible = false;
            label3.ForeColor = System.Drawing.Color.Red;
            label2.ForeColor = System.Drawing.Color.Black;
            label4.ForeColor = System.Drawing.Color.Black;
            label42.ForeColor = System.Drawing.Color.Black;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel3.Visible = false;
            flowLayoutPanel4.Visible = false;
            label2.ForeColor = System.Drawing.Color.Red;
            label3.ForeColor = System.Drawing.Color.Black;
            label4.ForeColor = System.Drawing.Color.Black;
            label42.ForeColor = System.Drawing.Color.Black;



        }

        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel3.Visible = false;
            label4.ForeColor = System.Drawing.Color.Red;
            label3.ForeColor = System.Drawing.Color.Black;
            label2.ForeColor = System.Drawing.Color.Black;
            label42.ForeColor = System.Drawing.Color.Black;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 sForm = new Form1();
            sForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel4.Visible = false;
            flowLayoutPanel3.Visible = false;
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmss");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {

                    String appointmentNo = GetTimestamp(DateTime.Now);
                    connect.Open();

                    String insertData = "INSERT INTO Appointment  (chooseyourdoctor, appointmentdate,patientid, appointmentid)" +
                        "VALUES(@chooseyourdoctor, @appointmentdate, @patientid, @appointmentid)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {

                        cmd.Parameters.AddWithValue("@chooseyourdoctor", comboBox3.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@appointmentdate", dateTimePicker2.Value.Date.ToString().Trim());
                        cmd.Parameters.AddWithValue("@patientid", comboBox7.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@appointmentid", appointmentNo);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record Added Succesfully. Your Appointment number is: "+ appointmentNo + "", "Save Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                        ClearInputFields();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    String insertData = "INSERT INTO MedicalRecords (reportnumber, reporttype, dateofvisit, diagnosis, condition, patientid)" +
                        "VALUES(@reportnumber, @reporttype, @dateofvisit, @diagnosis, @condition, @patientid)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@reportnumber", int.Parse(txtRnumber.Text.Trim()));
                        cmd.Parameters.AddWithValue("@reporttype", comboBox4.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@dateofvisit", dateTimePicker3.Value.Date.ToString().Trim());
                        cmd.Parameters.AddWithValue("@diagnosis", txtDiagnosis.Text.Trim());
                        cmd.Parameters.AddWithValue("@condition", comboBox5.SelectedItem.ToString());
                        // cmd.Parameters.AddWithValue("@treatment", txtTreatment.Text.Trim());
                        cmd.Parameters.AddWithValue("@patientid", comboBox2.SelectedItem.ToString());





                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record Added Succesfully", "Save Medical Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                        ClearInputFields();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }





        private void button5_Click(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    String insertData = "INSERT INTO PatientRegistration (firstname, lastname, nic, dateofbirth, gender, bloodgroup ,address, email ,mobilephone, homephone ,guardianname ,guardiannic, guardiancontactnumber)" +
                        "VALUES(@firstname, @lastname, @nic, @dateofbirth, @gender, @bloodgroup ,@address, @email ,@mobilephone, @homephone ,@guardianname ,@guardiannic, @guardiancontactnumber)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@lastname", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
                        cmd.Parameters.AddWithValue("@dateofbirth", dateTimePicker1.Value.Date.ToString().Trim());
                        cmd.Parameters.AddWithValue("@gender", radMale.Checked ? "Male" : "Female");
                        cmd.Parameters.AddWithValue("@bloodgroup", txtblood.Text.Trim());
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@mobilephone", int.Parse(txtMobilePhone.Text.Trim()));
                        cmd.Parameters.AddWithValue("@homephone", int.Parse(txtHomePhone.Text.Trim()));
                        cmd.Parameters.AddWithValue("@guardianname", txtGname.Text.Trim());
                        cmd.Parameters.AddWithValue("@guardiannic", txtGNIC.Text.Trim());
                        cmd.Parameters.AddWithValue("@guardiancontactnumber", int.Parse(txtContactNumber.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record Added Succesfully", "Register Patient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataIntoComboBox();
                        BindData();
                        ClearInputFields();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }

        }

        void BindData()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM PatientRegistration", connect);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dc = new DataTable();
            da.Fill(dc);
        }

        private void ClearInputFields()
        {

            comboBox.Text = "";

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtNIC.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            //gender.Text = "";
            txtblood.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobilePhone.Text = "";
            txtHomePhone.Text = "";
            txtGname.Text = "";
            txtGNIC.Text = "";
            txtContactNumber.Text = "";
            radMale.Checked = false;
            radFemale.Checked = false;

            txtFirstName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();


                    if (comboBox.SelectedItem != null)
                    {
                        string selectedRegNo = comboBox.SelectedItem.ToString();

                        string updateQuery = "UPDATE PatientRegistration SET " +
                            "firstname = @firstname, " +
                            "lastname = @lastname, " +
                            "nic = @nic, " +
                            "dateofbirth = @dateofbirth, " +
                            "gender = @gender, " +
                            "bloodgroup = @bloodgroup, " +
                            "address = @address, " +
                            "email = @email, " +
                            "mobilephone = @mobilephone, " +
                            "homephone = @homephone, " +
                            "guardianname = @guardianname, " +
                            "guardiannic = @guardiannic, " +
                            "guardiancontactnumber = @guardiancontactnumber ";
                        //"WHERE RegNo = @RegNo";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                        {
                            cmd.Parameters.AddWithValue("@RegNo", selectedRegNo);
                            cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@lastname", txtLastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
                            cmd.Parameters.AddWithValue("@dateofbirth", dateTimePicker1.Value.Date.ToString().Trim());
                            cmd.Parameters.AddWithValue("@gender", radMale.Checked ? "Male" : "Female"); 
                            cmd.Parameters.AddWithValue("@bloodgroup", txtblood.Text.Trim());
                            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@mobilephone", int.Parse(txtMobilePhone.Text.Trim()));
                            cmd.Parameters.AddWithValue("@homephone", int.Parse(txtHomePhone.Text.Trim()));
                            cmd.Parameters.AddWithValue("@guardianname", txtGname.Text.Trim());
                            cmd.Parameters.AddWithValue("@guardiannic", txtGNIC.Text.Trim());
                            cmd.Parameters.AddWithValue("@guardiancontactnumber", int.Parse(txtContactNumber.Text.Trim()));



                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record Updated Successfully", "Update Patient", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                DisplayDetails(selectedRegNo);


                                BindData();
                            }
                            else
                            {
                                MessageBox.Show("No records updated", "Update Patient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying details: " + ex.Message);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }

        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
            if (radMale.Checked)
            {
                radMale.Checked = true;
            }
        }

        private void radFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radFemale.Checked)
            {
                radFemale.Checked = true;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegNo = comboBox.SelectedItem.ToString();
            DisplayDetails(selectedRegNo);
        }

        private void DisplayDetails(string regNo)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM PatientRegistration WHERE id = @RegNo";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@RegNo", regNo);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    txtFirstName.Text = reader["firstname"].ToString();
                    txtLastName.Text = reader["lastname"].ToString();
                    txtNIC.Text = reader["nic"].ToString();
                    dateTimePicker1.Text = reader["dateofbirth"].ToString();
                    txtblood.Text = reader["bloodgroup"].ToString();
                    txtAddress.Text = reader["address"].ToString();
                    txtEmail.Text = reader["email"].ToString();
                    txtMobilePhone.Text = reader["mobilephone"].ToString();
                    txtHomePhone.Text = reader["homephone"].ToString();
                    txtGname.Text = reader["guardianname"].ToString();
                    txtGNIC.Text = reader["guardiannic"].ToString();
                    txtContactNumber.Text = reader["guardiancontactnumber"].ToString();

                    string genderValue = reader["gender"].ToString();
                    radMale.Checked = (genderValue == "Male");
                    radFemale.Checked = (genderValue == "Female");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void DisplayDetailsAppointment(string regNo)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM PatientRegistration WHERE id = @RegNo";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@RegNo", regNo);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtFirstName.Text = reader["firstname"].ToString();
                    txtLastName.Text = reader["lastname"].ToString();
                    txtNIC.Text = reader["nic"].ToString();
                    dateTimePicker1.Text = reader["dateofbirth"].ToString();
                    txtAddress.Text = reader["address"].ToString();
                    txtEmail.Text = reader["email"].ToString();
                    txtMobilePhone.Text = reader["mobilephone"].ToString();
                    txtHomePhone.Text = reader["homephone"].ToString();
                    txtGname.Text = reader["guardianname"].ToString();
                    txtGNIC.Text = reader["guardiannic"].ToString();
                    txtContactNumber.Text = reader["guardiancontactnumber"].ToString();

                    string genderValue = reader["gender"].ToString();
                    radMale.Checked = (genderValue == "Male");
                    radFemale.Checked = (genderValue == "Female");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            {
                txtFirstName.Clear();
                txtLastName.Clear();
                txtNIC.Clear();
                dateTimePicker1.Value = DateTime.Now;
                //gender.Text = "";
                txtblood.Clear();
                txtAddress.Clear();
                txtEmail.Clear();
                txtMobilePhone.Clear();
                txtHomePhone.Clear();
                txtGname.Clear();
                txtGNIC.Clear();
                txtContactNumber.Clear();
                radMale.Checked = false;
                radFemale.Checked = false;
            }

            txtFirstName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();


                    if (comboBox.SelectedItem != null)
                    {
                        string selectedRegNo = comboBox.SelectedItem.ToString();
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {

                            string deleteQuery = "DELETE FROM PatientRegistration WHERE id = @RegNo";

                            using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                            {
                                cmd.Parameters.AddWithValue("@RegNo", selectedRegNo);

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record Deleted Successfully", "Delete Patient", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoadDataIntoComboBox();
                                    ClearInputFields();


                                    BindData();
                                }
                                else
                                {
                                    MessageBox.Show("No records deleted", "Delete Patient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegNo = comboBox2.SelectedItem.ToString();
            DisplayDetailsAppointment(selectedRegNo);
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedRegNo = comboBox2.SelectedItem.ToString();
            DisplayDetailsMedicalRecords(selectedRegNo);
        }

        private void DisplayDetailsMedicalRecords(string regNo)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM PatientRegistration WHERE id = @RegNo";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@RegNo", regNo);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //cmd.Parameters.AddWithValue("@name", txtMname.Text.Trim());
                    //cmd.Parameters.AddWithValue("@contactnumber", int.Parse(txtMnumber.Text.Trim()));

                    txtMname.Text = reader["firstname"].ToString();
                    txtMnumber.Text = reader["mobilephone"].ToString();

                    //cmd.Parameters.AddWithValue("@gender", gender.Text.Trim());

                    string genderValue = reader["gender"].ToString();
                    radioMale.Checked = (genderValue == "Male");
                    radioFemale.Checked = (genderValue == "Female");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }


        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPatientDetails_Click(object sender, EventArgs e)
        {
            flowLayoutPanel3.Visible = true;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel4.Visible = false;
            label3.ForeColor = System.Drawing.Color.Black;
            label2.ForeColor = System.Drawing.Color.Black;
            label4.ForeColor = System.Drawing.Color.Black;
            label42.ForeColor = System.Drawing.Color.Red;
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegNo = comboBox7.SelectedItem.ToString();
            DisplayDetailsDAppointment(selectedRegNo);
        }

        private void DisplayDetailsDAppointment(string regNo)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM PatientRegistration WHERE id = @RegNo";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@RegNo", regNo);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //cmd.Parameters.AddWithValue("@name", txtMname.Text.Trim());
                    //cmd.Parameters.AddWithValue("@contactnumber", int.Parse(txtMnumber.Text.Trim()));

                    txtDname.Text = reader["firstname"].ToString();
                    txtDnumber.Text = reader["mobilephone"].ToString();

                    //cmd.Parameters.AddWithValue("@gender", gender.Text.Trim());

                    string genderValue = reader["gender"].ToString();
                    radioDmale.Checked = (genderValue == "Male");
                    radioDfemale.Checked = (genderValue == "Female");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }


        }

        private void DisplayDetailsMedicalRecord(string medicalRecordId)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM MedicalRecords WHERE id = @MedicalRecordId";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@MedicalRecordId", medicalRecordId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //cmd.Parameters.AddWithValue("@name", txtMname.Text.Trim());
                    //cmd.Parameters.AddWithValue("@contactnumber", int.Parse(txtMnumber.Text.Trim()));

                    txtAtype.Text = reader["reporttype"].ToString();
                    txtAdate.Text = reader["dateofvisit"].ToString();
                    txtAdiagnosis.Text = reader["diagnosis"].ToString();
                    txtAcondition.Text = reader["condition"].ToString();
                    //cmd.Parameters.AddWithValue("@gender", gender.Text.Trim());

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string selectedRegNo = txtAID.Text.ToString();
            DisplayDetailsPDetails(selectedRegNo);
        }

        private void DisplayDetailsPDetails(string regNo)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                }

                string query = "SELECT * FROM Appointment WHERE appointmentid = @appointmentid";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@appointmentid", regNo);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //cmd.Parameters.AddWithValue("@name", txtMname.Text.Trim());
                    //cmd.Parameters.AddWithValue("@contactnumber", int.Parse(txtMnumber.Text.Trim()));
                    String patientId = reader["patientid"].ToString(); ;

                    txtAPID.Text = patientId;
                    //txtDnumber.Text = reader["mobilephone"].ToString();

                    //cmd.Parameters.AddWithValue("@gender", gender.Text.Trim());

                    //string genderValue = reader["gender"].ToString();
                    // radioDmale.Checked = (genderValue == "Male");
                    // radioDfemale.Checked = (genderValue == "Female");

                    reader.Close();

                    string query1 = "SELECT * FROM PatientRegistration WHERE id = " + patientId + "";
                    SqlCommand cmd1 = new SqlCommand(query1, connect);

                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.Read())
                    {
                        txtAname.Text = reader1["firstname"].ToString();
                        txtAnumber.Text = reader1["mobilephone"].ToString();
                        string genderValue = reader1["gender"].ToString();
                        radioAmale.Checked = (genderValue == "Male");
                        radioAfemale.Checked = (genderValue == "Female");


                        reader1.Close();
                       LoadDataIntoComboBoxMedicalRecords(patientId);
                    }

                    // medical records
                }   
                }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying details: " + ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }


        }
        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    String insertData = "INSERT INTO Details (appointmentid, patientid, medicines, reportid)" +
                        "VALUES(@appointmentid, @patientid, @medicines, @reportid)";

                    using (SqlCommand cmd = new SqlCommand(insertData, connect))
                    {
                        cmd.Parameters.AddWithValue("@appointmentid", txtAID.Text.Trim());
                        cmd.Parameters.AddWithValue("@patientid", txtAPID.Text.Trim());
                        cmd.Parameters.AddWithValue("@medicines", txtMedicines.Text.Trim());
                        cmd.Parameters.AddWithValue("@reportid", comboBox1.SelectedItem.ToString());





                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record Added Succesfully", "Save Medical Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                        ClearInputFields();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connection DB: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedMedicalRecordId = comboBox1.SelectedItem.ToString();
            DisplayDetailsMedicalRecord(selectedMedicalRecordId);
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioAmale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtAID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
           
        }

        private void txtAcondition_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
