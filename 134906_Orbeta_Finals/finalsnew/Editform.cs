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
    public partial class Editform : Form
    {

        
        public Form1 main;
        private Constructor itemSaved;
        public Editform()
        {
            InitializeComponent();
        }

        public string GetName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }
        public string GetPrice
        {
            
            get {return String.Format("{0:N2}", txtPrice.Text); }
            set { txtPrice.Text = value; }
        }
        

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.DialogResult = DialogResult.OK;

            double outPrice;

            

            if (double.TryParse(GetPrice, out outPrice))
            {
                if (Convert.ToDouble(GetPrice) < 0)
                {
                    MessageBox.Show("Error: Must be positive");
                }
            }
            else
            {
                MessageBox.Show("Error: Must be a number");
            }

            MessageBox.Show("Succesfully Edited");
            this.Close();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
          

    }
}
