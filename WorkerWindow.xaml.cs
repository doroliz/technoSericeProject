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
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        int workerID;
        public WorkerWindow(int code)
        {
            InitializeComponent();
            workerID = code;
            DataContext = activeRequestTable.SelectedItem as Заявки;
            notRegisteredTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(r => r.Код_статуса == 0).ToList(); //0 - не выполняется
            activeRequestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(r => r.Код_статуса == 1 && r.Код_исполнителя == workerID).ToList(); //1 - выполняется
        }
        //регистрация заявки
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            Заявки выбраннаяЗаявка = notRegisteredTable.SelectedItem as Заявки;
            //добавление заявки
            выбраннаяЗаявка.Код_статуса = 1; //в процессе
            выбраннаяЗаявка.Код_исполнителя = workerID; //текущий сотрудник
            if (выбраннаяЗаявка.Дни_выполнения == null) выбраннаяЗаявка.Дни_выполнения = 1; //по умолчанию
            //сохранение заявки
            try
            {
                TechnoDatabaseEntities.GetTechnoDatabase().SaveChanges();
                MessageBox.Show("Заявка успешно зарегистрирована!");
                //обновление таблиц
                notRegisteredTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(r => r.Код_статуса == 0).ToList();
                activeRequestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(r => r.Код_статуса == 1 && r.Код_исполнителя == workerID).ToList();

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
        //добавить/изменить комментарий к табл. активных заявок
        private void addCommentBtn_Click(object sender, RoutedEventArgs e)
        {
            string comment = commentBox.Text;
            Заявки выбраннаяЗаявка = activeRequestTable.SelectedItem as Заявки;
            выбраннаяЗаявка.Коммент_исполнителя = comment;
            //сохранение заявки
            try
            {
                TechnoDatabaseEntities.GetTechnoDatabase().SaveChanges();
                MessageBox.Show("Комментарий изменён!");
                //обновление таблицы
                activeRequestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(r => r.Код_статуса == 1 && r.Код_исполнителя == workerID).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //завершить выполнение заявки
        private void endBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите завершить заявку?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Заявки выбраннаяЗаявка = activeRequestTable.SelectedItem as Заявки;
                выбраннаяЗаявка.Код_статуса = 2; //выполнено
                                                 //сохранение заявки
                try
                {
                    TechnoDatabaseEntities.GetTechnoDatabase().SaveChanges();
                    MessageBox.Show("Заявка успешно завершена!");
                    //обновление таблицы активных заявок
                    activeRequestTable.ItemsSource = TechnoDatabaseEntities.GetTechnoDatabase().Заявки.Where(r => r.Код_статуса == 1 && r.Код_исполнителя == workerID).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
