using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AllPage.xaml
    /// </summary>
    public partial class AllPage : Page
    {
        public AllPage()
        {
            InitializeComponent();
            dgProd.ItemsSource = TradeEntities.GetContext().Product.ToList();

            var manufacturers = TradeEntities.GetContext().Manufactures.ToList();
            manufacturers.Insert(0, new Manufactures { ManufactoryName = "Все производители" });
            cbManufacturers.ItemsSource = manufacturers;
            cbManufacturers.SelectedValuePath = "ManufactureID";

            cbManufacturers.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPdge(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                TradeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                dgProd.ItemsSource = TradeEntities.GetContext().Product.ToList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPdge((sender as Button).DataContext as Product));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var prodRemote = dgProd.SelectedItems.Cast<Product>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TradeEntities.GetContext().Product.RemoveRange(prodRemote);
                    TradeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dgProd.ItemsSource = TradeEntities.GetContext().Product.ToList();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void UpdateProducts()
        {
            var products = TradeEntities.GetContext().Product.ToList();

            //Для поиска
            if (!string.IsNullOrEmpty(tbSearch.Text))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            //Для производителей
            if (cbManufacturers.SelectedIndex > 0)
            {
                products = products.Where(p => p.ProductManufacturer == int.Parse(cbManufacturers.SelectedValue.ToString())).ToList();
            }
            //Для сортировки (на каждый пункт списка свой case!!)
            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        products = products.OrderBy(p => p.ProductCost).ToList();
                        break;
                    case 2:
                        products = products.OrderByDescending(p => p.ProductCost).ToList();
                        break;
                }
            }
            //Обновление данных в гриде
            dgProd.ItemsSource = products;
        }
    }
}
