﻿using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PRODUCTS_MANAGEMENT.BL;


namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class FrmROrder : Form
    {

     //   decimal _total;
        decimal _netValue = 0;
        decimal _total = 0;
        char _d = char.Parse(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
        public FrmROrder()
        {
            InitializeComponent();
            ClearData();
            LoadCustomer_Combo();
            txtOrder_SaleMan.Text = Program.User_FullName;
        }

        void ClearData()
        {

            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
            }
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
            }
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
            }


            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            //picBox_Cust.Image = null;
            dtpOrder_Date.ResetText();

            ClearBoxes();
            dgvDetails.Rows.Clear();
            dgvDetails.Refresh();


            btnAddNew.Enabled = true;
            btnSaveOrder.Enabled = false;
            btnUndoOrder.Enabled = false;

        }

        void ClearBoxes()
        {
            foreach (Control item in groupBox3.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
            }
           
            btnBrowse.Focus();
        }
        void ClearDownBoxes()
        {
            foreach (Control item in groupBox4.Controls)
            {
                if (item is TextBox)
                    item.Text = "0";
            }
        }
        void DetailsRefresh()
        {
            txtItems.Text = dgvDetails.Rows.Count.ToString();
        }
        

        private void LoadCustomer_Combo()
        {
            cls_Controls.BindCombo(ref cboCust_Name, "stp_SelectAll_Customers", "Cust_Name", "Cust_Id");
            cboCust_Name.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (btnSaveOrder.Enabled)
            {
                if (MessageBox.Show(@"Want To Exit?", @"Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    this.Close();
                else
                    return;
            }
            this.Close();
        }
        /* */
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearBoxes();
            txtOrder_Id.Text = cls_ROrder.stp_SelectLast_ROrder_Id().Rows[0][0].ToString();
            btnAddNew.Enabled = false;
            btnSaveOrder.Enabled = true;
            btnUndoOrder.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ClearBoxes();
                frm_Search frm = new frm_Search("Product");
                frm.ShowDialog();
                if ((bool)(frm.dgvSearch.CurrentRow.Cells[10].Value) == false)
                {


                    if (rdbLarge.Checked)
                    {
                        txtProduct_Id.Text = frm.dgvSearch.CurrentRow.Cells[0].Value.ToString();
                        txtBarcode.Text = frm.dgvSearch.CurrentRow.Cells[1].Value.ToString();
                        txtProduct_Name.Text = frm.dgvSearch.CurrentRow.Cells[2].Value.ToString();
                        txtAvQty.Text = frm.dgvSearch.CurrentRow.Cells[3].Value.ToString();
                        txtLqty.Text = frm.dgvSearch.CurrentRow.Cells[5].Value.ToString();
                        txtPrice.Text = frm.dgvSearch.CurrentRow.Cells[6].Value.ToString();
                        txtSqty.Text = frm.dgvSearch.CurrentRow.Cells[7].Value.ToString();
                        txtLqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[6].Value.ToString();
                        txtSqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[8].Value.ToString();


                    }
                    else
                    {
                        txtProduct_Id.Text = frm.dgvSearch.CurrentRow.Cells[0].Value.ToString();
                        txtBarcode.Text = frm.dgvSearch.CurrentRow.Cells[1].Value.ToString();
                        txtProduct_Name.Text = frm.dgvSearch.CurrentRow.Cells[2].Value.ToString();
                        txtAvQty.Text = frm.dgvSearch.CurrentRow.Cells[3].Value.ToString();
                        txtPrice.Text = frm.dgvSearch.CurrentRow.Cells[8].Value.ToString();
                        txtLqty.Text = frm.dgvSearch.CurrentRow.Cells[5].Value.ToString();
                        txtSqty.Text = frm.dgvSearch.CurrentRow.Cells[7].Value.ToString();
                        txtLqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[6].Value.ToString();
                        txtSqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[8].Value.ToString();


                    }
                    // ------- Tax = 0
                    txtDisc.Text = "0";

                }
                else
                {
                    if (rdbLarge.Checked)
                    {
                        txtProduct_Id.Text = frm.dgvSearch.CurrentRow.Cells[0].Value.ToString();
                        txtBarcode.Text = frm.dgvSearch.CurrentRow.Cells[1].Value.ToString();
                        txtProduct_Name.Text = frm.dgvSearch.CurrentRow.Cells[2].Value.ToString();
                        txtAvQty.Text = frm.dgvSearch.CurrentRow.Cells[3].Value.ToString();
                        txtLqty.Text = frm.dgvSearch.CurrentRow.Cells[5].Value.ToString();
                        txtPrice.Text = frm.dgvSearch.CurrentRow.Cells[6].Value.ToString();
                        txtSqty.Text = frm.dgvSearch.CurrentRow.Cells[7].Value.ToString();
                        txtLqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[6].Value.ToString();
                        txtSqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[8].Value.ToString();


                    }
                    else
                    {
                        txtProduct_Id.Text = frm.dgvSearch.CurrentRow.Cells[0].Value.ToString();
                        txtBarcode.Text = frm.dgvSearch.CurrentRow.Cells[1].Value.ToString();
                        txtProduct_Name.Text = frm.dgvSearch.CurrentRow.Cells[2].Value.ToString();
                        txtAvQty.Text = frm.dgvSearch.CurrentRow.Cells[3].Value.ToString();
                        txtPrice.Text = frm.dgvSearch.CurrentRow.Cells[8].Value.ToString();
                        txtLqty.Text = frm.dgvSearch.CurrentRow.Cells[5].Value.ToString();
                        txtSqty.Text = frm.dgvSearch.CurrentRow.Cells[7].Value.ToString();
                        txtLqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[6].Value.ToString();
                        txtSqtyPrice.Text = frm.dgvSearch.CurrentRow.Cells[8].Value.ToString();


                    }

                    txtDisc.Text = cls_Controls.CalculateTax(Convert.ToDecimal(txtPrice.Text)).ToString();
                }
                // any case do the following :-
                txtqty.Text = "1";
                txtqty.Focus();
                // compute inventory & order balance
                CalculateAmount();
                //استدعاء دالة لاظهار قيم مربعات النصوص اسفل الفاتورة
                Calculate_BalanceQtyInInvoice(txtProduct_Id.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "No Item Selected");
            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtqty.Text != string.Empty)
            {
                txtPrice.Focus();
            }
        }

        private void txtqty_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtqty.Text != "")
            {
                CalculateAmount();
                CalculateNetValue();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPrice.Text != string.Empty)
            {

                btnAdd.Focus();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateAmount();
            CalculateNetValue();
        }
      


            void Show_Status_Boxes()
        {

            // حساب رصيدالمنتج بعد اضافته للفاتورة
            txtAvQty.Text = (Convert.ToInt32(txtAvQty.Text) + Convert.ToInt32(txtCurrent.Text)).ToString();

        }
        //دالة لحساب كمية من منتج معين بالفاتورة الحالية
       //دالة لحساب كمية من منتج معين بالفاتورة الحالية
            private void Calculate_BalanceQtyInInvoice(string p)
            {
                _total = 0;
                decimal realQty = 0;
                for (int i = 0; i < dgvDetails.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgvDetails.Rows[i].Cells[0].Value).ToString() == p)
                    {
                        realQty = Convert.ToDecimal(dgvDetails.Rows[i].Cells[2].Value);
                        _total += realQty;
                        txtCurrent.Text = _total.ToString(CultureInfo.InvariantCulture);
                    }

                }
            }
        //دالة لحساب قيمة الضريبة على منتج معين
        private void CalculateTax(string price)
        {
            decimal tax = Convert.ToDecimal(price) * 5 / 100;
            txtDisc.Text = (Convert.ToDecimal(txtPrice.Text) * 5 / 100).ToString();
        }
       


        private void dgvDetails_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtProduct_Id.Text = this.dgvDetails.CurrentRow.Cells[0].Value.ToString();
                txtProduct_Name.Text = this.dgvDetails.CurrentRow.Cells[1].Value.ToString();
                txtqty.Text = this.dgvDetails.CurrentRow.Cells[2].Value.ToString();
                txtPrice.Text = this.dgvDetails.CurrentRow.Cells[3].Value.ToString();
                txtValue.Text = this.dgvDetails.CurrentRow.Cells[4].Value.ToString();
                txtDisc.Text = this.dgvDetails.CurrentRow.Cells[5].Value.ToString();
                dgvDetails.Rows.RemoveAt(dgvDetails.CurrentRow.Index);
                DetailsRefresh();
                txtqty.Focus();
                txtAvQty.Text = cls_Controls.ShowAvailableQty1(Convert.ToInt32(txtAvQty.Text), Convert.ToInt32(txtCurrent.Text)).ToString();

            }

            catch
            {
                return;
            }

        }

        private void dgvDetails_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // كود مكتوب بـ linq للتجميع من داتا جريد
            txtOrder_Total.Text =
                (from DataGridViewRow row in dgvDetails.Rows
                 where row.Cells[6].FormattedValue.ToString() != string.Empty
                 select Convert.ToDecimal(row.Cells[6].FormattedValue)).Sum().ToString();
            DetailsRefresh();
            decimal balance = cls_Controls.NetBalanceForCustomers((txtCust_Id.Text));
            txtBalance.Text = balance.ToString();
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetails_DoubleClick(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void حذفعنصرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetails.Rows.RemoveAt(dgvDetails.CurrentRow.Index);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            dgvDetails_Click(null, null);
        }

        private void حذفالكلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDetails.Rows.Count - 1; i++)
                {
                    dgvDetails.Rows.RemoveAt(dgvDetails.CurrentRow.Index);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // check values
                if (txtOrder_Id.Text == string.Empty || txtCust_Id.Text == string.Empty || dgvDetails.Rows.Count < 1)
                {
                    MessageBox.Show(@"يجب تسجيل المعلومات المهمة", @"تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #region
                //DataTable InvDet = new DataTable();
                //InvDet.Columns.Add("Product_Id");
                //InvDet.Columns.Add("Qty");
                //InvDet.Columns.Add("Price");
                //InvDet.Columns.Add("Discount");
                //InvDet.Columns.Add("Amount");

                //foreach (DataGridViewRow dr in dgvDetails.Rows)
                //{
                //    InvDet.Rows.Add(dr.Cells[0].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[5].Value, dr.Cells[6].Value);
                //}

                //cls_ROrder.Insert_ROrder(int.Parse(txtOrder_Id.Text), dtpOrder_Date.Value, Convert.ToDecimal(txtOrder_Total.Text), int.Parse(txtCust_Id.Text), txtOrder_SaleMan.Text, InvDet);
                #endregion
                bool Ins = true;
                Insert_ROrder(Ins);
                if (Ins == true)
                {
                    if (rdbCash.Checked)
                    {

                        Add_Payment_Cash("Repayment of Sales");
                        MessageBox.Show(@"Payment is made");
                        //  cashdrawer();
                    }
                    //else
                    //{
                    //    Add_Payment_Debit("فاتورة مبيعات");
                    //    MessageBox.Show(@"تم الحفظ وتعديل المديونية");
                    //}
                }
             
                PrintInvoice();
                btnAddNew.Enabled = true;
                btnSaveOrder.Enabled = false;
                btnUndoOrder.Enabled = false;
                txtBalance.Text = string.Empty;
                txtItems.Text = string.Empty;
                ClearData();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }



        private void Insert_ROrder(bool INS)
        {
            try
            {
                int x = cls_ROrder.stp_Insert_ROrder(int.Parse(txtOrder_Id.Text), DateTime.Parse(dtpOrder_Date.Text), decimal.Parse(txtOrder_Total.Text), int.Parse(txtCust_Id.Text), txtOrder_SaleMan.Text, lblType.Text);
                if (x > 0)
                {
                    for (int i = 0; i < dgvDetails.Rows.Count; i++)
                    {
                        cls_ROrder.stp_Insert_ROrderDetails(Convert.ToInt32(txtOrder_Id.Text),
                                                              Convert.ToInt32(dgvDetails.Rows[i].Cells[0].Value.ToString()),
                                                              Convert.ToDecimal(dgvDetails.Rows[i].Cells[2].Value.ToString()),
                                                              Convert.ToDecimal(dgvDetails.Rows[i].Cells[3].Value.ToString()),
                                                              Convert.ToDecimal(dgvDetails.Rows[i].Cells[5].Value.ToString()));
                    }
                    MessageBox.Show("Saved Successfully");
                    INS = true;
                }
                else
                {
                    MessageBox.Show("Save Failure");
                    INS = false;
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Payment_Cash(string paymethod)
        {
            try
            {
                DataTable dt = cls_CashIn.stp_SelectLast_CashIn_Id();
                int dbid = Convert.ToInt32(dt.Rows[0][0].ToString());
                cls_CashIn.Add_CashIn(dbid, DateTime.Parse(dtpOrder_Date.Text), decimal.Parse(txtOrder_Total.Text), paymethod, int.Parse(txtCust_Id.Text));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        private void PrintInvoice()
        {
            // get last order to print
            if (MessageBox.Show("Do you really want to cancel your billing data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
               
            {
                try
                {

                    this.Cursor = Cursors.WaitCursor;
                    int Order_No = int.Parse(txtOrder_Id.Text);
                    RPT.rpt_Print_ROrder MyReport = new RPT.rpt_Print_ROrder();
                    RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                    MyReport.SetDataSource(cls_ROrder.stp_SelectROrder_Details(Order_No));
                    MyReport.Refresh();
                    MyForm.crystalReportViewer1.ReportSource = MyReport;
                    MyForm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void btnUndoOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you really want to cancel your billing data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dgvDetails.Rows.Clear();
                    dgvDetails.Refresh();
                    ClearData();
                    ClearBoxes();
                    txtCurrent.Text = txtBalance.Text = txtOrder_Total.Text = txtAvQty.Text = txtLqty.Text = txtSqty.Text =
                    txtLqtyPrice.Text = txtSqty.Text = txtSqtyPrice.Text = string.Empty;
                    btnAddNew.Enabled = true;
                    btnUndoOrder.Enabled = false;
                    btnSaveOrder.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode != Keys.Enter || txtBarcode.Text == string.Empty) return;
                DataTable dt = cls_Product.stp_SelectAll_ProductsByBarcode(txtBarcode.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(@"Product Not Exist");
                    txtBarcode.Clear();
                    return;
                }

                if ((bool)dt.Rows[0]["Taxed"] == true)
                {
                    if (rdbLarge.Checked)
                    {
                        txtProduct_Id.Text = dt.Rows[0]["Product_Id"].ToString();
                        txtProduct_Name.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["L.Price"].ToString();
                        txtLqty.Text = dt.Rows[0]["Large Unit"].ToString();
                        txtSqty.Text = dt.Rows[0]["Small Unit"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["L.Price"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["S.Price"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Balance"].ToString();
                        // ------- Compute Tax
                        CalculateTax(txtPrice.Text);
                    }
                    else
                    {
                        txtProduct_Id.Text = dt.Rows[0]["Product_Id"].ToString();
                        txtProduct_Name.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["S.Price"].ToString();
                        txtLqty.Text = dt.Rows[0]["Large Unit"].ToString();
                        txtSqty.Text = dt.Rows[0]["Small Unit"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["L.Price"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["S.Price"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Balance"].ToString();
                        // ------- Compute Tax
                        CalculateTax(txtPrice.Text);
                    }
                }

                else if ((bool)dt.Rows[0]["Taxed"] == false)
                {
                    if (rdbLarge.Checked)
                    {
                        txtProduct_Id.Text = dt.Rows[0]["Product_Id"].ToString();
                        txtProduct_Name.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["L.Price"].ToString();
                        txtLqty.Text = dt.Rows[0]["Large Unit"].ToString();
                        txtSqty.Text = dt.Rows[0]["Small Unit"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["L.Price"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["S.Price"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Balance"].ToString();
                        // ------- Tax = 0
                        txtDisc.Text = "0";
                    }
                    else
                    {
                        txtProduct_Id.Text = dt.Rows[0]["Product_Id"].ToString();
                        txtProduct_Name.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["S.Price"].ToString();
                        txtLqty.Text = dt.Rows[0]["Large Unit"].ToString();
                        txtSqty.Text = dt.Rows[0]["Small Unit"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["L.Price"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["S.Price"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Balance"].ToString();
                        // ------- Tax = 0
                        txtDisc.Text = "0";
                    }
                }

                // any case do the following :-
                txtqty.Text = "1";
                txtqty.Focus();
                // compute inventory & order balance

                CalculateAmount();

                //استدعاء دالة لاظهار قيم مربعات النصوص اسفل الفاتورة
                txtAvQty.Text = cls_Controls.ShowAvailableQty1(Convert.ToInt32(txtAvQty.Text), Convert.ToInt32(txtCurrent.Text)).ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        void CalculateNetValue()
        {
            try
            {

                if (txtValue.Text != string.Empty && txtDisc.Text != string.Empty)
                {


                    _netValue = decimal.Parse(txtValue.Text) + decimal.Parse(txtDisc.Text);

                }
                else if (txtValue.Text != string.Empty)
                {

                    _netValue = decimal.Parse(txtValue.Text);

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        void CalculateAmount()
        {
            try
            {
                if (txtqty.Text != string.Empty && txtPrice.Text != string.Empty && txtDisc.Text != "0")
                {

                    txtValue.Text = (decimal.Parse(txtqty.Text) * decimal.Parse(txtPrice.Text)).ToString();
                    txtDisc.Text = (Convert.ToDecimal(txtValue.Text) * 5 / 100).ToString();

                }
                else
                {
                    txtValue.Text = (decimal.Parse(txtqty.Text) * decimal.Parse(txtPrice.Text)).ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }




        private void btnAdd_Click(object sender, EventArgs e)
        {

            // كود للتحقق من عدم تكرار ادخال نفس المنتج في نفس الفاتوره
            for (int i = 0; i < dgvDetails.Rows.Count - 1; i++)
            {
                if (dgvDetails.Rows[i].Cells[0].Value.ToString() == txtProduct_Id.Text)
                {
                    MessageBox.Show(@"Balance Not Avialable");
                    return;
                }

            }
            if (txtDisc.Text == string.Empty)
            {
                txtDisc.Text = "0.0";
            }


            // اضافة بيانات المنتج من مربع النص الى Datagrid
            dgvDetails.Rows.Add(txtProduct_Id.Text, txtProduct_Name.Text, txtqty.Text, txtPrice.Text, txtValue.Text, txtDisc.Text, _netValue);
            //استدعاء دالة لاظهار قيم مربعات النصوص اسفل الفاتورة
            Calculate_BalanceQtyInInvoice(txtProduct_Id.Text);
            txtAvQty.Text = cls_Controls.ShowAvailableQty1(Convert.ToInt32(txtAvQty.Text), Convert.ToInt32(txtCurrent.Text)).ToString();
            int nRowIndex = dgvDetails.Rows.Count - 1;
            dgvDetails.CurrentCell = dgvDetails.Rows[nRowIndex].Cells[0];
            ClearBoxes();
            DetailsRefresh();
            txtBarcode.Focus();

            // كود مكتوب بـ linq للتجميع من داتا جريد
            txtOrder_Total.Text =
                (from DataGridViewRow row in dgvDetails.Rows
                 where row.Cells[6].FormattedValue.ToString() != string.Empty
                 select Convert.ToDecimal(row.Cells[6].FormattedValue)).Sum().ToString();
        }

        private void cboCust_Name_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                dt = cls_Customer.stp_SelectCustomersByName(cboCust_Name.Text);
                // في حالة عدم توفر صوره للعميل
                if (dt.Rows[0][5] is DBNull)
                {
                    txtCust_Id.Text = dt.Rows[0][0].ToString();
                    txtCust_Name.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    // في حالة توفر الصورة
                    txtCust_Id.Text = dt.Rows[0][0].ToString();
                    txtCust_Name.Text = dt.Rows[0][1].ToString();
                    //picBox_Cust.Image = null;
                    //byte[] image = (byte[])dt.Rows[0][5];

                    //MemoryStream ms = new MemoryStream(image);
                    //picBox_Cust.Image = Image.FromStream(ms);
                    ////txtCust_Id.Text = dt.Rows[0][0].ToString();
                    ////txtCust_Id.Text = dt.Rows[0][0].ToString();
                }

                decimal balance = cls_Controls.NetBalanceForCustomers((txtCust_Id.Text));
                txtBalance.Text = balance.ToString();

            }
            catch
            {
                return;
            }
        }

        private void rdbSmall_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Text = txtLqtyPrice.Text;
            txtqty.Focus();
        }

        private void rdbLarge_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Text = txtSqtyPrice.Text;
            txtqty.Focus();
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

        private void dgvDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetails.Rows.Count != 0)
                {
                    DataTable dt = cls_Product.stp_SelectAll_ProductsById(Convert.ToInt32(dgvDetails.CurrentRow.Cells[0].Value.ToString()));
                    txtAvQty.Text = dt.Rows[0][3].ToString();
                    txtLqty.Text = dt.Rows[0][5].ToString();
                    txtLqtyPrice.Text = dt.Rows[0][6].ToString();
                    txtSqty.Text = dt.Rows[0][7].ToString();
                    txtSqtyPrice.Text = dt.Rows[0][8].ToString();
                    Calculate_BalanceQtyInInvoice(dt.Rows[0][0].ToString());
                    txtAvQty.Text = cls_Controls.ShowAvailableQty1(Convert.ToInt32(txtAvQty.Text), Convert.ToInt32(txtCurrent.Text)).ToString();

                }
                else
                {
                    ClearDownBoxes();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void dgvDetails_KeyDown(object sender, KeyEventArgs e)
        {
            dgvDetails_Click(null, null);
        }

        private void dgvDetails_KeyUp(object sender, KeyEventArgs e)
        {
            dgvDetails_Click(null, null);
        }
    }
}
