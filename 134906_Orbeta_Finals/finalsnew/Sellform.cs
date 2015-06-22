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
    public partial class Sellform : Form
    {
        public Sellform()
        {
            InitializeComponent();
        }

        public string Getstock
        {
            get { return txtQuantity.Text; }
        }

        Form1 main = new Form1();

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            /*int outPrice;

            if (int.TryParse(Getstock, out outPrice))
            {
                if (Convert.ToInt32(Getstock) < 0)
                {
                    MessageBox.Show("Error: Must be positive");
                }
                else if (Convert.ToInt32(Getstock) > 0)
                {
                    MessageBox.Show("Item Sold");
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Error: Must be a number");
            }*/


            main.UnsavedChanges = true;
            Close();
           

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                btnCancel.DialogResult = DialogResult.OK;
                this.Refresh();
                this.Close();
            }
            else
            {
                btnCancel.DialogResult = DialogResult.OK;
                this.Close();
            }

            
            /*if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Error");
                this.Refresh();
            }
            else
            {
                this.Close();
                this.Refresh();
            }*/


            
            //this.btnCancel.DialogResult = DialogResult.OK;
            
        }
    }
}
