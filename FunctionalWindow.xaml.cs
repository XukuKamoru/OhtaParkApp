using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Configuration;

namespace OhtaPark
{
    /// <summary>
    /// Логика взаимодействия для FunctionalWindow.xaml
    /// </summary>
    public partial class FunctionalWindow : Window
    {
        Entities.OhtaParkEntities database;
        string ActionType = "";

        string lastAddedService = "";

        public FunctionalWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Получаем данные о типе действия (добавление, редактирование)
            ActionType = Properties.Settings.Default.FuncType;
            if (ActionType == "AddClient")
            {
                Client.Visibility = Visibility.Visible;
            }
            if (ActionType == "AddOrder")
            {
                Order.Visibility = Visibility.Visible;
            }
            if (ActionType == "AddService")
            {
                Service.Visibility = Visibility.Visible;
            }
            if (ActionType == "AddStaff")
            {
                Staff.Visibility = Visibility.Visible;
            }
        }
        // bool элементы с названиями IsNoFirstDonw_названиеTextBox`a служат для очистки примеров
        // и предотвращают удаление введенных данных при повторном нажатии на TextBox`ы

        // Удаляем из Label ChoicedServices последнюю добавленную услугу
        private void deleteService_Click(object sender, RoutedEventArgs e)
        {
            int deletedLength = lastAddedService.Length;
            string Copy = Convert.ToString(ChoicedServices.Content);
            int deletedIndex = Copy.IndexOf(lastAddedService);
            string Result = Copy.Remove(deletedIndex, deletedLength);
        }
        // Сохраняем
        private void addOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            database.SaveChanges();
            this.Close();
        }

        bool IsNoFirstDown_fullNameBox = false;
        private void fullNameBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_fullNameBox)
            {
                fullNameBox.Text = "";
                fullNameBox.Foreground = Brushes.Black;
                IsNoFirstDown_fullNameBox = true;
            }
            else
            {

            }
        }
        bool IsNoFirstDown_passportData = false;
        private void passportData_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_passportData)
            {
                passportData.Text = "";
                passportData.Foreground = Brushes.Black;
            }
            else
            {

            }
        }

        private void dobChoicer_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dobBox.Text = dobChoicer.SelectedDate.ToString();
        }
        bool IsNoFirstDown_emailBox = false;
        private void emailBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_emailBox)
            {
                emailBox.Text = "";
                emailBox.Foreground = Brushes.Black;
            }
            else
            {

            }
        }
        bool IsNoFirstDown_passwordBox = false;
        private void passwordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_passwordBox)
            {
                passwordBox.Text = "";
                passwordBox.Foreground = Brushes.Black;
            }
        }
        // Сохраняем
        private void addClientBtn_Click(object sender, RoutedEventArgs e)
        {
            database.SaveChanges();
            this.Close();
        }
        bool IsNoFirstDown_serviceCodeBox = false;
        private void serviceCodeBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_serviceCodeBox)
            {
                serviceCodeBox.Text = "";
                serviceCodeBox.Foreground = Brushes.Black;
            }
        }
        bool IsNoFirstDown_serviceRentalTime = false;
        private void serviceRentalBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_serviceRentalTime)
            {
                serviceRentalBox.Text = "";
                serviceRentalBox.Foreground = Brushes.Black;
            }
        }
        bool IsNoFirstDown_serviceCostBox = false;
        private void serviceCostBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_serviceCostBox)
            {
                serviceCostBox.Text = "";
                serviceCostBox.Foreground = Brushes.Black;
            }
        }
        // Сохраняем
        private void addServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            database.SaveChanges();
            this.Close();
        }
        bool IsNoFirstDown_staffIdBox = false;
        private void staffIdBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_staffIdBox)
            {
                staffIdBox.Text = "";
                staffIdBox.Foreground = Brushes.Black;
            }
        }
        bool IsNoFirstDown_staffFulNameBox = false;
        private void staffFulNameBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_staffFulNameBox)
            {
                staffFulNameBox.Text = "";
                staffFulNameBox.Foreground = Brushes.Black;
            }
        }
        bool IsNoFirstDown_staffEmailBox = false;
        private void staffEmailBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_staffEmailBox)
            {
                staffEmailBox.Text = "";
                staffEmailBox.Foreground = Brushes.Black;
            }
        }
        bool IsNoFirstDown_staffPasswordBox = false;
        private void staffpasswordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsNoFirstDown_staffPasswordBox)
            {
                staffpasswordBox.Text = "";
                staffpasswordBox.Foreground = Brushes.Black;
            }
        }
        // Сохраняем фотографию
        byte[] image_bytes;
        string[] FIO;
        string SurN, N, P;
        private void selectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
            openFileDialog.ShowDialog(); // показываем
            image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла
            Uri uri = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
            BitmapImage image = new BitmapImage();
            image.UriSource = uri;
            staffImage.Source = image;
            string FromTB = staffFulNameBox.Text;
            FIO = FromTB.Split(' ');
            SurN = FIO[0];
            N = FIO[1];
            P = FIO[2];
        }
        // Сохраняем
        private void addStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            database.SaveChanges();
            string connectionString = ConfigurationManager.ConnectionStrings["OhtaParkEntites"].ConnectionString; // строка подключения
            using (SqlConnection connection = new SqlConnection(connectionString)) // создаем подключение
            {
                connection.Open(); // откроем подключение
                SqlCommand command = new SqlCommand(); // создадим запрос
                command.Connection = connection; // дадим запросу подключение
                command.CommandText = @"UPDATE Staff SET Photo = '" + image_bytes + "' WHERE Surname = '" + SurN + "' AND Name = '" + N + "' AND Patronymic = '" + P + "' AND Login = '" + staffEmailBox.Text + "' "; // пропишем запрос
                command.ExecuteNonQuery(); // запустим
            }
            this.Close();
        }
        // Конструкторы для реализации добавления новой записи в выбранную таблицу
        public FunctionalWindow(Entities.OhtaParkEntities DataBase, Entities.Client currentClient, string TitleC)
        {
            InitializeComponent();
            this.database = DataBase;
            this.DataContext = currentClient;
            this.Title = TitleC;
            userCmb.ItemsSource = App.DateBase.Clients.ToList();
            serviceCmb.ItemsSource = App.DateBase.Services.ToList();
            statusCmb.ItemsSource = App.DateBase.Statuses.ToList();
        }
        public FunctionalWindow(Entities.OhtaParkEntities DataBase, Entities.Order currentOrder, string TitleO)
        {
            this.database = DataBase;
            this.DataContext = currentOrder;
            this.Title = TitleO;
        }
        public FunctionalWindow(Entities.OhtaParkEntities DataBase, Entities.Service currentService, string TitleService)
        {
            this.database = DataBase;
            this.DataContext = currentService;
            this.Title = TitleService;
        }
        public FunctionalWindow(Entities.OhtaParkEntities DataBase, Entities.Staff currentStaff, string TitleSt)
        {
            this.database = DataBase;
            this.DataContext = currentStaff;
            this.Title = TitleSt;
            staffPostCmb.ItemsSource = App.DateBase.Posts.ToList();
        }

        private void cancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете покинуть окно - Добавление?\n" +
                "Не сохраненные данные будут утеряны.", "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                this.Close();
            }
            else
            {
                // Остаемся на окне
            }
        }

        private void cancelClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете покинуть окно - Добавление?\n" +
                "Не сохраненные данные будут утеряны.", "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                this.Close();
            }
            else
            {
                // Остаемся на окне
            }
        }

        private void cancelServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете покинуть окно - Добавление?\n" +
                "Не сохраненные данные будут утеряны.", "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                this.Close();
            }
            else
            {
                // Остаемся на окне
            }
        }

        private void cancelStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно желаете покинуть окно - Добавление?\n" +
                "Не сохраненные данные будут утеряны.", "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                this.Close();
            }
            else
            {
                // Остаемся на окне
            }
        }
        // Добавляем в Label ChoicedServices ID услуги
        private void serviceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ChoicedServices.Content += Convert.ToString(serviceCmb.SelectedValue) + ", ";
            lastAddedService = Convert.ToString(serviceCmb.SelectedValue);
        }

        private void statusCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void userCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var userFN = App.DateBase.Clients.FirstOrDefault(x => x.Code == Convert.ToInt32(userCmb.SelectedValue));
            string S, N, P;
            S = DeleteSpaces(userFN.Surname);
            N = DeleteSpaces(userFN.Name);
            P = DeleteSpaces(userFN.Patronymic);
            userFullName.Content = S + " " + N + " " + P;
        }

        private string DeleteSpaces(string S)
        {
            string result = S.Replace(" ", "");
            return result;
        }
    }
}
