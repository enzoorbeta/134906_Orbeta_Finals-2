using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace finalsnew
{
    public partial class Restockform : Form
    {
        public Restockform()
        {
            InitializeComponent();
        }

        public string GetRestock
        {
            get { return txtQuantity.Text; }
        }

        private void btnRestock_Click(object sender, EventArgs e)
        {
            
           
            int outResult;

            if (int.TryParse(GetRestock, out outResult))
            {
                if (Convert.ToInt32(GetRestock) < 0)
                {
                    MessageBox.Show("Error: Must be positive");
                }
                else
                {
                    btnRestock.DialogResult = DialogResult.OK;
                    
                }
            }
            else
            {
                MessageBox.Show("Error: Must be a digit!");
            }
            
            /*if (int.TryParse(GetRestock, out outResult))
            {
            }
            else
            {
                MessageBox.Show("Error: Must be a digit");
            }*/
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
