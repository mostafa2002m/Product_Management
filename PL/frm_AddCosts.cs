using System;
using System.Windows.Forms;
using System.Data;
using PRODUCTS_MANAGEMENT.BL;
using System.Drawing;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_AddCosts : Form
    {
        string ch;
        public frm_AddCosts(string _ch)
        {
            InitializeComponent();
            ch = _ch;
            cls_Controls.BindCombo(ref cboCost, "stp_SelectAll_Expense", "expense_Name", "Expense_Id");
            if (ch == "a")
            {
                groupBox1.Enabled = true;
                btnAdd.Text = "Add";
                this.Text = "Add Expense Type";
            }
            else if (ch == "u")
            {
                btnAdd.Text = "Update";
                this.Text = "Update Expense Type";
                btnAdd.Enabled = false;
                groupBox1.Enabled = true;
                DataTable dt = cls_Cost.stp_SelectAll_CostsById(FrmCustomerList.Id);
                foreach (DataRow dr in dt.Rows)
                {
                  
                    txtname.Text = dr["Cost_Name"].ToString();
                    dtpOrder_Date.Value =Convert.ToDateTime(dr["Cost_Date"].ToString());
                    txtdesc.Text=dr["Cost_Desc"].ToString();
                    txtamount.Text = dr["Cost_Amount"].ToString();
                }
            }

            else if (ch == "d")
            {
                btnAdd.Text = "Delete";
                this.Text = "Delete Expense Type";
                btnAdd.Enabled = false;
                groupBox1.Enabled = true;
                DataTable dt = cls_Cost.stp_SelectAll_CostsById(FrmCustomerList.Id);
                foreach (DataRow dr in dt.Rows)
                {

                    txtname.Text = dr["Cost_Name"].ToString();
                    dtpOrder_Date.Value = Convert.ToDateTime(dr["Cost_Date"].ToString());
                    txtdesc.Text = dr["Cost_Desc"].ToString();
                    txtamount.Text = dr["Cost_Amount"].ToString();
                }
            }

        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
             
            if (txtname.Text == string.Empty || txtdesc.Text == string.Empty || txtamount.Text == string.Empty)
            {
                MessageBox.Show(@"Please check data");
                return;
            }


                if (ch=="a")
                {
                    int lastcost = Convert.ToInt32(cls_Cost.stp_SelectLast_Cost_Id().Rows[0][0]);
                    cls_Cost.stp_ManipulateCost(ch,lastcost, txtname.Text, dtpOrder_Date.Value, txtdesc.Text, Convert.ToDecimal(txtamount.Text));

                    Add_Payment_Cash(txtdesc.Text);
                    MessageBox.Show(@"Added Successfuly ");
                }

                else if (ch=="u")
                {

                    int lastcost = Convert.ToInt32(cls_Cost.stp_SelectLast_Cost_Id().Rows[0][0]);
                    cls_Cost.stp_ManipulateCost(ch, lastcost, txtname.Text, dtpOrder_Date.Value, txtdesc.Text, Convert.ToDecimal(txtamount.Text));

                    Add_Payment_Cash(txtdesc.Text);
                    MessageBox.Show(@"Updated Successfully ");
                }
                else if (ch=="d")
                {
                    int lastcost = Convert.ToInt32(cls_Cost.stp_SelectLast_Cost_Id().Rows[0][0]);
                    cls_Cost.stp_ManipulateCost(ch, lastcost, txtname.Text, dtpOrder_Date.Value, txtdesc.Text, Convert.ToDecimal(txtamount.Text));

                    Add_Payment_Cash(txtdesc.Text);
                    MessageBox.Show(@"Deleted Successfully ");
                }
               
                groupBox1.Enabled = true;
                txtname.Text = string.Empty;
                txtdesc.Text = string.Empty;
                txtamount.Text = string.Empty;
                cboCost.SelectedIndex = -1;
           

            }
            catch (Exception ex)
            {
                MessageBox.Show("add Failure", ex.Message);
            }

        }

        private void Add_Payment_Cash(string paymethod)
        {
            try
            {
                DataTable dt = cls_CashOut.stp_SelectLast_CashOut_Id();
                int cashid = Convert.ToInt32(dt.Rows[0][0].ToString());
                cls_CashOut.Add_CashOut(cashid, Convert.ToDateTime(dtpOrder_Date.Value), Convert.ToDecimal(txtamount.Text), paymethod, 100);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cboCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtname.Text = cboCost.Text;
            groupBox1.Enabled = true;
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
