using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_List_Management : Form
    {
        string List;
        public frm_List_Management(string _List)
        {
            InitializeComponent();
            List = _List;
            if (List == "Orders")
            {
                dgvListManagement.DataSource = cls_Order.stp_Search_Orders("");
                lblList.Text = "Orders List Screen";
                cboType.Items.Add("Sales");
                cboType.Items.Add("Return Sales");
                cboType.SelectedIndex = 0;
            }
           
            else if (List == "Invoices")
            {
                dgvListManagement.DataSource = cls_Invoice.stp_Search_Invoices("");
                lblList.Text = "Invoices List Screen";
                cboType.Items.Add("Purchase");
                cboType.Items.Add("Return Purchase");
                cboType.SelectedIndex = 0;
            }
           
            else if (List == "MadeOrders")
            {
                dgvListManagement.DataSource = cls_MadeOrder.stp_Search_MadeOrders("");
                lblList.Text = "Made Orders Invoices List Screen";
            }

            else if (List == "Services")
            {
                dgvListManagement.DataSource = cls_Service.stp_SelectAll_Service();
                lblList.Text = "Service Invoices List Screen";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListManagement.Rows.Count == 0)
                {
                    return;
                }
                if (List == "Orders" && cboType.SelectedIndex == 0)
                {

                    // get last order to print
                    DialogResult respnse = MessageBox.Show(@"Print Saled Invoice?", @"Print Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respnse == DialogResult.Yes)
                    {

                        this.Cursor = Cursors.WaitCursor;
                        int Order_No = int.Parse(dgvListManagement.CurrentRow.Cells[0].Value.ToString());
                        RPT.rpt_Orders MyReport = new RPT.rpt_Orders();
                        RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                        MyReport.SetDataSource(cls_Order.stp_SelectOrder_Details(Order_No));
                        MyReport.Refresh();
                        MyForm.crystalReportViewer1.ReportSource = MyReport;
                        MyForm.ShowDialog();
                        this.Cursor = Cursors.Default;

                    }
                }

                // get last order to print
                else if (List == "Invoices" && cboType.SelectedIndex == 0)
                {
                    DialogResult respnse = MessageBox.Show(@"Print Purchase Invoice?", @"Print Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respnse == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int Order_No = Convert.ToInt32(dgvListManagement.CurrentRow.Cells[0].Value);
                        RPT.rpt_Invoices MyReport = new RPT.rpt_Invoices();
                        RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                        MyReport.SetDataSource(cls_Invoice.Stp_SelectInvoice_Details(Order_No));
                        //MyReport.Refresh();
                        MyForm.crystalReportViewer1.ReportSource = MyReport;
                        MyForm.ShowDialog();
                        this.Cursor = Cursors.Default;
                    }

                }
                else if(List == "Orders" && cboType.SelectedIndex == 1)
                {
                    DialogResult respnse = MessageBox.Show(@"Print Return Sales Invoice", @"Print Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respnse == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int Order_No = Convert.ToInt32(dgvListManagement.CurrentRow.Cells[0].Value);
                        RPT.rpt_Print_ROrder MyReport = new RPT.rpt_Print_ROrder();
                        RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                        MyReport.SetDataSource(cls_ROrder.stp_SelectROrder_Details(Order_No));
                        MyReport.Refresh();
                        MyForm.crystalReportViewer1.ReportSource = MyReport;
                        MyForm.ShowDialog();
                        this.Cursor = Cursors.Default;
                    }
                }

                else if (List == "Invoices" && cboType.SelectedIndex == 1)
                {
                    DialogResult respnse = MessageBox.Show(@"Print Return Purchase Invoice", @"Print Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respnse == DialogResult.Yes)
                    { 
                        int Inv_No = Convert.ToInt32(cls_Invoice.stp_SelectInvoice_DetailsForPrint().Rows[0][0]);
                    this.Cursor = Cursors.WaitCursor;
                    RPT.rpt_Print_RInvoice MyReport = new RPT.rpt_Print_RInvoice();
                    RPT.frm_Rpt_Product MyForm = new RPT.frm_Rpt_Product();
                    MyReport.SetDataSource(cls_RInvoice.stp_SelectRInvoice_Details(Inv_No));
                    MyReport.Refresh();
                    MyForm.crystalReportViewer1.ReportSource = MyReport;
                    MyForm.ShowDialog();
                    this.Cursor = Cursors.Default;
                    }
                }

                else if (List == "MadeOrders")
                {
                    dgvListManagement.DataSource = cls_MadeOrder.stp_Search_MadeOrders("");
                    lblList.Text = "Made Orders Invoices List Screen";
                }

                else if (List == "Services")
                {
                    dgvListManagement.DataSource = cls_Service.stp_SelectAll_Service();
                    lblList.Text = "Service Invoices List Screen";
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cboType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (List == "Orders" && cboType.SelectedIndex == 1)
            {
                dgvListManagement.DataSource = cls_ROrder.stp_Search_ROrders("");
                lblList.Text = "Return Orders List Screen";
            }
            else if (List == "Orders" && cboType.SelectedIndex == 0)
            {
                dgvListManagement.DataSource = cls_Order.stp_Search_Orders("");
                lblList.Text = "Orders List Screen";
            }
          
            else if (List=="Invoices" && cboType.SelectedIndex == 1)
            {
                dgvListManagement.DataSource = cls_RInvoice.stp_Search_RInvoices("");
                lblList.Text = "Return Invoices List Screen";
            }
            else if (List == "Invoices" && cboType.SelectedIndex == 0)
            {
                dgvListManagement.DataSource = cls_Invoice.stp_Search_Invoices("");
                lblList.Text = " Invoices List Screen";
            }
        }
    }
}