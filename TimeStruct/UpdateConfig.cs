using System;

namespace TimeStruct
{
    /// <summary>
    /// Структура хранение результата изменения элемента
    /// </summary>
    public struct TimeUpdateInfo
    {
        /// <summary>
        /// Путь к элементу
        /// </summary>
        public string Path;
        /// <summary>
        /// Старое значение даты
        /// </summary>
        public DateTime OldTime;
        /// <summary>
        /// Новое значение даты
        /// </summary>
        public DateTime NewTime;
        /// <summary>
        /// Результат изменения даты (true - удачно, false - неудачно)
        /// </summary>
        public bool Success;

        /// <summary>
        /// Путь к элементу
        /// </summary>
        public string FolderPath { get { return Path.ToString(); } }
        /// <summary>
        /// Старое значение даты
        /// </summary>
        public string OldCreate { get { return OldTime.ToShortDateString() + " " + OldTime.ToLongTimeString(); } }
        /// <summary>
        /// Новое значение даты
        /// </summary>
        public string NewCreate { get { return NewTime.ToShortDateString() + " " + NewTime.ToLongTimeString(); } }
        /// <summary>
        /// Результат изменения даты (true - удачно, false - неудачно)
        /// </summary>
        public string Status { get { return Success.ToString(); } }
    }

    /// <summary>
    /// Класс конфигурации анализа папки
    /// </summary>
    public class UpdateConfig
    {
        /// <summary>
        /// Изменение даты пустой папки на значение по-умолчанию
        /// </summary>
        public bool SetEmptyToDefault { get; set; }

        /// <summary>
        /// Значение по-умолчанию
        /// </summary>
        public DateTime DefaultTime { get; set; }

        /// <summary>
        /// Изменение даты создания подкаталогов
        /// </summary>
        public bool ChangeSubfolders { get; set; }

        /// <summary>
        /// Строка статуса "true"
        /// </summary>
        public string statusSuccessed { get; set; }

        /// <summary>
        /// Строка статуса "false"
        /// </summary>
        public string statusFailed { get; set; }

        /// <summary>
        /// Глубина сканирования (-1 - не ограничено)
        /// </summary>
        public int Depth { get; set; }

        public UpdateConfig() : this(0, false, false, DateTime.MinValue, "True", "False") {}

        public UpdateConfig(int depth, bool changeSub, bool emptyToDefault, DateTime defaultTime, string statusTrue, string statusFalse)
        {
            SetEmptyToDefault = emptyToDefault;
            DefaultTime = defaultTime;
            ChangeSubfolders = changeSub;
            Depth = depth;
            statusSuccessed = statusTrue;
            statusFailed = statusFalse;
        }
    }
}
