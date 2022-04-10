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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private User user = new User();

        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = user;
            cbRole.ItemsSource = TradeEntities.GetContext().Role.ToList();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(user.UserSurname))
                errors.AppendLine("Укажите Фамилию");
            if (string.IsNullOrWhiteSpace(user.UserName))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(user.UserPatronymic))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(user.UserLogin))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(user.UserPassword))
                errors.AppendLine("Придумайте пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (user.UserID == 0)
                TradeEntities.GetContext().User.Add(user);

            try
            {
                TradeEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранина", "Уведомление");
                new LoginWindow().Show();
                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}
