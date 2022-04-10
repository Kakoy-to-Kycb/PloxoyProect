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

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
