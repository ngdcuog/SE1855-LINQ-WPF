using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;
using Services;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductService iproductService;
        private readonly ICategoryService icategoryService;

        public MainWindow()
        {
            InitializeComponent();
            iproductService = new ProductService();
            icategoryService = new CategoryService();
        }

        public void LoadCategoryList()
        {
            try
            {
                var catList = icategoryService.GetCategories();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of categories");
            }
        }


        public void LoadProductList()
        {
            try
            {
                var productList = iproductService.GetProducts();
                dgData.ItemsSource = null;
                dgData.ItemsSource = productList; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading product list");
            }
            finally
            {
                resetInput();
            }
        }


        private void resetInput()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnitsInStock.Text = "";
            cboCategory.SelectedValue = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newId = iproductService.GetProducts().Count > 0
                    ? iproductService.GetProducts().Max(p => p.ProductId) + 1
                    : 1;

                Product product = new Product
                {
                    ProductId = newId,
                    ProductName = txtProductName.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    UnitInStock = short.Parse(txtUnitsInStock.Text),
                    CategoryId = int.Parse(cboCategory.SelectedValue.ToString())
                };

                iproductService.SaveProduct(product);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }


        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Product selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtPrice.Text = selectedProduct.UnitPrice?.ToString();
                txtUnitsInStock.Text = selectedProduct.UnitInStock?.ToString();
                cboCategory.SelectedValue = selectedProduct.CategoryId;
            }
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Product product = new Product
                    {
                        ProductId = int.Parse(txtProductID.Text), 
                        ProductName = txtProductName.Text,
                        UnitPrice = decimal.Parse(txtPrice.Text),
                        UnitInStock = short.Parse(txtUnitsInStock.Text),
                        CategoryId = int.Parse(cboCategory.SelectedValue.ToString())
                    };
                    iproductService.UpdateProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Product product = new Product
                    {
                        ProductId = int.Parse(txtProductID.Text),
                        ProductName = txtProductName.Text,
                        UnitPrice = decimal.Parse(txtPrice.Text),
                        UnitInStock = short.Parse(txtUnitsInStock.Text),
                        CategoryId = int.Parse(cboCategory.SelectedValue.ToString())
                    };
                    iproductService.DeleteProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();

            }
        }
    }
}