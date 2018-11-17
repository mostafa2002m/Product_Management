using System;
using System.Data;
using System.Windows.Forms;
using PRODUCTS_MANAGEMENT.BL;
using System.Drawing;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class FrmAddCustomer : Form
    {
        string ch;
        public FrmAddCustomer(string _ch)
        {
            InitializeComponent();
            ch = _ch;
            if (ch=="a")
            {
                groupBox1.Enabled = false;
                btnAddNew.Text = "Add";
                this.Text = "New Customer";
            }
            else if (ch=="u")
            {
                btnAddNew.Text = "Update";
                this.Text = "Update Customer";
                btnAddNew.Enabled = false;
                groupBox1.Enabled = true;
                DataTable dt = cls_Customer.stp_SelectCustomersById(FrmCustomerList.Id);
                foreach (DataRow dr in dt.Rows)
                {

                    txtSupId.Text = dr["Cust_Id"].ToString();
                    txtName.Text = dr["Cust_Name"].ToString();
                    txtAddress.Text = dr["Cust_Address"].ToString();
                    txtPhone.Text = dr["Cust_Phone"].ToString();
                }
            }
            else if (ch=="d")
            {
                btnAddNew.Text = "Delete";
                this.Text = "Delete Customer";
                btnAddNew.Enabled = false;
                groupBox1.Enabled = true;
                DataTable dt = cls_Customer.stp_SelectCustomersById(FrmCustomerList.Id);
                foreach (DataRow dr in dt.Rows)
                {

                    txtSupId.Text = dr["Cust_Id"].ToString();
                    txtName.Text = dr["Cust_Name"].ToString();
                    txtAddress.Text = dr["Cust_Address"].ToString();
                    txtPhone.Text = dr["Cust_Phone"].ToString();
                }
            }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            groupBox1.Enabled = true;
            txtAddress.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtSupId.Clear();
            txtEmail.Clear();
            txtName.Focus();
            lblmassage.Text = "";
            txtSupId.Text = cls_Customer.stp_SelectLast_Customer_Id().Rows[0][0].ToString();
        }

     

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == string.Empty)
                {
                    MessageBox.Show(@"Check Data");
                    txtName.Focus();
                    return;
                }
                if (ch=="a")
                {
                    cls_Customer.stp_ManipulateCustomer(ch,Convert.ToInt32(txtSupId.Text), txtName.Text, txtAddress.Text, txtPhone.Text);
                    MessageBox.Show(@"Added Successfully");
                    txtPhone.Text = txtAddress.Text = txtPhone.Text = string.Empty;
                    DialogResult respnse = MessageBox.Show(@"Add Initial Balance", @"Initial Balance ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respnse == DialogResult.Yes)
                    {
                        //Add_Payment();
                        new frm_Customerpayments("Initial Balance").ShowDialog();
                    }
                }
                else if (ch=="u")
                {
                    cls_Customer.stp_ManipulateCustomer(ch,FrmCustomerList.Id, txtName.Text, txtAddress.Text, txtPhone.Text);
                    MessageBox.Show(@"Updated Successfully");
                    txtName.Text = txtAddress.Text = txtPhone.Text = string.Empty;
                }
               
               
                else if (ch=="d")
                {
                    if (MessageBox.Show(@"Delete Data", @"Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        cls_Customer.stp_ManipulateCustomer(ch,FrmCustomerList.Id, txtName.Text, txtAddress.Text, txtPhone.Text);
                        MessageBox.Show(@"deleted Successfully");
                        txtName.Text = txtAddress.Text = txtPhone.Text = string.Empty;
                    }
                }
                txtName.Text = txtAddress.Text = txtPhone.Text = txtEmail.Text = string.Empty;
                btnSave.Enabled = false;
                btnAddNew.Enabled = true;
                groupBox1.Enabled = false;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void ChangeColour(object sender, EventArgs e)
        {
            Button bColour = (Button)sender;
            bColour.BackColor = Color.LightSteelBlue;
        }

        private void ColourChange(object sender, EventArgs e)
        {
            Button bColour = (Button)sender;
            bColour.BackColor = Color.FromKnownColor(KnownColor.Control);
        }
    }
}
