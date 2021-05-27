using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaleLibrary
{
    public class FileLocaleStrings
    {

        /// <summary>
        /// Заголовок столбца "Путь к папке"
        /// </summary>
        public string PathTitle { get; set; }

        /// <summary>
        /// Заголовок столбца "Изначальная дата создания"
        /// </summary>
        public string OldCreationTimeTitle { get; set; }

        /// <summary>
        /// Заголовок столбца "Новая дата создания"
        /// </summary>
        public string NewCreationTimeTitle { get; set; }

        /// <summary>
        /// Заголовок столбца "Статус"
        /// </summary>
        public string StatusTitle { get; set; }

        /// <summary>
        /// Строка интерпретации статуса TRUE
        /// </summary>
        public string StatusSuccessed { get; set; }

        /// <summary>
        /// Строка интерпретации статуса FALSE
        /// </summary>
        public string StatusFailed { get; set; }

        /// <summary>
        /// Английская локализация
        /// </summary>
        public static FileLocaleStrings EnglishLocale
        {
            get
            {
                FileLocaleStrings f = new FileLocaleStrings(
                    "Folder path", 
                    "Original creation time", 
                    "New creation time", 
                    "Status", 
                    "Successed", 
                    "Failed");
                return f;
            }
        }

        /// <summary>
        /// Русская локализация
        /// </summary>
        public static FileLocaleStrings RussianLocale
        {
            get
            {
                FileLocaleStrings f = new FileLocaleStrings(
                    "Путь к папке",
                    "Изначальное время создания папки",
                    "Изменённое время создания папки",
                    "Статус",
                    "Успех",
                    "Неудача");
                return f;
            }
        }

        /// <summary>
        /// Украинская локализация
        /// </summary>
        public static FileLocaleStrings UkrainianLocale
        {
            get
            {
                FileLocaleStrings f = new FileLocaleStrings(
                    "Шлях до каталогу",
                    "Вихідний час створення каталогу",
                    "Новий час створення каталогу",
                    "Статус",
                    "Успішна",
                    "Невдала");
                return f;
            }
        }

        /// <summary>
        /// Конструктор без параметров (англ. локализация)
        /// </summary>
        public FileLocaleStrings() : this("Folder path", "Original created time", "New created time", "Status", "Succeed", "Failed") { }

        /// <summary>
        /// Конструктор со всеми строками локализации
        /// </summary>
        /// <param name="path">путь к папке</param>
        /// <param name="oldTime">старое время создание</param>
        /// <param name="newTime">новое время создания</param>
        /// <param name="status">статус</param>
        /// <param name="success">успех</param>
        /// <param name="failed">провал</param>
        public FileLocaleStrings(string path, string oldTime, string newTime, string status, string success, string failed)
        {
            SetLocale(path, oldTime, newTime, status, success, failed);
        }

        /// <summary>
        /// Установка новой локализации (заголовков столбцов)
        /// </summary>
        /// <param name="path">путь к папке</param>
        /// <param name="oldTime">старое время создание</param>
        /// <param name="newTime">новое время создания</param>
        /// <param name="status">статус</param>
        /// <param name="success">успех</param>
        /// <param name="failed">провал</param>
        public void SetLocale(string path, string oldTime, string newTime, string status, string success, string failed)
        {
            PathTitle = path;
            OldCreationTimeTitle = oldTime;
            NewCreationTimeTitle = newTime;
            StatusTitle = status;
            StatusSuccessed = success;
            StatusFailed = failed;
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
                    case "0":
                        res = PathTitle;
                        break;
                    case "1":
                        res = OldCreationTimeTitle;
                        break;
                    case "2":
                        res = NewCreationTimeTitle;
                        break;
                    case "3":
                        res = StatusTitle;
                        break;
                    case "4":
                        res = StatusSuccessed;
                        break;
                    case "5":
                        res = StatusFailed;
                        break;
                    default: throw new ArgumentOutOfRangeException("index", index, "this index is not supported");
                }
                return res;
            }
            set
            {
                switch (index)
                {
                    case "0":
                        PathTitle = value;
                        break;
                    case "1":
                        OldCreationTimeTitle = value;
                        break;
                    case "2":
                        NewCreationTimeTitle = value;
                        break;
                    case "3":
                        StatusTitle = value;
                        break;
                    case "4":
                        StatusSuccessed = value;
                        break;
                    case "5":
                        StatusFailed = value;
                        break;
                    default: throw new ArgumentOutOfRangeException("index", index, "this index is not supported");
                }
            }
        }
    }
}
