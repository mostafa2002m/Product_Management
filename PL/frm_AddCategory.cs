using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_AddCategory : Form
    {
        string ch;
        public frm_AddCategory(string _ch)
        {
            InitializeComponent();
            ch = _ch;
            if (ch=="a")
            {
                this.Text = "Add New Category";
                btnAddNew.Text = "Save Adding";
                txtCategoryName.Focus();
            }
            else if (ch=="u")
            {
                this.Text = "Update Category";
                btnAddNew.Text = "Save Update";
                DataTable dt = cls_Category.stp_SelectAll_CategoriesById(frm_CategoryManagement.ID);
                foreach (DataRow dr in dt.Rows)
                {
                    txtCategoryName.Text = dr[1].ToString();
                    txtDescription.Text = dr[2].ToString();
                    txtCategoryName.Focus();
                }
            }
            else if (ch=="d")
            {
                this.Text = "Delete Category";
                btnAddNew.Text = "Save Delete";
                DataTable dt = cls_Category.stp_SelectAll_CategoriesById(frm_CategoryManagement.ID);
                foreach (DataRow dr in dt.Rows)
                {
                    txtCategoryName.Text = dr[1].ToString();
                    txtDescription.Text = dr[2].ToString();

                }
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryName.Text == string.Empty)
                {
                    MessageBox.Show(@"Please Insert Category");
                    return;
                }

                if (ch=="a")
                {
                    int i = cls_Category.stp_ManipulateCategory(ch,txtCategoryName.Text,txtDescription.Text);
                    MessageBox.Show(@"Added : " + i + @"Rows");
                    txtCategoryName.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    txtCategoryName.Focus();
                }

                else if (ch == "u")
                {
                    int i = cls_Category.stp_ManipulateCategory(ch, txtCategoryName.Text, txtDescription.Text);
                    MessageBox.Show(@"Updated : " + i + @"Rows");
                    txtCategoryName.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    txtCategoryName.Focus();
                }
                else if (ch == "d")
                {
                    if (MessageBox.Show(@"Delete Current", @"Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int i = cls_Category.stp_ManipulateCategory(ch, txtCategoryName.Text, txtDescription.Text);
                        MessageBox.Show(@"Deleted : " + i + @"Rows");
                        txtCategoryName.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        txtCategoryName.Focus();
                    }
                }

            }
            catch (SqlException ex)
            {

                MessageBox.Show("Name Duplicated");
                txtCategoryName.Focus();
                return;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
