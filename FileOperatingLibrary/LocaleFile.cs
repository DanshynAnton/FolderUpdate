using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LocaleLibrary;
using System.Text.RegularExpressions;

namespace FileOperatingLibrary
{
    public class LocaleFile
    {
        /// <summary>
        /// Название локализации на языке локализации
        /// </summary>
        public string LocaleName { get; set; }

        /// <summary>
        /// Название локализации на английском языке
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// Лакализация для вывода в файл
        /// </summary>
        public FileLocaleStrings FileLocale { get; set; }

        /// <summary>
        /// Локализация для пользовательского интерфейса
        /// </summary>
        public UILocaleStrings UILocale { get; set; }

        /// <summary>
        /// Английская локализация
        /// </summary>
        public static LocaleFile EnglishLocale
        {
            get
            {
                LocaleFile u = new LocaleFile();
                u.EnglishName = "English";
                u.LocaleName = "English";
                u.FileLocale = FileLocaleStrings.EnglishLocale;
                u.UILocale = UILocaleStrings.EnglishLocale;

                return u;
            }
        }

        /// <summary>
        /// Русская локализация
        /// </summary>
        public static LocaleFile RussianLocale
        {
            get
            {
                LocaleFile u = new LocaleFile();
                u.EnglishName = "Russian";
                u.LocaleName = "Русская";
                u.FileLocale = FileLocaleStrings.RussianLocale;
                u.UILocale = UILocaleStrings.RussianLocale;
                return u;
            }
        }

        /// <summary>
        /// Украинская локализация
        /// </summary>
        public static LocaleFile UkrainianLocale
        {
            get
            {
                LocaleFile u = new LocaleFile();
                u.EnglishName = "Ukrainian";
                u.LocaleName = "Українська";
                u.FileLocale = FileLocaleStrings.UkrainianLocale;
                u.UILocale = UILocaleStrings.UkrainianLocale;

                return u;
            }
        }

        public LocaleFile()
        {
            this.EnglishName = "English";
            this.LocaleName = "English";
            this.FileLocale = FileLocaleStrings.EnglishLocale;
            this.UILocale = UILocaleStrings.EnglishLocale;
        }

        /// <summary>
        /// Считать локализацию из файла
        /// </summary>
        /// <param name="fileName">Имя файла локализации</param>
        public void ReadLocaleFromFile(string fileName)
        {
            //Открытие потока файла
            using (StreamReader sr = new StreamReader(fileName, Encoding.Unicode))
            {
                //Считывание название локализации на английском языке
                EnglishName = sr.ReadLine();
                //Считывание названия локализации на языке локализации
                LocaleName = sr.ReadLine();
                FileLocale = new FileLocaleStrings();
                UILocale = new UILocaleStrings();

                //Считывание строк параметров до конца файла
                string readStr = "";
                while (!sr.EndOfStream)
                {
                    readStr = sr.ReadLine();
                    Match match = Regex.Match(readStr, @"^([0-9]+)\s*=\s*(.+)$");
                    if (match.Success)
                    {
                        //Применение параметра

                        try
                        {
                            FileLocale[match.Groups[1].ToString()] = match.Groups[2].ToString();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                UILocale[match.Groups[1].ToString()] = match.Groups[2].ToString();
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Записать текущую локализацию в файл
        /// </summary>
        /// <param name="fileName">имя файла</param>
        public void WriteLocaleToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                sw.WriteLine(EnglishName);
                sw.WriteLine(LocaleName);
                try
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        sw.WriteLine(i + " = " + FileLocale[i.ToString()]);
                    }
                }
                catch (Exception) { }
                try
                {
                    for (int i = 6; i <= 23; i++)
                    {
                        sw.WriteLine(i + " = " + UILocale[i.ToString()]);
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
