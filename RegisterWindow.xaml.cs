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

namespace TechnoServiceProject
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private Пользователь user = new Пользователь();
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = user;
        }
        //кнопка регистрации
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";
            if (loginBox.Text.Length < 8)
            {
                errors += "Слишком короткий логин! Введите минимум 8 символов. \n";
                loginBox.Focus();
            }
            else if (passBox.Text.Length < 7)
            {
                errors += "Слишком короткий пароль! Введите минимум 7 символов. \n";
                passBox.Focus();
            }
            else if (!loginBox.Text.Contains("@") || !loginBox.Text.Contains("."))
            {
                errors += "Неправильный логин. Должен содержать <<@>> и <<.>>\n";
                passBox.Focus();
            }
            else
            {
                //добавление пользователя
                user.Код_роли = 3; //клиент
                TechnoDatabaseEntities.GetTechnoDatabase().Пользователь.Add(user);
                //сохранение пользователя в базе
                try
                {
                    TechnoDatabaseEntities.GetTechnoDatabase().SaveChanges();
                    MessageBox.Show("Регистрация успешна!");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    mainWindow.loginBox.Text = user.Логин;
                    mainWindow.passBox.Password = user.Пароль;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //кнопка назад
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.loginBox.Focus();
            this.Close();
        }

        private void cardBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //написание только цифр в карте
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void phoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //написание только цифр в телефоне
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
