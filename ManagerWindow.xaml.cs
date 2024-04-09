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
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        int managerID;
        public ManagerWindow(int code)
        {
            InitializeComponent();
            managerID = code;
            DataContext = allRequestTable.SelectedItem as Заявки;
            allRequestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.ToList();
            allUsersTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Пользователь.ToList();


            //статистика
            //количество заявок
            int count = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(z=>z.Код_статуса==2).Count();
            countBox.Text = count.ToString();
        }
        //изменить дни
        private void daysChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            int days = Convert.ToInt32(daysBox.Text);
            Заявки выбраннаяЗаявка = allRequestTable.SelectedItem as Заявки;
            выбраннаяЗаявка.Дни_выполнения = days;
            //сохранение заявки
            try
            {
                TechnoDatabaseEntities.GetTechnoDatabase().SaveChanges();
                MessageBox.Show("Дни изменены!");
                //обновление таблицы
                allRequestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //назад
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.loginBox.Focus();
            this.Close();
        }
        //окно регистрации пользователя (не только клиента)
        private void userRegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerRegisterWindow managerRegisterWindow = new ManagerRegisterWindow();
            managerRegisterWindow.Show();
        }

        //обновление таблицы
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            allUsersTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Пользователь.ToList();
        }
    }
}
