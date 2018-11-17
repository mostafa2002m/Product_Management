using System;
using System.IO;
using System.Windows.Forms;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frmInvoices_List : Form
    {
       
        public frmInvoices_List()
        {
            InitializeComponent();
            this.dgvOrders.DataSource = cls_Invoice.stp_Search_Invoices("");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgvOrders.DataSource = cls_Invoice.stp_Search_Invoices(txtSearch.Text);
            }
            catch (Exception)
            {

                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // get last order to print

            try
            {
                if (dgvOrders.Rows.Count == 0)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                int Order_No = Convert.ToInt32(dgvOrders.CurrentRow.Cells[0].Value);
                RPT.rpt_Invoices MyReport = new RPT.rpt_Invoices();
                RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                MyReport.SetDataSource(cls_Invoice.Stp_SelectInvoice_Details(Order_No));
                //MyReport.Refresh();
                MyForm.crystalReportViewer1.ReportSource = MyReport;
                MyForm.ShowDialog();
                this.Cursor = Cursors.Default;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmInvoices_List_Load(object sender, EventArgs e)
        {

        }
    }
}
