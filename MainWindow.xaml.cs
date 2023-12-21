using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace OhtaPark
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int UnsuccessfulAttemptsCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private  void authBtn_Click(object sender, RoutedEventArgs e)
        {
            // Авторизация через PasswordBox(пароль)
            if (IsShow.IsChecked == false)
            {
                if (lBox.Text.Length > 0)
                {
                    if (pBox.Password.Length > 0)
                    {
                        // Если админ
                        var staff = App.DateBase.Staffs.FirstOrDefault(x => x.Login == lBox.Text && x.Passwrod == pBox.Password &&
                        x.PostId == 1);
                        if (staff != null)
                        {
                            // Записываем в settings что авторизовался администратор
                            Properties.Settings.Default.UserType = "Admin";
                            // Получаем имя и отчество, открываем меню
                            string Name = DeleteSpaces(staff.Name);
                            string Patronymic = DeleteSpaces(staff.Patronymic);
                            MessageBox.Show("Здравствуйте, " + Name + " " + Patronymic + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            Hide();
                            Menu menuWindow = new Menu();
                            menuWindow.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            // Если не админ
                            var noAdmStaff = App.DateBase.Staffs.FirstOrDefault(x => x.Login == lBox.Text && x.Passwrod == pBox.Password &&
                                x.PostId > 1);
                            if (noAdmStaff != null)
                            {
                                // Записываем в settings что пользователь не является администратором
                                Properties.Settings.Default.UserType = "NoAdmin";
                                // Получаем имя отчество, открываем меню
                                string Name = DeleteSpaces(staff.Name);
                                string Patronymic = DeleteSpaces(staff.Patronymic);
                                MessageBox.Show("Здравствуйте, " + Name + " " + Patronymic + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                Hide();
                                Menu menuWindow = new Menu();
                                menuWindow.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                if (UnsuccessfulAttemptsCount >= 3)
                                {
                                    Captcha capWindow = new Captcha();
                                    capWindow.Show();
                                }
                                UnsuccessfulAttemptsCount++;
                                MessageBox.Show("Пользователь не обнаружен!" +
                                            "Проверьте правильность введенных данных!" +
                                            "Если у вас нет учетной записи, обратитесь к администратору.", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                           
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста заполните поле для пароля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста заполните поле для логина!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            // Авторизация через TextBox(пароль)
            if (IsShow.IsChecked == true)
            {
                if (lBox.Text.Length > 0)
                {
                    if (pTBox.Text.Length > 0)
                    {
                        var staff = App.DateBase.Staffs.FirstOrDefault(x => x.Login == lBox.Text && x.Passwrod == pTBox.Text &&
                        x.PostId == 1);
                        if (staff != null)
                        {
                            // Записываем в settings что авторизовался администратор
                            Properties.Settings.Default.UserType = "Admin";
                            // Получаем имя и отчество, открываем меню
                            string Name = DeleteSpaces(staff.Name);
                            string Patronymic = DeleteSpaces(staff.Patronymic);
                            MessageBox.Show("Здравствуйте, " + Name + " " + Patronymic + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            Hide();
                            Menu menuWindow = new Menu();
                            menuWindow.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            var noAdmStaff = App.DateBase.Staffs.FirstOrDefault(x => x.Login == lBox.Text && x.Passwrod == pTBox.Text &&
                                x.PostId > 1);
                            if (noAdmStaff != null)
                            {
                                // Записываем в settings что пользователь не является администратором
                                Properties.Settings.Default.UserType = "NoAdmin";
                                // Получаем имя отчество, открываем меню
                                string Name = DeleteSpaces(staff.Name);
                                string Patronymic = DeleteSpaces(staff.Patronymic);
                                MessageBox.Show("Здравствуйте, " + Name + " " + Patronymic + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                Hide();
                                Menu menuWindow = new Menu();
                                menuWindow.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                if (UnsuccessfulAttemptsCount >= 3)
                                {

                                }
                                UnsuccessfulAttemptsCount++;
                                MessageBox.Show("Пользователь не обнаружен!" +
                                            "Проверьте правильность введенных данных!" +
                                            "Если у вас нет учетной записи, обратитесь к администратору.", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста заполните поле для пароля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста заполните поле для логина!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void IsShow_Checked(object sender, RoutedEventArgs e)
        {
            pTBox.Text = "";
            pTBox.Text = pBox.Password;
            pTBox.Visibility = Visibility.Visible;
            pBox.Visibility = Visibility.Hidden;
        }

        private void IsShow_Unchecked(object sender, RoutedEventArgs e)
        {
            pBox.Password = "";
            pBox.Password = pTBox.Text;
            pBox.Visibility = Visibility.Visible;
            pTBox.Visibility = Visibility.Hidden;
        }

        private string DeleteSpaces(string S)
        {
            string result = S.Replace(" ", "");
            return result;
        }
        // Обнуляем в settings параметр Time
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Time = "";
        }
    }
}
