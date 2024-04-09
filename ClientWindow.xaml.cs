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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        Заявки request = new Заявки();
        int clientID;
        public ClientWindow(int code)
        {
            InitializeComponent();
            DataContext = request;
            clientID = code;
            оборудованиеBox.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Оборудование.ToList();
            неисправностьBox.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Неисправности.ToList();
            Пользователь manager = TechnoDatabaseEntities.GetTechnoDatabase().Пользователь.Where(u => u.Код_роли == 2).FirstOrDefault();
            string manageNumber = manager.Телефон;
            manageNumBox.Text = "+" + manageNumber;
            requestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(u=>u.Код_клиента==clientID).ToList();
        }
        //добавление заявки
        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            //добавление заявки
            request.Дата = DateTime.Today;
            request.Код_клиента = clientID;
            TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Add(request);
            //сохранение заявки
            try
            {
                TechnoDatabaseEntities.GetTechnoDatabase().SaveChanges();
                MessageBox.Show("Заявка успешно подана!");
                //обновление таблицы заявок
                requestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(u => u.Код_клиента == clientID).ToList();
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
    }
}
