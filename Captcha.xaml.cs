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
using System.Windows.Threading;

namespace OhtaPark
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        int errorCount = 0;
        bool IsCorrect;
        int remainingSeconds = 15;
        DispatcherTimer timer = new DispatcherTimer();
        public Captcha()
        {
            InitializeComponent();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1);
        }
        AppCaptchaMaker captchaCreator = new AppCaptchaMaker();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            capImage.Source = captchaCreator.CreateBitmap(Convert.ToInt32(capImage.Width), Convert.ToInt32(capImage.Height));
        }

        private void checkBtn_Click(object sender, RoutedEventArgs e)
        {
            IsCorrect = captchaCreator.Check(input.Text);
            if (IsCorrect)
            {
                MessageBox.Show("Вы прошли капчу", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                if (errorCount == 3)
                {
                    timer.Start();
                    checkBtn.IsEnabled = false;
                    updateImage.IsEnabled = false;
                    MessageBox.Show("Вы не прошли капчу!\n" +
                        "Капча заблокирована, попробуйте через 15 секунд", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Вы не прошли капчу\n" +
                        "Попыток до блокировки: " + Convert.ToString(3 - errorCount), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    errorCount++;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
            }
            else
            {
                timer.Stop();
                checkBtn.IsEnabled = true;
                updateImage.IsEnabled = true;
                errorCount = 0;
                MessageBox.Show("Капча разблокирована", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void updateImage_Click(object sender, RoutedEventArgs e)
        {
            capImage.Source = captchaCreator.CreateBitmap(Convert.ToInt32(capImage.Width), Convert.ToInt32(capImage.Height));
        }
    }
}
