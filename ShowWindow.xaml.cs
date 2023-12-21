using OhtaPark.Entities;
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

namespace OhtaPark
{
    /// <summary>
    /// Логика взаимодействия для ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        string OpenedObj = "";
        public ShowWindow()
        {
            InitializeComponent();
        }
        // Обработчики кнопок
        // Добавление, Редактирование и Удаление
        // Суть: С помощью полученной информации о отображаемой таблице, 
        // открываем окно FunctionalWindow (точнее один из конструкторов) и вводим или редактируем данные
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OpenedObj == "Clients")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "AddClient";
                // Новая запись
                var currentClient = new Entities.Client();
                App.DateBase.Clients.Add(currentClient);
                string TitleC = "Добавление";
                var addWin = new FunctionalWindow(App.DateBase, currentClient, TitleC);
                addWin.ShowDialog();
                AppTable.ItemsSource = App.DateBase.Clients.ToList();
            }
            if (OpenedObj == "Orders")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "AddOrder";
                // Новая запись
                var currentOrder = new Entities.Order();
                App.DateBase.Orders.Add(currentOrder);
                string TitleO = "Добавление";
                var addWin = new FunctionalWindow(App.DateBase, currentOrder, TitleO);
                addWin.ShowDialog();
                AppTable.ItemsSource = App.DateBase.Clients.ToList();
            }
            if (OpenedObj == "Services")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "AddService";
                // Новая запись
                var currentService = new Entities.Service();
                App.DateBase.Services.Add(currentService);
                string TitleService = "Добавление";
                var addWin = new FunctionalWindow(App.DateBase, currentService, TitleService);
            }
            if (OpenedObj == "Staff")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "AddStaff";
                // Новая запись
                var currentStaff = new Entities.Staff();
                App.DateBase.Staffs.Add(currentStaff);
                string TitleSt = "Добавление";
                var addWin = new FunctionalWindow(App.DateBase, currentStaff, TitleSt);
                AppTable.ItemsSource = App.DateBase.Clients.ToList();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OpenedObj == "Clients")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "AddClient";
                // Новая запись
                var currentClient = AppTable.SelectedItem as Entities.Client;
                if (currentClient == null)
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    string TitleC = "Редактирование";
                    var editWin = new FunctionalWindow(App.DateBase, currentClient, TitleC);
                    editWin.ShowDialog();
                    AppTable.ItemsSource = App.DateBase.Clients.ToList();
                }
            }
            if (OpenedObj == "Orders")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "EditOrder";
                // Новая запись
                var currentOrder = AppTable.SelectedItem as Entities.Order;
                if (currentOrder == null)
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    string TitleO = "Редактирование";
                    var editWin = new FunctionalWindow(App.DateBase, currentOrder, TitleO);
                    editWin.ShowDialog();
                    AppTable.ItemsSource = App.DateBase.Orders.ToList();
                }
            }
            if (OpenedObj == "Services")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "EditService";
                // Новая запись
                var currentService = AppTable.SelectedItem as Entities.Service;
                if (currentService == null)
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    string TitleService = "Редактирование";
                    var editWin = new FunctionalWindow(App.DateBase, currentService, TitleService);
                    editWin.ShowDialog();
                    AppTable.ItemsSource = App.DateBase.Services.ToList();
                }
            }
            if (OpenedObj == "Staff")
            {
                // Записываем для FunctionalWindow тип действия
                Properties.Settings.Default.FuncType = "EditStaff";
                // Новая запись
                var currentStaff = AppTable.SelectedItem as Entities.Staff;
                if (currentStaff == null)
                {
                    MessageBox.Show("Выберите строку для редактирования!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    string TitleSt = "Редактирование";
                    var editWin = new FunctionalWindow(App.DateBase, currentStaff, TitleSt);
                    editWin.ShowDialog();
                    AppTable.ItemsSource = App.DateBase.Staffs.ToList();
                }
            }
        }
        
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OpenedObj == "Clients")
            {
                var selectedRow = AppTable.SelectedItem as Entities.Client;
                if (selectedRow == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        App.DateBase.Clients.Remove(selectedRow);
                        App.DateBase.SaveChanges();
                        AppTable.ItemsSource = App.DateBase.Clients.ToList();
                    }
                }
            }
            if (OpenedObj == "Orders")
            {
                var selectedRow = AppTable.SelectedItem as Entities.Order;
                if (selectedRow == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        App.DateBase.Orders.Remove(selectedRow);
                        App.DateBase.SaveChanges();
                        AppTable.ItemsSource = App.DateBase.Orders.ToList();
                    }
                }
            }
            if (OpenedObj == "Services")
            {
                var selectedRow = AppTable.SelectedItem as Entities.Service;
                if (selectedRow == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        App.DateBase.Services.Remove(selectedRow);
                        App.DateBase.SaveChanges();
                        AppTable.ItemsSource = App.DateBase.Services.ToList();
                    }
                }
            }
            if (OpenedObj == "Staff")
            {
                var selectedRow = AppTable.SelectedItem as Entities.Staff;
                if (selectedRow == null)
                {
                    MessageBox.Show("Не выбрана строка для удаления!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        App.DateBase.Staffs.Remove(selectedRow);
                        App.DateBase.SaveChanges();
                        AppTable.ItemsSource = App.DateBase.Staffs.ToList();
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Получаем из settings сведения о таблицее, которую необходимо открыть
            OpenedObj = Properties.Settings.Default.ShowingObj;
            // Генерируем столбцы для соответсвующей таблицы и выполняем их Binding к EF
            // Задаем Title для окна
            if (OpenedObj == "Clients")
            {
                this.Title = "Охта Парк - Клиенты";
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Фамилия",
                    Binding = new Binding("Surname")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Имя",
                    Binding = new Binding("Name")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Отчество",
                    Binding = new Binding("Отчество")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Email",
                    Binding = new Binding("Email")
                });
                // Заполняем DataGrid
                AppTable.ItemsSource = App.DateBase.Clients.ToList();
            }
            if (OpenedObj == "Orders")
            {
                this.Title = "Охта Парк - Заказы";
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID",
                    Binding = new Binding("ID")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Код заказа",
                    Binding = new Binding("OrderCode")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата создания",
                    Binding = new Binding("CreateDate")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Время заказа",
                    Binding = new Binding("OrderTime")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Код клиента",
                    Binding = new Binding("Client.Code")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Статус",
                    Binding = new Binding("Status.Status1")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Дата закрытия",
                    Binding = new Binding("CloseDate")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Время проката (в часах)",
                    Binding = new Binding("RentalTime")
                });
                AppTable.ItemsSource = App.DateBase.Orders.ToList();
            }
            if (OpenedObj == "Services")
            {
                this.Title = "Охта Парк - Услуги";
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID",
                    Binding = new Binding("ID")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Название услуги",
                    Binding = new Binding("ServiceName")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Код услуги",
                    Binding = new Binding("ServiceCode")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Время проката",
                    Binding = new Binding("RentalTime")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Стоимость проката в час",
                    Binding = new Binding("CostPerHour")
                });
                AppTable.ItemsSource = App.DateBase.Services.ToList();
            }
            if (OpenedObj == "Staff")
            {
                this.Title = "Охта Парк - Сотрудники";
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "ID",
                    Binding = new Binding("ID")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Фамилия",
                    Binding = new Binding("Surname")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Имя",
                    Binding = new Binding("Name")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Отчество",
                    Binding = new Binding("Отчество")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Логин",
                    Binding = new Binding("Login")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Пароль",
                    Binding = new Binding("Password")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Должность",
                    Binding = new Binding("Post.Title")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Последний вход",
                    Binding = new Binding("LastAuth")
                });
                AppTable.Columns.Add(new DataGridTextColumn
                {
                    Header = "Тип входа",
                    Binding = new Binding("AuthType")
                });
                AppTable.ItemsSource = App.DateBase.Staffs;
            }
        }
        // Обнуляем параметр TIme в settings
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Time = "";
        }
    }
}
