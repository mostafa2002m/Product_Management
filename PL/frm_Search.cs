﻿using PRODUCTS_MANAGEMENT.BL;
using System.Windows.Forms;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_Search : Form
    {
        string ch;
        public frm_Search(string _ch)
        {
            InitializeComponent();
            ch = _ch;
            if (ch =="Supplier")
            {
               dgvSearch.DataSource = cls_Supplier.stp_SelectAll_Supplier();
               this.Text = "Suppliers Screen ";
            }

            else if (ch == "Customer" )
            {
                dgvSearch.DataSource = cls_Customer.stp_SelectAll_Customers();
                this.Text = "Customers Screen";
            }

            else if (ch=="Product")
            {
                dgvSearch.DataSource = cls_Product.stp_SelectAll_Products();
                this.Text = "Products Screen";
            }
            else if (ch=="Service")
            {
                dgvSearch.DataSource = cls_Service.stp_SelectAll_Service();
                this.Text = "Service Screen";
            }
            else if (ch=="MadeProduct")
            {
                dgvSearch.DataSource = cls_MadeProduct.stp_SelectAll_MadeProducts();
                this.Text = "WorkShop Screen";
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

       
        private void dgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            btnClose.Tag = string.Empty;
            Close();
        }
    }
}
