using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRODUCTS_MANAGEMENT.BL;
using System.Globalization;
using System.IO;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_Pos : Form
    {
        decimal Tax = 0;
        decimal _total;
        decimal _netValue = 0;
       
        char _d = char.Parse(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
        public frm_Pos()
        {
            InitializeComponent();


        }

        private void clearData()
        {
            foreach (Control item in pnlOrder.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is DateTimePicker)
                {
                    item.Text = string.Empty;
                }
            }

            foreach (Control item in pnlOperation.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is DateTimePicker)
                {
                    item.Text = string.Empty;
                }
            }
            foreach (Control item in pnlTotal.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is DateTimePicker)
                {
                    item.Text = string.Empty;
                }
            }
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is DateTimePicker)
                {
                    item.Text = string.Empty;
                }
            }

            foreach (Control item in grpInfo.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is DateTimePicker)
                {
                    item.Text = string.Empty;
                }
            }
            pnlBoxes.Enabled = false;
            pnlDgv.Enabled = false;
            pnlOperation.Enabled = false;
            pnlOrder.Enabled = false;
            pnlTotal.Enabled = false;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnClear.Enabled = false;

            dtpInvDate.ResetText();


            dgvPos.Rows.Clear();
            dgvPos.Refresh();

        }

        private void ClearBoxes()
        {
            foreach (Control item in pnlBoxes.Controls)
            {
                if (item is TextBox)
                    item.Text = string.Empty;
                else if (item is DateTimePicker)
                {
                    item.Text = string.Empty;
                }
                cboProductName.SelectedIndex = -1;
            }


        }

        private void frm_Pos_Load(object sender, EventArgs e)
        {

            cls_Controls.BindCombo(ref cboProductName, "stp_SelectAll_Products", "اسم المنتج", "رقم المنتج");

            clearData();
            ClearBoxes();
            txtUser.Text = Program.User_FullName;
            LoadCustomer_Combo();
            cboProductName.SelectedIndex = -1;
            cboType.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkBarcode.Checked)
                {

                    txtBarcode.Focus();
                }
                else
                {
                    cboProductName.Focus();
                }
                ClearBoxes();
                txtOrder_Id.Text = cls_Order.stp_SelectLast_Order_Id().Rows[0][0].ToString();
                btnNew.Enabled = false;
                btnSave.Enabled = true;
                btnClear.Enabled = true;
                btnPrint.Enabled = true;
                pnlBoxes.Enabled = true;
                pnlDgv.Enabled = true;
                pnlOperation.Enabled = true;
                pnlOrder.Enabled = true;
                pnlTotal.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void chkBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBarcode.Checked)
            {

                txtBarcode.Focus();
            }
            else
            {
                cboProductName.Focus();
            }
        }

 

        private void CalculateTax(string price)
        {
            decimal tax = Convert.ToDecimal(price) * 5 / 100;
            txtDisc.Text = (Convert.ToDecimal(txtPrice.Text) * 5 / 100).ToString();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text == string.Empty) return;
                DataTable dt = cls_Product.stp_SelectAll_ProductsByBarcode(txtBarcode.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(@"هذا المنتج غير مسجل");
                    txtBarcode.Clear();
                    txtBarcode.Focus();
                    return;
                }

               
                if ((bool)dt.Rows[0]["Taxed"] == true)
                {
                    txtProductId.Text = dt.Rows[0]["Product_Id"].ToString();
                    cboProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                    txtPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                    txtLqty.Text = dt.Rows[0]["lunitqty"].ToString();
                    txtSqty.Text = dt.Rows[0]["sunitqty"].ToString();
                    txtLqtyPrice.Text = dt.Rows[0]["lunitprice"].ToString();
                    txtSqtyPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                    txtAvQty.Text = dt.Rows[0]["Qte_In_Stock"].ToString();
                    // ------- Compute Tax
                    CalculateTax(txtPrice.Text);

                }
                #region
                //if (rdbLarge.Checked)
                //{
                //    txtProductId.Text = dt.Rows[0]["رقم المنتج"].ToString();
                //    cboProductName.SelectedValue = dt.Rows[0]["اسم المنتج"].ToString();
                //    txtPrice.Text = dt.Rows[0]["سعر كبرى"].ToString();
                //    txtLqty.Text = dt.Rows[0]["وحدة كبرى"].ToString();
                //    txtSqty.Text = dt.Rows[0]["وحدة صغرى"].ToString();
                //    txtLqtyPrice.Text = dt.Rows[0]["سعر كبرى"].ToString();
                //    txtSqtyPrice.Text = dt.Rows[0]["سعر صغرى"].ToString();
                //    txtqty.Focus();
                //}
                #endregion
                else
                {

                    txtProductId.Text = dt.Rows[0]["Product_Id"].ToString();
                    cboProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                    txtPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                    txtLqty.Text = dt.Rows[0]["lunitqty"].ToString();
                    txtSqty.Text = dt.Rows[0]["sunitqty"].ToString();
                    txtLqtyPrice.Text = dt.Rows[0]["lunitprice"].ToString();
                    txtSqtyPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                    txtAvQty.Text = dt.Rows[0]["Qte_In_Stock"].ToString();
                    // ------- Tax = 0
                    txtDisc.Text = "0";

                }
                // any case do the following :-
                txtQty.Text = "1";
                txtQty.Focus();
                // compute inventory & order balance
                _total = 0;
                Calculate_Balance(txtProductId.Text);
                txtCurrent.Text = _total.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void cboProductName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboProductName.SelectedIndex == -1) return;
                DataTable dt = cls_Product.stp_SelectProductByName(cboProductName.Text);
                if (dt.Rows.Count > 0)
                {

                    if ((bool)dt.Rows[0]["Taxed"] == true)
                    {
                        txtProductId.Text = dt.Rows[0]["Product_Id"].ToString();
                        cboProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtLqty.Text = dt.Rows[0]["lunitqty"].ToString();
                        txtSqty.Text = dt.Rows[0]["sunitqty"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["lunitprice"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Qte_In_Stock"].ToString();

                        // ------- Compute Tax
                        CalculateTax(txtPrice.Text);

                       

                    }
                    #region
                    //if (rdbLarge.Checked)
                    //{
                    //    txtProductId.Text = dt.Rows[0]["رقم المنتج"].ToString();
                    //    cboProductName.SelectedValue = dt.Rows[0]["اسم المنتج"].ToString();
                    //    txtPrice.Text = dt.Rows[0]["سعر كبرى"].ToString();
                    //    txtLqty.Text = dt.Rows[0]["وحدة كبرى"].ToString();
                    //    txtSqty.Text = dt.Rows[0]["وحدة صغرى"].ToString();
                    //    txtLqtyPrice.Text = dt.Rows[0]["سعر كبرى"].ToString();
                    //    txtSqtyPrice.Text = dt.Rows[0]["سعر صغرى"].ToString();
                    //    txtqty.Focus();
                    //}
                    #endregion
                    else
                    {

                        txtProductId.Text = dt.Rows[0]["Product_Id"].ToString();
                        cboProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtLqty.Text = dt.Rows[0]["lunitqty"].ToString();
                        txtSqty.Text = dt.Rows[0]["sunitqty"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["lunitprice"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Qte_In_Stock"].ToString();
                       // ------- Tax = 0
                        txtDisc.Text = "0";
                                              
                    }
                    // any case do the following :-
                    txtQty.Text = "1";
                    txtQty.Focus();
                    // compute inventory & order balance
                    _total = 0;
                    Calculate_Balance(txtProductId.Text);
                    txtCurrent.Text = _total.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

    


        private void cboProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (cboProductName.SelectedIndex == -1) return;
                DataTable dt = cls_Product.stp_SelectProductByName(cboProductName.Text);
                if (dt.Rows.Count > 0)
                {

                    if ((bool)dt.Rows[0]["Taxed"] == true)
                    {
                        txtProductId.Text = dt.Rows[0]["Product_Id"].ToString();
                        cboProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtLqty.Text = dt.Rows[0]["lunitqty"].ToString();
                        txtSqty.Text = dt.Rows[0]["sunitqty"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["lunitprice"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Qte_In_Stock"].ToString();

                        // ------- Compute Tax
                        CalculateTax(txtPrice.Text);



                    }
                    #region
                    //if (rdbLarge.Checked)
                    //{
                    //    txtProductId.Text = dt.Rows[0]["رقم المنتج"].ToString();
                    //    cboProductName.SelectedValue = dt.Rows[0]["اسم المنتج"].ToString();
                    //    txtPrice.Text = dt.Rows[0]["سعر كبرى"].ToString();
                    //    txtLqty.Text = dt.Rows[0]["وحدة كبرى"].ToString();
                    //    txtSqty.Text = dt.Rows[0]["وحدة صغرى"].ToString();
                    //    txtLqtyPrice.Text = dt.Rows[0]["سعر كبرى"].ToString();
                    //    txtSqtyPrice.Text = dt.Rows[0]["سعر صغرى"].ToString();
                    //    txtqty.Focus();
                    //}
                    #endregion
                    else
                    {

                        txtProductId.Text = dt.Rows[0]["Product_Id"].ToString();
                        cboProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                        txtPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtLqty.Text = dt.Rows[0]["lunitqty"].ToString();
                        txtSqty.Text = dt.Rows[0]["sunitqty"].ToString();
                        txtLqtyPrice.Text = dt.Rows[0]["lunitprice"].ToString();
                        txtSqtyPrice.Text = dt.Rows[0]["sunitprice"].ToString();
                        txtAvQty.Text = dt.Rows[0]["Qte_In_Stock"].ToString();
                        // ------- Tax = 0
                        txtDisc.Text = "0";

                    }
                    // any case do the following :-
                    txtQty.Text = "1";
                    txtQty.Focus();
                    // compute inventory & order balance
                    _total = 0;
                    Calculate_Balance(txtProductId.Text);
                    txtCurrent.Text = _total.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtQty.Text != string.Empty)
            {
                txtPrice.Focus();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPrice.Text != string.Empty)
            {

                btnAdd.Focus();
            }
        }

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtQty.Text != "")
            {
            CalculateAmount();
            CalculateNetValue();
            }
           
        }

        void CalculateNetValue()
        {
            try
            {
               
                if (txtValue.Text != string.Empty && txtDisc.Text != string.Empty )
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
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message);
            }

        }
        void CalculateAmount()
        {
            try
            {
                if (txtQty.Text != string.Empty && txtPrice.Text != string.Empty && txtDisc.Text != "0")
                {

                    txtValue.Text = (decimal.Parse(txtQty.Text) * decimal.Parse(txtPrice.Text)).ToString();
                    txtDisc.Text = (Convert.ToDecimal(txtValue.Text) * 5 / 100).ToString();

                }
                else
                {
                    txtValue.Text = (decimal.Parse(txtQty.Text) * decimal.Parse(txtPrice.Text)).ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateAmount();
            CalculateNetValue();
        }

        #region
        //private void txtDisc_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void txtDisc_KeyUp(object sender, KeyEventArgs e)
        //{
        //    CalculateNetValue();
        //}

        //private void txtDisc_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        btnAdd.Focus();
        //    }
        //}

        //private void txtOrder_Total_TextChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        txtTax.Text = "5"; txtTotal.Text = string.Empty;
        //        if (txtTax.Text != string.Empty && txtOrder_Total.Text != string.Empty)
        //        {
        //            txtTax.Text = (Convert.ToDecimal(txtOrder_Total.Text) * (Convert.ToDecimal(txtTax.Text)) / 100).ToString();
        //            txtTotal.Text = (Convert.ToDecimal(txtOrder_Total.Text) + Convert.ToDecimal(txtTax.Text)).ToString();
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        #endregion
        private void txtPaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPaid.Text == "")
                {
                    txtRest.Text = txtOrder_Total.Text;
                    return;
                }
                txtRest.Text = (Convert.ToDecimal(txtOrder_Total.Text) - Convert.ToDecimal(txtPaid.Text)).ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Calculate_Balance(string p)
        {
            decimal realQty;
            for (int i = 0; i < dgvPos.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgvPos.Rows[i].Cells[0].Value).ToString() == p)
                {
                    realQty = Convert.ToDecimal(dgvPos.Rows[i].Cells[2].Value);
                    _total += realQty;

                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // check values|| txtCust_Id.Text == string.Empty
                if (txtOrder_Id.Text == string.Empty || dgvPos.Rows.Count < 1)
                {
                    MessageBox.Show(@"أدخل بيانات العميل", @"تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #region
                //DataTable invDet = new DataTable();
                //invDet.Columns.Add("Product_Id");
                //invDet.Columns.Add("Qty");
                //invDet.Columns.Add("Price");
                //invDet.Columns.Add("Discount");
                //invDet.Columns.Add("Amount");

                //foreach (DataGridViewRow dr in dgvDetails.Rows)
                //{
                //    invDet.Rows.Add(dr.Cells[0].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[5].Value, dr.Cells[6].Value);
                //}

                //cls_Order.Insert_Order(int.Parse(txtOrder_Id.Text),DateTime.Parse(dtpOrder_Date.Text), decimal.Parse(txtOrder_Total.Text), int.Parse(txtCust_Id.Text), txtOrder_SaleMan.Text, invDet);
                #endregion
                bool Ins = true;
                Insert_Order(Ins);
                if (Ins == true)
                {
                    if (rdbCash.Checked)
                    {

                        Add_Payment_Debit("سداد فاتورة مبيعات");
                        MessageBox.Show(@"تم السداد نقدا");
                        //  cashdrawer();
                    }
                    else
                    {
                        Add_Payment_Debit("فاتورة مبيعات");
                        MessageBox.Show(@"تم الحفظ وتعديل المديونية");
                    }
                }
                else
                    return;




                MessageBox.Show(@"لقد تم اضافة دفعة  للعميل");

                PrintInvoice();
                btnNew.Enabled = true;
                btnSave.Enabled = false;
                btnClear.Enabled = false;
                clearData();
                txtBalance.Text = string.Empty;
                txtItems.Text = string.Empty;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Order(bool INS)
        {

            try
            {
                int x = cls_Order.stp_Insert_Order(int.Parse(txtOrder_Id.Text), DateTime.Parse(dtpInvDate.Text), decimal.Parse(txtOrder_Total.Text), int.Parse(txtCust_Id.Text), txtUser.Text);
                if (x > 0)
                {
                    for (int i = 0; i < dgvPos.Rows.Count; i++)
                    {
                        cls_Order.stp_Insert_OrderDetails(Convert.ToInt32(txtOrder_Id.Text),
                                                              Convert.ToInt32(dgvPos.Rows[i].Cells[0].Value.ToString()),
                                                              Convert.ToDecimal(dgvPos.Rows[i].Cells[2].Value.ToString()),
                                                              Convert.ToDecimal(dgvPos.Rows[i].Cells[3].Value.ToString()),
                                                              Convert.ToDecimal(dgvPos.Rows[i].Cells[5].Value.ToString()),
                                                              Convert.ToDecimal(dgvPos.Rows[i].Cells[6].Value.ToString()));
                    }
                    MessageBox.Show("تم حفظ الفاتورة وبياناتها بنجاح");
                    INS = true;
                }
                else
                {
                    MessageBox.Show("لا يمكن حفظ الفاتورة");
                    INS = false;
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void Add_Payment_Debit(string paymethod)
        {
            try
            {
                DataTable dt = cls_Debit.stp_SelectLast_Debit_Id();
                int dbid = Convert.ToInt32(dt.Rows[0][0].ToString());
                cls_Debit.Add_Debit(dbid, DateTime.Parse(dtpInvDate.Text), decimal.Parse(txtOrder_Total.Text), paymethod, int.Parse(txtCust_Id.Text));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void PrintInvoice()
        {
            // get last order to print
            DialogResult respnse = MessageBox.Show(@"طباعة الفاتورة؟", @"طباعة فاتورة مبيعات", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respnse == DialogResult.Yes)
            {

                try
                {

                    this.Cursor = Cursors.WaitCursor;
                    int orderNo = int.Parse(txtOrder_Id.Text);
                    RPT.rpt_Orders myReport = new RPT.rpt_Orders();
                    RPT.frm_Rpt_Product myForm = new RPT.frm_Rpt_Product();
                    myReport.SetDataSource(cls_Order.stp_SelectOrder_Details(orderNo));
                    myReport.Refresh();
                    myForm.crystalReportViewer1.ReportSource = myReport;
                    myForm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // get last order to print

            try
            {

                this.Cursor = Cursors.WaitCursor;
                int orderNo = int.Parse(txtOrder_Id.Text);
                RPT.rpt_Orders myReport = new RPT.rpt_Orders();
                RPT.frm_Rpt_Product myForm = new RPT.frm_Rpt_Product();
                myReport.SetDataSource(cls_Order.stp_SelectOrder_Details(orderNo));
                myReport.Refresh();
                myForm.crystalReportViewer1.ReportSource = myReport;
                myForm.ShowDialog();
                this.Cursor = Cursors.Default;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
            ClearBoxes();
            txtBalance.Text = string.Empty;
            txtItems.Text = string.Empty;
            btnClear.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductId.Text == "" || txtQty.Text == "" || txtPrice.Text == "" ||
                       txtValue.Text == "" || cboProductName.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Check Your Data");
                    return;
                }
                if (cls_Order.Verify_Qty(txtProductId.Text, Convert.ToInt32(txtQty.Text)).Rows.Count < 1)
                {
                    MessageBox.Show(@"الرصيد  لا يكفي");
                    return;
                }



                if (txtDisc.Text == string.Empty)
                {
                    txtDisc.Text = @"0.0";
                }
                #region
                //// اذا كانت وحدة كبرى يتم ضرب الكمية الكبرى بالصغري
                //if (rdbLarge.Checked)
                //{
                //    txtqty.Text = (Math.Round(decimal.Parse(txtqty.Text),2) * Math.Round(decimal.Parse(txtSqty.Text),2)).ToString();
                //}
                #endregion

                dgvPos.Rows.Add(txtProductId.Text, cboProductName.Text, txtQty.Text, txtPrice.Text, txtValue.Text, txtDisc.Text, _netValue);

                _total = 0;

                // حساب رصيدالمنتج بعد اضافته للفاتورة
                Calculate_Balance(txtProductId.Text);
                txtCurrent.Text = _total.ToString(CultureInfo.InvariantCulture);

                DetailsRefresh();
                ClearBoxes();
                txtBarcode.Focus();

                // كود مكتوب بـ linq للتجميع من داتا جريد
                txtOrder_Total.Text =
                    (from DataGridViewRow row in dgvPos.Rows
                     let formattedValue = row.Cells[6].FormattedValue
                     where formattedValue != null && formattedValue.ToString() != string.Empty
                     select Convert.ToDecimal(row.Cells[6].FormattedValue)).Sum().ToString(CultureInfo.CurrentCulture);
                // compute Rest And Paid Minus Order Total
                txtPaid_TextChanged(null, null);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        void DetailsRefresh()
        {
            txtItems.Text = dgvPos.Rows.Count.ToString();
        }

        void NetBalance()
        {
            if (txtCust_Id.Text != string.Empty)
            {
                DataTable dt = cls_Debit.stp_SelectNet_Debit_ByCustId(int.Parse(txtCust_Id.Text));
                decimal Invoice = Convert.ToDecimal(dt.Rows[0][0].ToString());
                decimal RInvoice = Convert.ToDecimal(dt.Rows[0][1].ToString());
                decimal Cashout = Convert.ToDecimal(dt.Rows[0][2].ToString());
                decimal RCashout = Convert.ToDecimal(dt.Rows[0][3].ToString());
                decimal madeproduct = Convert.ToDecimal(dt.Rows[0][4].ToString());
                decimal cashprduct = Convert.ToDecimal(dt.Rows[0][5].ToString());
                decimal servinvice = Convert.ToDecimal(dt.Rows[0][6].ToString());
                decimal cashService = Convert.ToDecimal(dt.Rows[0][7].ToString());
                decimal balance = (Invoice + madeproduct + servinvice) - (RInvoice + Cashout - RCashout + cashprduct + cashService);
                txtBalance.Text = balance.ToString();
            }

        }

        private void dgvPos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtProductId.Text = this.dgvPos.CurrentRow.Cells[0].Value.ToString();
                cboProductName.Text = this.dgvPos.CurrentRow.Cells[1].Value.ToString();
                txtQty.Text = this.dgvPos.CurrentRow.Cells[2].Value.ToString();
                txtPrice.Text = this.dgvPos.CurrentRow.Cells[3].Value.ToString();
                txtValue.Text = this.dgvPos.CurrentRow.Cells[4].Value.ToString();
                txtDisc.Text = this.dgvPos.CurrentRow.Cells[5].Value.ToString();
                dgvPos.Rows.RemoveAt(dgvPos.CurrentRow.Index);
                DetailsRefresh();
                txtQty.Focus();
                _total = 0;
                Calculate_Balance(txtProductId.Text);
                txtCurrent.Text = _total.ToString();
            }

            catch
            {
                return;
            }

        }

        private void dgvPos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // كود مكتوب بـ linq للتجميع من داتا جريد
            txtOrder_Total.Text =
                (from DataGridViewRow row in dgvPos.Rows
                 let value = row.Cells[6].FormattedValue
                 where value != null && value.ToString() != string.Empty
                 select Convert.ToDecimal(row.Cells[6].FormattedValue)).Sum().ToString();
            DetailsRefresh();
            NetBalance();

            // compute Rest And Paid Minus Order Total
            txtPaid_TextChanged(null, null);


        }

        private void cboCust_Name_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = cls_Customer.stp_SelectCustomersByName(cboCust_Name.Text);
                // في حالة عدم توفر صوره للعميل
                if (dt.Rows[0][5] is DBNull)
                {
                    txtCust_Id.Text = dt.Rows[0][0].ToString();
                    //txtCust_Name.Text = dt.Rows[0][1].ToString();

                    NetBalance();

                    return;
                }
                // في حالة توفر الصورة
                txtCust_Id.Text = dt.Rows[0][0].ToString();
                //txtCust_Name.Text = dt.Rows[0][1].ToString();

                NetBalance();
                //picBox_Cust.Image = null;
                //byte[] image = (byte[])dt.Rows[0][5];

                //MemoryStream ms = new MemoryStream(image);
                //picBox_Cust.Image = Image.FromStream(ms);
                ////txtCust_Id.Text = dt.Rows[0][0].ToString();
                ////txtCust_Id.Text = dt.Rows[0][0].ToString();
            }
            catch
            {
                return;
            }
        }

        private void LoadCustomer_Combo()
        {
            cls_Controls.BindCombo(ref cboCust_Name, "stp_SelectAll_Customers", "Cust_Name", "Cust_Id");
            cboCust_Name.SelectedIndex = 0;
            DataTable dt = cls_Customer.stp_SelectCustomersByName(cboCust_Name.Text);
            // في حالة عدم توفر صوره للعميل
            if (dt.Rows.Count > 0)
            {
                txtCust_Id.Text = dt.Rows[0][0].ToString();
                //txtCust_Name.Text = dt.Rows[0][1].ToString();

                NetBalance();

                return;
            }

        }

        private void frm_Pos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control == true && e.KeyCode == Keys.N)
                {
                    btnNew.PerformClick();
                }
                 if (e.Control == true && e.KeyCode == Keys.P)
                {
                    btnPrint.PerformClick();
                }

                 if (e.Control == true && e.KeyCode == Keys.S)
                {
                    btnSave.PerformClick();
                }
                 if (e.Control == true && e.KeyCode == Keys.U)
                {
                    DialogResult respnse = MessageBox.Show(@"هل أنت متأكد من مسح الفاتورة؟", @"مسح الفاتورة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respnse == DialogResult.Yes)
                        btnClear.PerformClick();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

    }
}