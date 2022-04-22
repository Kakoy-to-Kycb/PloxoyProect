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
    /// Логика взаимодействия для AddPdge.xaml
    /// </summary>
    public partial class AddPdge : Page
    {
        Product prod = new Product();

        public AddPdge(Product selectedProd)
        {
            InitializeComponent();

            if (selectedProd != null)
                prod = selectedProd;

            DataContext = prod;

            cbCategory.ItemsSource = TradeEntities.GetContext().Categories.ToList();
            cbManufactory.ItemsSource = TradeEntities.GetContext().Manufactures.ToList();
            cbSupplier.ItemsSource = TradeEntities.GetContext().Supplier.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(prod.ProductArticleNumber)) 
                errors.AppendLine("Укажите артикул");
            if (string.IsNullOrWhiteSpace(prod.ProductName))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(prod.ProductDescription))
                errors.AppendLine("Отсутствует описание");
            if (prod.ProductCost < 1)
                errors.AppendLine("Цена не может быть меньше 1");
            if (prod.ProductQuantityInStock < 0)
                errors.AppendLine("Кол-во на складе не может быть меньше 0");
            if (prod.ProductDiscountMax < 0)
                errors.AppendLine("Максимальная скидка не может быть меньше 0");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),"Внимание", MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }

           /* if (prod.ProductID == 0)
                TradeEntities.GetContext().Product.Add(prod);

            try
            {
                TradeEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена","Уведомление");
                Manager.MainFrame.GoBack();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
        }


        private void tbCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void tbDiscountAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void tbQuantityInStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void tbDiscountMax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }
    }
}
