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

namespace TechnoServiceProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //быстрые проверки
            //для сотрудника
            //loginBox.Text = "sysadmin@.";
            //passBox.Password = "sysadmin01";
            //для клиента
            //loginBox.Text = "client@mail.ru";
            //passBox.Password = "client01";
            //для менеджера
            loginBox.Text = "manager@.";
            passBox.Password = "manager01";
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim().ToLower();
            string password = passBox.Password.Trim();
            string errors = "";
            Пользователь user = null;
            if (login.Length == 0)
            {
                errors += "Введите логин!\n";
                loginBox.Focus();
            }
            else if (password.Length == 0)
            {
                errors += "Введите пароль!\n";
                passBox.Focus();
            }
            else
            {
                using (TechnoDatabaseEntities database = new TechnoDatabaseEntities())
                {
                    user = database.Пользователь.Where(u => u.Логин == login && u.Пароль == password).FirstOrDefault();
                }
                if (user != null)
                {
                    MessageBox.Show("Авторизация успешна! Добро пожаловать, " +user.Имя+"!");
                    if (user.Код_роли == 1)//сотрудник
                    {
                        WorkerWindow workerWindow = new WorkerWindow(user.Код);
                        workerWindow.Show();
                        this.Close();
                    }
                    else if (user.Код_роли == 2)//manager
                    {
                        ManagerWindow managerWindow = new ManagerWindow(user.Код);
                        managerWindow.Show();
                        this.Close();
                    }
                    else if (user.Код_роли == 3) //client
                    {
                        ClientWindow clientWindow = new ClientWindow(user.Код);
                        clientWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Пользователя с таким логином или паролем не существует! Попробуйте снова.");
                    passBox.Clear();
                    passBox.Focus();
                }
            }
        }

        private void regBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
