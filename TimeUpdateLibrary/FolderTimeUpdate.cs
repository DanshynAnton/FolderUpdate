using System;
using System.Collections.Generic;
using System.IO;
using TimeStruct;

namespace TimeUpdateLibrary
{
    public static partial class FolderTimeUpdate
    {

        /// <summary>
        /// Конфигурация работы программы
        /// </summary>
        public static UpdateConfig Config { get; set; }

        /// <summary>
        /// Хранение отчёта изменения даты создания элементов
        /// </summary>
        private static List<TimeUpdateInfo> ResultInfo = new List<TimeUpdateInfo>();

        /// <summary>
        /// Получение результата последнего анализа
        /// </summary>
        /// <returns>Результат последней работы алгоритма</returns>
        public static List<TimeUpdateInfo> GetLastResult()
        {
            return ResultInfo;
        }
        /// <summary>
        /// Изменения даты создания каталога
        /// </summary>
        /// <param name="path">Путь к исходному каталогу</param>
        /// <returns>Отчёт изменений</returns>
        public static List<TimeUpdateInfo> UpdateFolderCreationTime(string path)
        {
            ResultInfo.Clear();
            UPDFolderCreationTime(path, Config.Depth, true);
            return ResultInfo;
        }

        /// <summary>
        /// Обновление времени создания папки
        /// </summary>
        /// <param name="path">Путь к папке</param>
        /// <param name="count">глубина поиска (0 - не затрагивать содержимое подпапок, -1 - не ограничивать глубину)</param>
        /// <returns></returns>
        private static DateTime UPDFolderCreationTime(string path, int count, bool allowChange)
        {
            //Список подкаталогов
            string[] dirs;
            //Список файлов
            string[] files;

            //Изначальная дата создания
            DateTime OldDir = DateTime.MinValue;
            //Изменённая дата создания
            DateTime NewDir = DateTime.MinValue;

            //Результат
            TimeUpdateInfo tui = new TimeUpdateInfo();
            tui.Path = path;
            tui.OldTime = OldDir;
            tui.NewTime = NewDir;
            tui.Success = true;

            try
            {
                //Список подкаталогов
                dirs = Directory.GetDirectories(path);
                //Список файлов
                files = Directory.GetFiles(path);
                //Изначальная дата создания
                OldDir = Directory.GetCreationTimeUtc(path);
                tui.OldTime = OldDir;

                if ((dirs.Length + files.Length) == 0)
                {
                    //Папка пустая
                    if (Config.SetEmptyToDefault)
                    {
                        Directory.SetCreationTimeUtc(path, Config.DefaultTime);
                    }
                }
                else
                {
                    //Папка не пустая
                    //Переменные для хранения дат создания
                    DateTime DirMax = DateTime.MinValue;
                    DateTime FileMax = DateTime.MinValue;

                    DateTime NewFile = DateTime.MinValue;

                    //Анализподкаталогов подкаталогов, если они есть
                    if (dirs.Length > 0)
                    {
                        //Поиск максимального времени создания каталогов
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            if ((count > 0) || (count == -1))
                            {
                                //Анализ содержимого каталога
                                //если count > 0 - то отнимаем одну итерацию
                                //иначе (т.е. count == -1 (см предыдущий if) - так и оставляем)
                                NewDir = UPDFolderCreationTime(dirs[i], (count > 0) ? count - 1 : count, Config.ChangeSubfolders);
                                //присвоение наибольшей даты создания

                            }
                            else
                            {
                                //Получение даты создания каталога без анализа его содержимого
                                NewDir = Directory.GetCreationTimeUtc(dirs[i]);
                            }
                            if (NewDir > DirMax) { DirMax = NewDir; }
                        }
                    }

                    //Анализ файлов, если они есть
                    if (files.Length > 0)
                    {
                        //Поиск максимального времени создания файлов
                        for (int i = 0; i < files.Length; i++)
                        {
                            NewFile = File.GetCreationTimeUtc(files[i]);
                            if (NewFile > FileMax) { FileMax = NewFile; }
                        }
                    }

                    //Присвоение результата и вывод его в отчёт
                    if (allowChange)
                    {
                        //Если можно менять время создания - изменить его
                        Directory.SetCreationTimeUtc(path, (FileMax > DirMax) ? FileMax : DirMax);
                    }
                }
                tui.NewTime = Directory.GetCreationTimeUtc(path);
                tui.Success = true;
            }
            catch (Exception)
            {
                tui.Success = false;
            }
            //Вывод в отчёт
            ResultInfo.Add(tui);
            //Возвращение результата
            return tui.NewTime;
        }
    }
}
