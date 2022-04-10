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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userLog = TradeEntities.GetContext().User.FirstOrDefault(x => x.UserLogin == tbUserLogin.Text && x.UserPassword == pbPassword.Password);
                if (userLog == null)
                {
                    MessageBox.Show("Такого пользователя не существует", "Внимание!",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    switch (userLog.UserRole)
                    {
                        case 1: 
                            MessageBox.Show("Здравствуйте, Администратор " + userLog.UserName + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            new MainWindow().Show();
                            Close();
                            break;
                        
                        case 2 :
                            MessageBox.Show("Здравствуйте, Менеджер " + userLog.UserName + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            new AdminGuestWimdow().Show();
                            Close();
                            break;

                        case 3:
                            MessageBox.Show("Здравствуйте, Клиент " + userLog.UserName + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                        default: MessageBox.Show("Проверьте введеные данные!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            new AdminGuestWimdow().Show();
                            Close();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message.ToString() + "Критическая работа приложения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().Show();
            Close();

        }
    }
}
