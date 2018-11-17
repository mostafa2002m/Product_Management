using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PRODUCTS_MANAGEMENT.BL;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_AddExpense : Form
    {
        string ch;
        public frm_AddExpense(string _ch)
        {
            InitializeComponent();
            ch = _ch;
            btnSave.Enabled = false;
            groupBox1.Enabled = false;
            if (ch == "a")
            {
                groupBox1.Enabled = true;
                btnAddNew.Text = "Add";
                this.Text = "Add New Expense";
            }
            else if (ch == "u")
            {
                btnAddNew.Text = "Update";
                this.Text = "Update Expense";
                btnAddNew.Enabled = false;
                btnSave.Enabled = true;
                groupBox1.Enabled = true;
                DataTable dt = cls_Cost.stp_SelectAll_ExpenseById(frm_ExpenseManagement.ID);
                foreach (DataRow dr in dt.Rows)
                {
                    txtExpenseName.Text = dr["Expense_Name"].ToString();
                    txtDescription.Text= dr["Description"].ToString();
                }
            }

            else if (ch == "d")
            {
                btnAddNew.Text = "Delete";
                this.Text = "Delete Expense";
                btnAddNew.Enabled = false ;
                btnSave.Enabled = true;
                groupBox1.Enabled = true;
                DataTable dt = cls_Cost.stp_SelectAll_ExpenseById(frm_ExpenseManagement.ID);
                foreach (DataRow dr in dt.Rows)
                {


                    txtExpenseName.Text = dr["Expense_Name"].ToString();
                    txtDescription.Text = dr["Description"].ToString();

                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            txtExpenseName.Clear();
            txtDescription.Clear();
            groupBox1.Enabled = true;
            txtExpenseName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtExpenseName.Text == string.Empty || txtDescription.Text == string.Empty)
                {
                    MessageBox.Show(@"Check Data");
                    return;
                }


                if (ch == "a")
                {

                    cls_Cost.stp_ManipulateExpense(ch, txtExpenseName.Text, txtDescription.Text);


                    MessageBox.Show(@"Added Successfully ");
                    
                }

                else if (ch == "u")
                {

                    cls_Cost.stp_ManipulateExpense(ch, txtExpenseName.Text, txtDescription.Text);
                    MessageBox.Show(@"Updated Successfully ");
                }
                else if (ch == "d")
                {
                    cls_Cost.stp_ManipulateExpense(ch, txtExpenseName.Text, txtDescription.Text);
                    MessageBox.Show(@"Deleted Successfully ");
                }

                groupBox1.Enabled = false;
                txtExpenseName.Text = string.Empty;
                txtDescription.Text = string.Empty;
                btnAddNew.Enabled = true;
                btnSave.Enabled = false;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Failed", ex.Message);
            }
        }
    }
    }
