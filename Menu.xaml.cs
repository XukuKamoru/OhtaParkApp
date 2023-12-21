using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.Windows.Threading;

namespace OhtaPark
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        DateTime startTime;
        DateTime now;
        TimeSpan passed;
        public Menu()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();
        }
        // Время в системе
        private void timer_Tick(object sender, EventArgs e)
        {
            now = DateTime.Now;
            passed = now.Subtract(startTime);
            timeInApp.Content = string.Format("{0}:{1}:{2}", passed.Hours, passed.Minutes, passed.Seconds);
        }
        // Обработчики кнопок
        // Общая суть: В settings записываем на какую кнопку нажал пользователь? т.е. какую таблицу открыть (Клиенты, Заказы и т.д.)
        // Открываем окно ShowWindow
        private void clientsOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowingObj = "Clients";
            Properties.Settings.Default.Time = Convert.ToString(timeInApp);
            ShowWindow showWindow = new ShowWindow();
            showWindow.Show();
            this.Close();
        }

        private void servicesOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowingObj = "Services";
            Properties.Settings.Default.Time = Convert.ToString(timeInApp);
            ShowWindow showWindow = new ShowWindow();
            showWindow.Show();
            this.Close();
        }

        private void ordersOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowingObj = "Orders";
            Properties.Settings.Default.Time = Convert.ToString(timeInApp);
            ShowWindow showWindow = new ShowWindow();
            showWindow.Show();
            this.Close();
        }

        private void newUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowingObj = "Staff";
            Properties.Settings.Default.Time = Convert.ToString(timeInApp);
            ShowWindow showWindow = new ShowWindow();
            showWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startTime = DateTime.Now;
            // Загружаем время

            // Получаем сведения о роли пользователя
            string userType = Properties.Settings.Default.UserType;
            if (userType == "Admin")
            {
                // открываем доступную для администратора кнопку
                newUserBtn.Visibility = Visibility.Visible;
                
            }
            else
            {

            }
        }
        // Возвращение на форму авторизации
        private void outBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        // Обнуляем в settings параметр Time
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Time = "";
        }
    }
}
