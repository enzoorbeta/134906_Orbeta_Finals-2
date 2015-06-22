using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace finalsnew
{
    public partial class Form1 : Form
    {
        
        public List<Constructor> inventory;
        private bool unsavedChanges;
        public List<Constructor> Inventory

        {
            get { return inventory; }
            set { inventory = value; }
        }
        public bool UnsavedChanges
        {
            get { return unsavedChanges; }
            set { unsavedChanges = value; }
        }
       
        public bool CheckStock(int stock)
        {
            bool isValid = true;
            foreach (char character in Convert.ToString(stock))
            {
                isValid = isValid && char.IsNumber(character);
            }
            return isValid && stock > 0;
        }

        /*public bool CheckStock(int stock)
        {
            string str = Convert.ToString(stock);

            bool IsValid = true;
            foreach (char character in str)
            {
                IsValid = IsValid && char.IsLetter(character);
            }
            return IsValid;
         
            
        }*/

        public bool CheckCode(string code)
        {
            bool isValid = true;
            foreach (char character in code)
            {
                isValid = isValid && char.IsNumber(character);
            }
            return isValid && code.Length == 5;
        }

        public bool CheckPrice(string price)
        {
            bool isValid = true;
            foreach (char character in price)
            {
                isValid = isValid && (char.IsNumber(character) || character.Equals('.'));
            }
            return isValid;
        }


        public bool ValidationChecks(string code, string price, int stocknum)
        {
            if (!CheckCode(code))
            {
                MessageBox.Show("Invalid ID!");
                return false;
            }
            if (!CheckPrice(price))
            {
                MessageBox.Show("Invalid Price!");
                return false;
            }
            if (!CheckStock(stocknum))
            {
                MessageBox.Show("Error");
                return false;
            }

            return true;
        }

        public bool Empty(TextBox txt)
        {
            return txt.Text.Equals ("");
        }

        public Constructor Add(int code, string name, double price, int stock, double sales)
        {
            Constructor a = new Constructor(code.ToString(), name, price, stock);
            return a;
        }

        public void makeList()
        {
            dataGridView1.Rows.Clear();
            foreach (Constructor item in inventory)
            {
                dataGridView1.Rows.Add(item.Code, item.Name, String.Format("{0:N2}", item.Price), item.InventoryCount, item.GetSales());
            }

            
            
        }

        public bool find(string code)
        {
            return inventory.Where(s => s.Code == code).Count() == 1;
        }


        public Form1()
        {
            InitializeComponent();

            inventory = new List<Constructor>();
            unsavedChanges = false;

            try
            {
                FileStream fs = new FileStream(@"storage.txt", FileMode.Open);
                StreamReader reader = new StreamReader(fs);


                while (!reader.EndOfStream)
                {
                    

                    string text = reader.ReadLine();
                    string[] words = text.Split(',');
                   
                    dataGridView1.Rows.Add(words[0], words[1], String.Format("{0:N2}", double.Parse(words[2])), words[3], string.Format("{0:N2}", double.Parse(words[4])));

                    //dataGridView1.Rows.Add("hi", "Hello", "z", "sup", "boom");
                    //dataGridView1.Rows.Add("CS");
                    

                       
                
                    
                    //for (int i = 0; i < text.Length; i++)
                    //{
                        //dataGridView1[i, row].Value = words[i];
                    //}
                    //row++;
                }

                reader.Close();
                fs.Close();
            }
            catch (Exception) { MessageBox.Show("Error!"); }

            
            

            //save file
            /*try
            {
                FileStream fs = new FileStream(@"items.txt", FileMode.Open);
                StreamReader reader = new StreamReader(fs);

                while (!reader.EndOfStream)
                {
                    string text = reader.ReadLine();
                    string[] input = text.Split(',');
                    Add(int.Parse(input[0]), input[1], double.Parse(input[2]), int.Parse(input[3]), double.Parse(input[4]));
                }
                reader.Close();
                fs.Close();
            }
            catch (Exception) { }*/


            

            

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Addform addform = new Addform(this);
            addform.ShowDialog();
            //Constructor c1 = new Constructor(addform.Code, addform.CName, addform.Price, addform.InventoryCount);
            //inventory.Add(c1);
          
            
               // dataGridView1.Rows.Add(inventory[0].Code, inventory[0].Name, inventory[0].Price, inventory[0].InventoryCount, inventory[0].GetSales());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Editform editForm = new Editform();
            editForm.GetName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            editForm.GetPrice = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            
            editForm.ShowDialog();

            string getPrice = String.Format("{0:N2}", double.Parse(editForm.GetPrice));
            editForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[1].Value = editForm.GetName);
            editForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[2].Value = getPrice);
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string price = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            DialogResult r = MessageBox.Show(String.Format("Are you sure you want to delete {0} ({1:N2})?", name, double.Parse(price)), "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                inventory.Clear();
            }
            if (r == DialogResult.No)
            {
                this.DialogResult = DialogResult.OK;
            }

            /*dataGridView1.Rows.RemoveAt(0);*/
            /*MessageBox.Show("Are you sure you want to delete " + dataGridView1.SelectedRows[0].Cells[1].Value + dataGridView1.SelectedRows[0].Cells[2].Value);*/
            

        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            /*int outPrice;
            string str;
            string price;
            int answer;
            string firstStock;
            double salesAnswer;
            Sellform sellForm = new Sellform();

            sellForm.ShowDialog();
            string quantity = sellForm.Getstock;

            str = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[2].Value.ToString();
            firstStock = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[3].Value.ToString();
            price = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[2].Value.ToString();
            answer = int.Parse(str) - int.Parse(quantity);
            String.Format("{0:C}", salesAnswer = double.Parse(firstStock) + (Convert.ToInt32(quantity) * double.Parse(price)));

            if (int.TryParse(sellForm.Getstock, out outPrice))
            {
                if (Convert.ToInt32(sellForm.Getstock) < 0)
                {
                    MessageBox.Show("Error: must be positive");
                }
                if (Convert.ToInt32(firstStock) < Convert.ToInt32(quantity))
                {
                    MessageBox.Show("Error: quantity can not be more than stock");
                }
                else if (Convert.ToInt32(sellForm.Getstock) > 0)
                {
                    sellForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[3].Value = answer);
                    sellForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[4].Value = salesAnswer);

                    MessageBox.Show("Item sold!");
                }
                
            }
            else
            {
                MessageBox.Show("Error: Not a number!");
            }*/

            Sellform sellForm = new Sellform();
            string price = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string stock = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string sales = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

            sellForm.ShowDialog();

            
            string quantity = sellForm.Getstock;
            int outPrice;
            /*int answer = int.Parse(stock) - int.Parse(quantity);*/
            /*double salesAnswer = double.Parse(quantity) * double.Parse(price);*/
            int answer;
            

            if (int.TryParse(quantity, out outPrice))
            {
                if (int.Parse(quantity) <= 0)
                {
                    MessageBox.Show("Error: must be positive");
                }
                else if (int.Parse(quantity) > int.Parse(stock))
                {
                    MessageBox.Show("Error: quantity can not be more than stock");
                }
                else if (Convert.ToInt32(sellForm.Getstock) > 0)
                {
                    sellForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[3].Value = int.Parse(stock) - int.Parse(quantity));
                    sellForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[4].Value = String.Format("{0:N2}", double.Parse(sales) + (double.Parse(quantity) * double.Parse(price))));
                    MessageBox.Show("Item sold!");
                    this.Refresh();
                    
                }
            }
            else if (string.IsNullOrEmpty(quantity))
            {
                sellForm.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Error: Must be a number");
            }
            
            
            
            
            

            

            /*string str;
            Sellform sellForm = new Sellform();

            str = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[3].Value.ToString();
            
            sellForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[3].Value = 

            sellForm.ShowDialog();*/
        }

        private void btnExit_Click(object sender, EventArgs e)
         
        {
            try
            {
                
                FileStream fs = new FileStream(@"storage.txt", FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);

                

                /*for (int i = 0; i < Int32.MaxValue; i++)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4}", dataGridView1);
                }*/

            

                foreach(Constructor i in inventory)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4}", i.Code.ToString(), i.Name.ToString(), i.Price.ToString(), i.InventoryCount.ToString(), i.GetSales());
                }
                //writer.WriteLine("{0},{1},{2},{3},{4}", dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                //writer.Flush();
                writer.Close();
                fs.Close();
            }
            catch (Exception) { }

            Close();
    


            /*try
            {
                FileStream fs = new FileStream(@"items.txt", FileMode.Create);
                StreamWriter writeout = new StreamWriter(fs);
                foreach (Constructor i in inventory)
                {
                    if (i != null)
                    {
                        writeout.WriteLine("{0}, {1}, {2}, {3}, {4}", i.Code.ToString(), i.Price.ToString(), i.Name.ToString(), i.InventoryCount.ToString(), i.GetSales());
                    }
                }
                writeout.Close();
                fs.Close();
            }
            catch (Exception) { }   

            this.Close();*/
        }

        private void btnRestock_Click(object sender, EventArgs e)
        {
            int answer;
            Restockform r = new Restockform();

            r.ShowDialog();

            string stock = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string amount = r.GetRestock; 
            

                if (r.DialogResult == DialogResult.OK)
                {
                    answer = int.Parse(stock) + int.Parse(amount);
                    MessageBox.Show("Updated!");
                    r.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[3].Value = answer);

                }

            
            /*Restockform restockForm = new Restockform();

            restockForm.ShowDialog();

            string stock = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string quantity = restockForm.GetRestock;
            int answer;


            answer = (int.Parse(stock) + int.Parse(quantity));


            if (restockForm.DialogResult == DialogResult.OK)
            {
                restockForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[3].Value = answer);
                MessageBox.Show("Updated!");
                
            }
            restockForm.DialogResult = DialogResult.OK;*/
            
            /*if (int.Parse(quantity) <= 0)
            {
                MessageBox.Show("Error: Must be positive", "Error");
            }
            else  
            {
                restockForm.DialogResult.Equals(dataGridView1.SelectedRows[0].Cells[3].Value = answer);
                MessageBox.Show("Updated");
            }*/
            


        }
       

        

        
    }
}
