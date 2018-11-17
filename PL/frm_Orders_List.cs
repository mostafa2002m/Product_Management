using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRODUCTS_MANAGEMENT.PL
{
    public partial class frm_Orders_List : Form
    {
        public frm_Orders_List()
        {
            InitializeComponent();
            dgvOrders.DataSource = cls_Order.stp_Search_Orders("");
        }
    }
}
