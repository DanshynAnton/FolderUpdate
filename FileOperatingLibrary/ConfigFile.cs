using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStruct;
using System.IO;
using System.Text.RegularExpressions;

namespace FileOperatingLibrary
{

    public static class ConfigFile
    {
        /// <summary>
        /// Считывание конфигурации из файла
        /// </summary>
        /// <param name="fileName">имя файла конфигурации</param>
        /// <returns>конфигурация</returns>
        public static void ReadConfigFromFile(string fileName, ref UpdateConfig config)
        {
            config = new UpdateConfig();
            bool analiseSubFolders = false;
            using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
            {
                string readStr = "";
                while (!sr.EndOfStream)
                {
                    readStr = sr.ReadLine();
                    Match match = Regex.Match(readStr, @"^(.+?)\s*=\s*(.+)$");
                    if (match.Success)
                    {

                        try
                        {
                            string paramName = match.Groups[1].ToString();
                            string paramValue = match.Groups[2].ToString();
                            //Применение параметра
                            switch (paramName)
                            {
                                case "AnaliseSubFolder":
                                    analiseSubFolders = GetBool(paramValue);
                                    break;
                                case "Depth":
                                    config.Depth = Convert.ToInt32(paramValue);
                                    break;
                                case "ChangeSubCreationTime":
                                    config.ChangeSubfolders = GetBool(paramValue);
                                    break;
                                case "EmptyFolders":
                                    switch (paramValue)
                                    {
                                        case "0":
                                            config.SetEmptyToDefault = false;
                                            break;
                                        case "1":
                                            config.SetEmptyToDefault = true;
                                            break;
                                        case "2":
                                            config.SetEmptyToDefault = true;
                                            break;
                                    }
                                    break;
                                case "DefaultTime":
                                    config.DefaultTime = GetDateTimeFromString(paramValue);
                                    break;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                if (!analiseSubFolders) { config.Depth = 0; }
            }
        }

        /// <summary>
        /// Запись конфигурации в файл
        /// </summary>
        /// <param name="fileName">имя файла конфигурации</param>
        /// <param name="config">конфигурация</param>
        public static void WriteConfigToFile(string fileName, UpdateConfig config)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                sw.WriteLine("AnaliseSubFolder = " + GetStr(config.Depth != 0));
                sw.WriteLine("Depth = " + config.Depth.ToString());
                sw.WriteLine("ChangeSubCreationTime = " + GetStr(config.ChangeSubfolders));
                sw.WriteLine("EmptyFolders = " + (config.SetEmptyToDefault ? "1" : "0"));
                sw.WriteLine("DefaultTime = " + GetStringFromDateTime(config.DefaultTime));
            }
        }

        /// <summary>
        /// Получение строки времени из DateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeStringFromDateTime(DateTime dt)
        {
            return String.Format("{0:D2}:{1:D2}:{2:D2}", dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// Получение строки даты из DateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateStringFromDateTime(DateTime dt)
        {
            return String.Format("{0:D2}.{1:D2}.{2:D4}", dt.Day, dt.Month, dt.Year);
        }

        /// <summary>
        /// Получение троки даты и времени из DateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetStringFromDateTime(DateTime dt)
        {
            return GetDateStringFromDateTime(dt) + " " + GetTimeStringFromDateTime(dt);
        }

        /// <summary>
        /// Получение DateTime из строки
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromString(string str)
        {
            DateTime dt = DateTime.MinValue;
            //проверка строки на корректность
            Match match = Regex.Match(str, @"^([0-9]{2})\.([0-9]{2})\.([0-9]{4})\s+([0-9]{2}):([0-9]{2}):([0-9]{2})$");
            if (match.Success)
            {
                    dt = new DateTime(Convert.ToInt32(match.Groups[3].Value), Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[6].Value), Convert.ToInt32(match.Groups[5].Value), Convert.ToInt32(match.Groups[4].Value));
            }
            else
            {
                throw new ArgumentException("Argument does not much pattern \n'" + str + "'", "str");
            }

            return dt;
        }

        /// <summary>
        /// Перевод false и true в строки "0" или "1" соответственно
        /// </summary>
        /// <param name="val">bool для перевода</param>
        /// <returns>строковое представление</returns>
        private static string GetStr(bool val)
        {
            string result = val ? "1" : "0";
            return result;
        }

        /// <summary>
        /// Перевод строки "0" или "1" в значения false и true соответственно
        /// </summary>
        /// <param name="inStr">строка для перевода</param>
        /// <returns>bool-значение для строки</returns>
        private static bool GetBool(string inStr)
        {
            bool result = false;
            if (inStr == "1") { result = true; }
            else if (inStr == "0") { result = false; }
            else { throw new ArgumentException("Current parametr must receive values \"0\" or \"1\". Current value = \"" + inStr + "\"", "inStr"); }
            return result;
        }
    }
}
