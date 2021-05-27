using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System.Windows.Forms;
using System.ComponentModel;
using TimeStruct;
using TimeUpdateLibrary;
using FileOperatingLibrary;
using LocaleLibrary;

namespace FolderUpdate
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UILocaleStrings uiLocale = new UILocaleStrings();
        private FileLocaleStrings fLocale = new FileLocaleStrings();

        private UpdateConfig updateConfig = new UpdateConfig();
        private List<TimeUpdateInfo> ResultLog = new List<TimeUpdateInfo>();


        /// <summary>
        /// Список доступных языков локализации
        /// </summary>
        private Dictionary<int, LocaleFile> localeList = new Dictionary<int, LocaleFile>();

        public MainWindow()
        {
            InitializeComponent();
            uiLocale = new UILocaleStrings();
            for (int i = 1; i <= 20; i++)
            {
                ComboBoxItem cbiNew = new ComboBoxItem();
                cbiNew.Name = "depth" + i.ToString();
                cbiNew.Content = i.ToString();
                dbSubFoldersDepth.Items.Add(cbiNew);
            }
            //Установка времени по-умолчанию
            dpDefaultDate.SelectedDate = DateTime.Now;
            tbDefaultTime.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //Получение локализаций
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\locale");
            string[] files = Directory.GetFiles( Directory.GetCurrentDirectory() + @"\locale");
            int localeCount = 0;
            bool enCheck = false;
            bool ruCheck = false;
            bool uaCheck = false;

            for(int i = 0; i < files.Length; i++)
            {

                if (files[i].EndsWith(".loc"))
                {
                    LocaleFile newLocFile = new LocaleFile();
                    newLocFile.ReadLocaleFromFile(files[i]);
                    if (newLocFile.EnglishName == "English") { enCheck = true; }
                    if (newLocFile.EnglishName == "Russian") { ruCheck = true; }
                    if (newLocFile.EnglishName == "Ukrainian") { uaCheck = true; }
                    localeList.Add(localeCount, newLocFile);
                    dbLang.Items.Add(newLocFile.LocaleName + " (" + newLocFile.EnglishName + ")");
                    localeCount++;
                }
            }

            //Добавление стандартных локализаций, если их не было среди файлов
            if (!enCheck)
            {
                LocaleFile newLocFile = LocaleFile.EnglishLocale;
                localeList.Add(localeCount, newLocFile);
                dbLang.Items.Add(newLocFile.LocaleName + " (" + newLocFile.EnglishName + ")");
                newLocFile.WriteLocaleToFile(Directory.GetCurrentDirectory() + @"\locale\en-EN.loc");
                localeCount++;
            }
            if (!ruCheck)
            {
                LocaleFile newLocFile = LocaleFile.RussianLocale;
                localeList.Add(localeCount, newLocFile);
                dbLang.Items.Add(newLocFile.LocaleName + " (" + newLocFile.EnglishName + ")");
                newLocFile.WriteLocaleToFile(Directory.GetCurrentDirectory() + @"\locale\ru-RU.loc");
                localeCount++;
            }
            if (!uaCheck)
            {
                LocaleFile newLocFile = LocaleFile.UkrainianLocale;
                localeList.Add(localeCount, newLocFile);
                dbLang.Items.Add(newLocFile.LocaleName + " (" + newLocFile.EnglishName + ")");
                newLocFile.WriteLocaleToFile(Directory.GetCurrentDirectory() + @"\locale\ua-UA.loc");
                localeCount++;
            }

            dbLang.SelectedIndex = 0;
            //Получение конфигурации
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\config");
            files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\config");
            bool isExist = false;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].EndsWith("default.cfg"))
                {
                    isExist = true;
                    ConfigFile.ReadConfigFromFile(files[i], ref updateConfig);
                }
            }
            if (!isExist) { ConfigFile.WriteConfigToFile(Directory.GetCurrentDirectory() + @"\config\default.cfg", updateConfig); }
            SetConfiguration();
            ApplyLocale();
        }

        /// <summary>
        /// Применение локализации к элемнетам интерфейса
        /// </summary>
        private void ApplyLocale()
        {
            //Применение локализации к TextBlock
            LBaseFolder.Text = uiLocale.BaseFolder;
            LSubFoldersDepth.Text = uiLocale.Depth;
            LEmptyFolders.Text = uiLocale.EmptyFolder;

            //Приемнение локализации к CheckBox
            cbSubFoldersAnalise.Content = uiLocale.SubAnalise;
            cbSubFoldersChange.Content = uiLocale.SubChange;

            //Применение локализации к ComboBoxItem
            cbiDoNotUse.Content = uiLocale.EmptyNoChange;
            cbiTimeUTC.Content = uiLocale.EmptyToUTC;
            cbiTimeLocal.Content = uiLocale.EmptyToLocal;
            cbiDepthUnlimited.Content = uiLocale.ScanNoLimits;

            //Применение локализации к кнопкам
            bSaveLog.Content = uiLocale.SaveLog;
            bSaveConfig.Content = uiLocale.SaveConfig;
            bLoadConfig.Content = uiLocale.LoadConfig;
            bStart.Content = uiLocale.Start;
            tbLang.Text = uiLocale.Language;
        }

        /// <summary>
        /// Получение конфигурации на основе введ'нніх пользователем значений
        /// </summary>
        private void GetConfiguration()
        {
            //опредление параметров анализа подпапок
            bool subAnalise = cbSubFoldersAnalise.IsChecked ?? false;
            //определение глубины анализа
            int depth = 0;
            if (subAnalise)
            {
                if (cbiDepthUnlimited.IsSelected) { depth = -1; }
                else
                {
                    depth = dbSubFoldersDepth.SelectedIndex;
                }
            }
            //Определнения разрешения изменения времени создания для подпапки
            bool subChange = cbSubFoldersChange.IsChecked ?? false;

            //Определение действий над пустыми папками и запуск анализа
            DateTime dt;
            try
            {
                dt = ConfigFile.GetDateTimeFromString(ConfigFile.GetDateStringFromDateTime(dpDefaultDate.SelectedDate ?? DateTime.Now) + " " + tbDefaultTime.Text);
            }
            catch (Exception) { dt = DateTime.Now; }
            DateTime emptyDateTime = cbiTimeLocal.IsSelected ? dt.ToUniversalTime() : dt;

            //получение конфигурации
            updateConfig = new UpdateConfig(depth, subChange, !cbiDoNotUse.IsSelected, dt, "Hello", fLocale.StatusFailed);
        }

        /// <summary>
        /// Применение конфигурации на форму
        /// </summary>
        private void SetConfiguration()
        {
            cbSubFoldersAnalise.IsChecked = (updateConfig.Depth != 0);
            dbSubFoldersDepth.SelectedIndex = (updateConfig.Depth == -1) ? 0 : updateConfig.Depth;
            cbSubFoldersChange.IsChecked = updateConfig.ChangeSubfolders;
            dbEmptyFolders.SelectedIndex = updateConfig.SetEmptyToDefault ? 1 : 0;
            dpDefaultDate.SelectedDate = updateConfig.DefaultTime;
            tbDefaultTime.Text = ConfigFile.GetTimeStringFromDateTime(updateConfig.DefaultTime);
        }
       
        /// <summary>
        /// Выбор папки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFolder(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult dr = dialog.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                tbBaseFolder.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Начало анализа папки с файлами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartAnalise(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(tbBaseFolder.Text))
            {
                //Включение кнопок сохранения результата
                bSaveLog.IsEnabled = true;
                dgvLog.ItemsSource = null;
                //Определение параметров поиска
                //Определение исходной папки
                string folderPath = tbBaseFolder.Text;
                GetConfiguration();

                //Запуск анализа
                FolderTimeUpdate.Config = updateConfig;
                FolderTimeUpdate.UpdateFolderCreationTime(folderPath);
                ResultLog = FolderTimeUpdate.GetLastResult();

                //Вывод отчёта
                try
                {
                    dgvLog.ItemsSource = null;
                    dgvLog.AutoGenerateColumns = true;
                    dgvLog.ItemsSource = ResultLog;
                    dgvLog.Columns[0].Header = uiLocale.Path;
                    dgvLog.Columns[1].Header = uiLocale.OldTime;
                    dgvLog.Columns[2].Header = uiLocale.NewTime;
                    dgvLog.Columns[3].Header = uiLocale.Status;
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Сохранение отчёта о работе в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLog(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckFileExists = false;
            dialog.FileName = "Log";
            dialog.Filter = "txt file (.txt)|*.txt|XML-file (.xml)|*.xml|HTML-table (.html)|*.html|Excel table (.xlsx)|*.xlsx";
            if (dialog.ShowDialog() ?? false)
            {
                //Формирование файла с отчётом
                string logFileName = dialog.FileName;
                LogFile logFile = new LogFile();
                logFile.Locale = fLocale;
                switch (dialog.FilterIndex)
                {
                    case 1:
                        //txt-format
                        logFile.TextFormat(ResultLog, logFileName);
                        break;
                    case 2:
                        //xml-format
                        logFile.XmlFormat(ResultLog, logFileName);
                        break;
                    case 3:
                        //html-format
                        logFile.HtmlFormat(ResultLog, logFileName);
                        break;
                    case 4:
                        logFile.XlsFormat(ResultLog, logFileName);
                        break;
                }
            }
        }

        /// <summary>
        /// Сохранение конфигурации в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConfig(object sender, RoutedEventArgs e)
        {
            GetConfiguration();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.CheckFileExists = false;
            dialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\config";
            dialog.FileName = "Config";
            dialog.Filter = "config file (.cfg)|*.cfg";
            if (dialog.ShowDialog() ?? false)
            {
                //Формирование файла конфигурации
                string logFileName = dialog.FileName;
                ConfigFile.WriteConfigToFile(logFileName, updateConfig);
            }
        }

        /// <summary>
        /// Загрузка конфигурации из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadConfig(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = false;
            dialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\config";
            dialog.FileName = "Config";
            dialog.DefaultExt = ".cfg";
            if (dialog.ShowDialog() ?? false)
            {
                //Получение конфигурации из файла
                string fileName = dialog.FileName;
                ConfigFile.ReadConfigFromFile(fileName, ref updateConfig);
                //Применение конфигурации на форму
                SetConfiguration();
            }
        }

        /// <summary>
        /// Изменение локализации программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLocale(object sender, SelectionChangedEventArgs e)
        {
            LocaleFile loc = localeList[dbLang.SelectedIndex];
            fLocale = loc.FileLocale;
            uiLocale = loc.UILocale;

            ApplyLocale();
        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, CancelEventArgs e)
        {
            //Сохранение конфигурации при закрытии
            GetConfiguration();
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\config");
            ConfigFile.WriteConfigToFile(Directory.GetCurrentDirectory() + @"\config\default.cfg", updateConfig);
        }
    }
}
