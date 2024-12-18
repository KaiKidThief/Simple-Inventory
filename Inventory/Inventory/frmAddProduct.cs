using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace Inventory
{
    public partial class Form1 : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        private BindingSource showProduct;

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory =
            {
                "Beverage",
                "Bread",
                "Canned Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Others"
            };

            foreach (string c in ListOfProductCategory)
            {
                cbCategory.Items.Add(c);
            }

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProduct.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));

                gridViewProductList.DataSource = showProduct;
            }
            catch (NumberFormatException nfe)
            {
                MessageBox.Show("Error Number Format" + nfe);
            }
            catch (StringFormatException sfe)
            {
                MessageBox.Show("Error String Format" + sfe);
            }
            catch (CurrencyFormatException cfe)
            {
                MessageBox.Show("Error Currency Format" + cfe);
            }
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new StringFormatException("Error!! the product name that you type is not format");
            }
                
                return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                throw new NumberFormatException("Error!! the quantity that you type is invalid");
            }
                
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException("Error!! the Sellingprice that you type is invalid");
            }
                
                return Convert.ToDouble(price);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();

            showProduct = new BindingSource();
            gridViewProductList.DataSource = showProduct;
        }




        class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }

        class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }

        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }

    }
}