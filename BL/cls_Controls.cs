using PRODUCTS_MANAGEMENT.DAL;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System;

namespace PRODUCTS_MANAGEMENT.PL
{
    class cls_Controls
    {
        // method to fill comboboxes with data
        public static void BindCombo(ref ComboBox cmb, string query, string displayMem, string valueMem)
        {
            DataAccessLayer.Open();
            var dt = DataAccessLayer.ExcuteTable(query,CommandType.StoredProcedure);
            cmb.DataSource = dt; 
            cmb.DisplayMember = displayMem;
            cmb.ValueMember = valueMem;

        }

        // دالة لحساب رصيد العميل من العمليات السابقة
        public static decimal NetBalanceForSuppliers(string ID)
        {

            if (ID != string.Empty)
            {
                DataTable dt = cls_Credit.stp_Select_Net_Credit_BySupId(int.Parse(ID));
                decimal debit = Convert.ToDecimal(dt.Rows[0][0].ToString());
                decimal credit = Convert.ToDecimal(dt.Rows[0][1].ToString());
                decimal balance = debit - credit;
                return balance;
            }
            else
            {
                return 0;
            }
        }
        // دالة لحساب رصيد العميل من العمليات السابقة
        public static decimal NetBalanceForCustomers(string ID)
        {

            if (ID != string.Empty)
            {
                DataTable dt = cls_Debit.stp_SelectNet_Debit_ByCustId(int.Parse(ID));
                decimal debit = Convert.ToDecimal(dt.Rows[0][0].ToString());
                decimal credit = Convert.ToDecimal(dt.Rows[0][1].ToString());
                decimal balance = debit - credit;
                return balance;
            }
            else
            {
                return 0;
            }
        }
        // حساب رصيدالمنتج بعد اضافته للفاتورة
        public static int ShowAvailableQty(int balqty, int currqty)
        {
            int avqty = balqty - currqty;
            return avqty;
        }
        public static int ShowAvailableQty1(int balqty, int currqty)
        {
            int avqty = balqty + currqty;
            return avqty;
        }
        public static decimal CalculateTax(decimal price)
        {
            decimal tax = Convert.ToDecimal(price) * 5 / 100;
            return tax;

        }
        public static void PrintInvoice(int invid, Form MyForm, object MyReport, string proc)
        {

            try
            {


                ////RPT.rpt_PrintRInvoice MyReport = new RPT.rpt_PrintRInvoice();
                ////RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                //MyReport.SetDataSource(proc + "("+invid+")");
                //MyReport.Refresh();
                //MyForm.crystalReportViewer1.ReportSource = MyReport;
                //MyForm.ShowDialog();

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
