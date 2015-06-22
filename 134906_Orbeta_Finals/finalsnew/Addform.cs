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
    public partial class Addform : Form
    {
        int extra = 0;
        public Form1 main;
        public Addform()
        {
            InitializeComponent();
  
        }

        /*public string CName
        {
            get { return txtName.Text; }
        }
        public string Code
        {
            get { return txtCode.Text; }
        }
        public double Price
        {
            get { return double.Parse(txtPrice.Text); }
        }
        public int InventoryCount
        {
            get { return int.Parse(txtStock.Text); }
        }*/

        public Addform(Form1 form)
        {
            InitializeComponent();
            main = form;
        }


        //Form1 main = new Form1();

        private void btnSave_Click(object sender, EventArgs e)
        {
            int outStock;

            if (main.Empty(txtCode) || main.Empty(txtName) || main.Empty(txtPrice) || main.Empty(txtStock))
            {
                MessageBox.Show("Please fill out missing information");
                return;
            }
            if (int.TryParse(txtStock.Text, out outStock))
            {
                if (main.ValidationChecks(txtCode.Text, txtPrice.Text, int.Parse(txtStock.Text)))
                {
                    if (!main.find(txtCode.Text))
                    {
                        Constructor item = new Constructor(txtCode.Text, txtName.Text, (double.Parse(txtPrice.Text)), int.Parse(txtStock.Text));
                        main.Inventory.Add(item);
                        main.makeList();
                        MessageBox.Show("Added Successfully");
                        main.UnsavedChanges = true;
                        this.Close();


                        // main.makeList();
                        //MessageBox.Show("Succesfully Added");
                        //main.UnsavedChanges = true;
                        //this.Close();
                    }
                }

                else
                {
                    MessageBox.Show("ID already exists!");
                }

            }
            else
            {
                MessageBox.Show("Must be a digit");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
