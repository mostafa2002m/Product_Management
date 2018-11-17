using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class FrmLogin : Form
    {
        public static bool CheckUser = false;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)

        {


            DataTable dt = cls_Login.STP_LOGIN(txtId.Text, txtPass.Text);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Per_Id"].ToString() == "2")
                {
                    // enable the menu through the getmainform method in main form
                    MessageBox.Show(@"Login Successfully");
                   
                    frmMain.CheckUser = true;
                    frmMain.P_Id = int.Parse(dt.Rows[0]["Per_Id"].ToString());
                    Program.User_FullName = dt.Rows[0]["User_FullName"].ToString();

                    this.Close();
                   
                }


              else if (dt.Rows[0]["Per_Id"].ToString() == "1")
                {
                    MessageBox.Show(@"Login Successfully");
                    frmMain.CheckUser = true;
                    frmMain.P_Id = int.Parse(dt.Rows[0]["Per_Id"].ToString());
                    // invoking the username as refernce for operations
                    Program.User_FullName = dt.Rows[0]["User_FullName"].ToString();
                  

                    this.Close();
                }

                
               
                else
                {
                    MessageBox.Show(@"Login Failure");
                    frmMain.CheckUser = false;
                    frmMain.P_Id = 0;
                }

            }
        }

        private void ChangeColour(object sender, EventArgs e)
        {
            //Button bColour = (Button)sender;
            //bColour.BackColor = Color.LightSteelBlue;
        }

        private void ColourChange(object sender, EventArgs e)
        {
            //Button bColour = (Button)sender;
            //bColour.BackColor = Color.FromKnownColor(KnownColor.Control);
        }
    }
}
