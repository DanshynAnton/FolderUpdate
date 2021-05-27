using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaleLibrary
{
    public class UILocaleStrings
    {
        //Названия на форме
        public string BaseFolder { get; set; }
        public string SubAnalise { get; set; }
        public string Depth { get; set; }
        public string SubChange { get; set; }
        public string EmptyFolder { get; set; }
        //Названия кнопок
        public string Language { get; set; }
        public string SaveLog { get; set; }
        public string SaveConfig { get; set; }
        public string LoadConfig { get; set; }
        public string Start { get; set; }

        //Столбцы в dgv
        public string Path { get; set; }
        public string OldTime { get; set; }
        public string NewTime { get; set; }
        public string Status { get; set; }
        //Строки в ComboBox
        public string ScanNoLimits { get; set; }
        public string EmptyNoChange { get; set; }
        public string EmptyToUTC { get; set; }
        public string EmptyToLocal { get; set; }

        /// <summary>
        /// Английская локализация
        /// </summary>
        public static UILocaleStrings EnglishLocale
        {
            get
            {
                UILocaleStrings u = new UILocaleStrings();

                u.BaseFolder = "Base Folder";
                u.SubAnalise = "Analise Subfolders content";
                u.Depth = "SubFolders Depth";
                u.SubChange = "Change Creation time for Subfolders";
                u.EmptyFolder = "Empty Folders";

                u.Language = "Language";
                u.SaveLog = "Save Log";
                u.SaveConfig = "Save Configuration";
                u.LoadConfig = "Load Configuration";
                u.Start = "Start";

                u.Path = "Folder Path";
                u.OldTime = "Original Creation Time";
                u.NewTime = "New Creation Time";
                u.Status = "Status";

                u.ScanNoLimits = "Unlimited";
                u.EmptyNoChange = "Don't change time for empty folders";
                u.EmptyToUTC = "(UTC) Set the specified time for empty folders";
                u.EmptyToLocal = "(Local) Set the specified time for empty folders";
                return u;
            }
        }

        /// <summary>
        /// Русская локализация
        /// </summary>
        public static UILocaleStrings RussianLocale
        {
            get
            {
                UILocaleStrings u = new UILocaleStrings();
                u.BaseFolder = "Исходная папка";
                u.SubAnalise = "Учитывать содержимое подпапок";
                u.Depth = "Глубина сканирования подпапок";
                u.SubChange = "Изменять время создания подпапок";
                u.EmptyFolder = "Действия над пустыми каталогами";

                u.Language = "Язык";
                u.SaveLog = "Сохранить отчёт";
                u.SaveConfig = "Сохранить настройки";
                u.LoadConfig = "Загрузить настройки";
                u.Start = "Начать";

                u.Path = "Путь к папке";
                u.OldTime = "Изначальное время создания папки";
                u.NewTime = "Изменённое время создания папки";
                u.Status = "Статус";

                u.ScanNoLimits = "Неограничено";
                u.EmptyNoChange = "Не изменять время создания";
                u.EmptyToUTC = "(UTC) Установить время создания на выбранное";
                u.EmptyToLocal = "(Локальный) Установить время создания на выбранное";
                return u;
            }
        }

        /// <summary>
        /// Украинская локализация
        /// </summary>
        public static UILocaleStrings UkrainianLocale
        {
            get
            {
                UILocaleStrings u = new UILocaleStrings();
                u.BaseFolder = "Вихідний каталог";
                u.SubAnalise = "Аналізувати вміст підкаталогів";
                u.Depth = "Глибина сканування підкаталогів";
                u.SubChange = "Змінювати час створення підкаталогів";
                u.EmptyFolder = "Дії над порожніми каталогами";

                u.Language = "Мова";
                u.SaveLog = "Зберегти звіт";
                u.SaveConfig = "Зберегти налаштування";
                u.LoadConfig = "Завантажити налаштування";
                u.Start = "Почати";

                u.Path = "Шлях до каталогу";
                u.OldTime = "Вихідний час створення каталогу";
                u.NewTime = "Новий час створення каталогу";
                u.Status = "Статус";

                u.ScanNoLimits = "Необмежена";
                u.EmptyNoChange = "Не змінювати час створення";
                u.EmptyToUTC = "(UTC) Змінити час створення на обраний";
                u.EmptyToLocal = "(Локальний) Змінити час створення на обраний";
                return u;
            }
        }

        public UILocaleStrings()
        {
            this.Clear();
        }

        /// <summary>
        /// Очищение всех полей (значения по-умолчанию на англ)
        /// </summary>
        public void Clear()
        {
            this.BaseFolder = "Исходная папка";
            this.SubAnalise = "Учитывать содержимое подпапок";
            this.Depth = "Глубина сканирования подпапок";
            this.SubChange = "Изменять время создания подпапок";
            this.EmptyFolder = "Действия над пустыми каталогами";

            this.Language = "Язык";
            this.SaveLog = "Сохранить отчёт";
            this.SaveConfig = "Сохранить настройки";
            this.LoadConfig = "Загрузить настройки";
            this.Start = "Начать";

            this.Path = "Путь к папке";
            this.OldTime = "Изначальное время создания папки";
            this.NewTime = "Изменённое время создания папки";
            this.Status = "Статус";

            this.ScanNoLimits = "Неограничено";
            this.EmptyNoChange = "Не изменять время создания";
            this.EmptyToUTC = "(UTC) Установить время создания на выбранное";
            this.EmptyToLocal = "(Локальный) Установить время создания на выбранное";
        }

        /// <summary>
        /// Получение поля по индексу
        /// </summary>
        /// <param name="index">индекс поля</param>
        /// <returns>значение поля</returns>
        public string this[string index]
        {
            get
            {
                string res = "";
                switch (index)
                {
                    case "6":
                        res = BaseFolder;
                        break;
                    case "7":
                        res = SubAnalise;
                        break;
                    case "8":
                        res = Depth;
                        break;
                    case "9":
                        res = SubChange;
                        break;
                    case "10":
                        res = EmptyFolder;
                        break;
                    case "11":
                        res = Language;
                        break;
                    case "12":
                        res = SaveLog;
                        break;
                    case "13":
                        res = SaveConfig;
                        break;
                    case "14":
                        res = LoadConfig;
                        break;
                    case "15":
                        res = Start;
                        break;
                    case "16":
                        res = Path;
                        break;
                    case "17":
                        res = OldTime;
                        break;
                    case "18":
                        res = NewTime;
                        break;
                    case "19":
                        res = Status;
                        break;
                    case "20":
                        res = ScanNoLimits;
                        break;
                    case "21":
                        res = EmptyNoChange;
                        break;
                    case "22":
                        res = EmptyToUTC;
                        break;
                    case "23":
                        res = EmptyToLocal;
                        break;
                    default: throw new ArgumentOutOfRangeException("index", index, "this index is not supported");
                }
                return res;
            }
            set
            {
                switch (index)
                {
                    case "6":
                        BaseFolder = value;
                        break;
                    case "7":
                        SubAnalise = value;
                        break;
                    case "8":
                        Depth = value;
                        break;
                    case "9":
                        SubChange = value;
                        break;
                    case "10":
                        EmptyFolder = value;
                        break;
                    case "11":
                        Language = value;
                        break;
                    case "12":
                        SaveLog = value;
                        break;
                    case "13":
                        SaveConfig = value;
                        break;
                    case "14":
                        LoadConfig = value;
                        break;
                    case "15":
                        Start = value;
                        break;
                    case "16":
                        Path = value;
                        break;
                    case "17":
                        OldTime = value;
                        break;
                    case "18":
                        NewTime = value;
                        break;
                    case "19":
                        Status = value;
                        break;
                    case "20":
                        ScanNoLimits = value;
                        break;
                    case "21":
                        EmptyNoChange = value;
                        break;
                    case "22":
                        EmptyToUTC = value;
                        break;
                    case "23":
                        EmptyToLocal = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("index", index, "this index is not supported");
                        break;
                }
            }
        }
    }
}
