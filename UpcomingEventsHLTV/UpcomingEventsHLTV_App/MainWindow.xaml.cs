using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UpcomingEventsHLTV_App.Models;
using static OpenQA.Selenium.BiDi.Modules.Script.RemoteValue.WindowProxy;

namespace UpcomingEventsHLTV_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Список турниров
        private List<Tournament> tournaments = new List<Tournament>();
        // Список команд
        private List<Team> teams = new List<Team>();
        
        /// <summary>
        /// Конструктор главного окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            // устанавливаем сегодняшнюю дату
            TodayDateText.Text = DateTime.Today.ToShortDateString();
            LastUpdateText.Text = DefaultSettings.Default.LastUpdate;
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            Close(); // закрываем приложение
        }


        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            string url = "https://www.hltv.org/events";          // ссылка на веб страницу
            tournaments.Clear();                                 // очищаем список турниров    
            Parser parser = new Parser(url, tournaments, teams); // начинаем парсинг
            EventsGrid.ItemsSource = null;                       // обнуляем прошлые данные грида
            EventsGrid.ItemsSource = tournaments;                // запихиваем турниры в грид
            // устанавливаем последнее обновление на сегодня
            DefaultSettings.Default.LastUpdate = DateTime.Today.ToShortDateString();
            LastUpdateText.Text = DefaultSettings.Default.LastUpdate;
            DefaultSettings.Default.Save();
        }

        /// <summary>
        /// При запуске приложения достаются данные из БД по последнему сохранению
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // достаем данные и сохраняем в коллекцию
            using (var db = new HLTV_Context())
            {
                var t = db.Tournaments.ToList();
                foreach (Tournament item in t)
                    tournaments.Add(item);

                var tm = db.Teams.ToList();
                foreach(Team item in tm)
                    teams.Add(item);
            } // Устанавливаем источник данных для грида
            EventsGrid.ItemsSource = tournaments;
        }

        /// <summary>
        /// При выборе турнира показывает какой кликнули
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tournament t = EventsGrid.SelectedItem as Tournament;   // устанавливаем в текстовое
            ChoosedEventText.Text = t.Name;                         // окно название турнира
        }

        /// <summary>
        /// Удаляет все данные из базы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            // очищаем базу данных
            using (var db = new HLTV_Context())
            {
                foreach (Tournament item in db.Tournaments.ToList())
                    db.Remove(item);

                foreach (Team item in db.Teams.ToList())
                    db.Remove(item);

                foreach (Tournament_Team item in db.Tournament_Teams.ToList())
                    db.Remove(item);

                db.SaveChanges();
            }
            EventsGrid.ItemsSource = null; // очищаем грид
        }

        /// <summary>
        ///  Создает новое окно с информацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow(); // создаем окно
            infoWindow.Owner = this;                  // главное окно владелец
            infoWindow.Show();                        // показываем окно
        }

        /// <summary>
        /// Кнопка перехода к другим командам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTeamsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChoosedEventText.Text == null || ChoosedEventText.Text == string.Empty)
            {
                MessageBox.Show("You didnt chose an event to show teams which attend this one");
                return;
            }
            TeamsGrid.ItemsSource = null;
            Tournament tt = null;
            foreach (Tournament item in tournaments)
            {
                if (item.Name == ChoosedEventText.Text)
                {
                    tt = item;
                    break;
                }
            }
            List<Team> t = new List<Team>();
            foreach (var item in teams)
            {
                if (item.Id / 100 == tt.Id)
                {
                    t.Add(item);
                }
            }
            TeamsGrid.ItemsSource = t;
            Scroller.Visibility = Visibility.Collapsed;
            TeamsGrid.Visibility = Visibility.Visible;
            BackToEventsBtn.Visibility = Visibility.Visible;
            ShowTeamsBtn.Visibility = Visibility.Collapsed;  
        }

        /// <summary>
        /// Кнопка возврата к турнирам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToEventsBtn_Click(object sender, RoutedEventArgs e)
        {
            TeamsGrid.ItemsSource = null;
            Scroller.Visibility= Visibility.Visible;
            TeamsGrid.Visibility= Visibility.Collapsed;
            BackToEventsBtn.Visibility= Visibility.Collapsed;
            ShowTeamsBtn.Visibility= Visibility.Visible;
        }

        /// <summary>
        /// кнопка генерации экселевского файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            string PATH = string.Empty;
            // Открываем окно диалога с пользователем.
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Documents (*.xls)|*.xlsx", // Устанавливаем фильтр для файлов
                DefaultExt = "xlsx", // Расширение по умолчанию
                AddExtension = true, // Добавлять расширение, если его не указали
                Title = "Сохраните файл как" // Заголовок окна
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                PATH = saveFileDialog.FileName;
                Excel_Helper excel_Helper = new Excel_Helper(tournaments, PATH);
                excel_Helper.Create();
            }
        }

        /// <summary>
        /// кнопка генерации вордовского файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChoosedEventText.Text == null || ChoosedEventText.Text == string.Empty)
            {
                MessageBox.Show("You didnt chose an event to show teams which attend this one");
                return;
            }
            Tournament tt = null;
            foreach (Tournament item in tournaments)
            {
                if (item.Name == ChoosedEventText.Text)
                {
                    tt = item;
                    break;
                }
            }
            List<Team> t = new List<Team>();
            foreach (var item in teams)
            {
                if (item.Id / 100 == tt.Id)
                {
                    t.Add(item);
                }
            }
            string PATH = string.Empty;
            // Открываем окно диалога с пользователем.
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Word Documents (*.docx)|*.docx", // Устанавливаем фильтр для файлов
                DefaultExt = "docx", // Расширение по умолчанию
                AddExtension = true, // Добавлять расширение, если его не указали
                Title = "Сохраните файл как" // Заголовок окна
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                PATH = saveFileDialog.FileName;
                Word_Helper word_Helper = new Word_Helper(tt, t, PATH);
                word_Helper.Create();
            }
        }
    }
}