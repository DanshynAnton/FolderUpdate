using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeStruct;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using LocaleLibrary;

namespace FileOperatingLibrary
{
    /// <summary>
    /// Класс формирования файла отчёта
    /// </summary>
    public class LogFile
    {

        /// <summary>
        /// Описание локализации
        /// </summary>
        public FileLocaleStrings Locale { get; set; }

        public LogFile()
        {
            Locale = new FileLocaleStrings();
        }

        /// <summary>
        /// Формирование текстового файла отчёта
        /// </summary>
        /// <param name="resultList">Результат работы</param>
        /// <param name="fileName">Имя файла (путь к нему)</param>
        public bool TextFormat(List<TimeUpdateInfo> resultList, string fileName)
        {
            //Открытие файла
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                //Запись каждого елемента в файл
                for (int i = 0; i < resultList.Count; i++)
                {
                    //запись елемента в файл
                    sw.WriteLine("=======================================");
                    sw.WriteLine(Locale.PathTitle + ": " + resultList[i].Path);
                    sw.WriteLine(Locale.OldCreationTimeTitle + ": " + resultList[i].OldTime);
                    sw.WriteLine(Locale.NewCreationTimeTitle + ": " + resultList[i].NewTime);
                    sw.WriteLine(Locale.StatusTitle + ": " + (resultList[i].Success ? Locale.StatusSuccessed : Locale.StatusFailed));
                    sw.WriteLine("=======================================\n");
                }
            }
            return true;
        }

        /// <summary>
        /// Формирование xml-файла отчёта
        /// </summary>
        /// <param name="resultList">Результат работы</param>
        /// <param name="fileName">Имя файла (путь к нему)</param>
        public bool XmlFormat(List<TimeUpdateInfo> resultList, string fileName)
        {
            //Открытие файла
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                //Запись заголовка
                sw.WriteLine("<?xml version=\"1.0\" ?>");
                sw.WriteLine("<folder_update>");
                //Запись каждого елемента в файл
                for (int i = 0; i < resultList.Count; i++)
                {
                    //запись елемента в файл
                    sw.WriteLine("\t<item>");
                    sw.WriteLine("\t\t<folder_path>" + resultList[i].Path + "</folder_path>");
                    sw.WriteLine("\t\t<original_creation_time>" + resultList[i].OldTime + "</original_creation_time>");
                    sw.WriteLine("\t\t<new_creation_time>" + resultList[i].NewTime + "</new_creation_time>");
                    sw.WriteLine("\t\t<status>" + (resultList[i].Success ? Locale.StatusSuccessed : Locale.StatusFailed) + "</status>");
                    sw.WriteLine("\t</item>");
                }

                //Запись окончания файла
                sw.WriteLine("</folder_update>");
            }
            return true;
        }

        /// <summary>
        /// Формирование html-файла отчёта
        /// </summary>
        /// <param name="resultList">Результат работы</param>
        /// <param name="fileName">Имя файла (путь к нему)</param>
        public bool HtmlFormat(List<TimeUpdateInfo> resultList, string fileName)
        {
            //Открытие файла
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
            {
                //Запись заголовка файла
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("\t<title>FolderUpdate</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");

                //Запись заголовка таблицы
                sw.WriteLine("\t<table border=\"1\">");
                sw.WriteLine("\t\t<tr>");
                sw.WriteLine("\t\t\t<th>" + Locale.PathTitle + "</th>");
                sw.WriteLine("\t\t\t<th>" + Locale.OldCreationTimeTitle + "</th>");
                sw.WriteLine("\t\t\t<th>" + Locale.NewCreationTimeTitle + "</th>");
                sw.WriteLine("\t\t\t<th>" + Locale.StatusTitle + "</th>");
                sw.WriteLine("\t\t</tr>");
                //Запись каждого елемента в файл
                for (int i = 0; i < resultList.Count; i++)
                {
                    //запись елемента в файл
                    sw.WriteLine("\t\t<tr>");
                    sw.WriteLine("\t\t\t<td>" + resultList[i].Path + "</td>");
                    sw.WriteLine("\t\t\t<td>" + resultList[i].OldTime + "</td>");
                    sw.WriteLine("\t\t\t<td>" + resultList[i].NewTime + "</td>");
                    sw.WriteLine("\t\t\t<td>" + (resultList[i].Success ? Locale.StatusSuccessed : Locale.StatusFailed) + "</td>");
                    sw.WriteLine("\t\t</tr>");
                }
                sw.WriteLine("\t</table>");

                //Запись окончания файла
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }
            return true;
        }

        /// <summary>
        /// Формирование xls-файла отчёта (Excell table)
        /// </summary>
        /// <param name="resultList">Результат работы</param>
        /// <param name="fileName">Имя файла (путь к нему)</param>
        public bool XlsFormat(List<TimeUpdateInfo> resultList, string fileName)
        {
            //Создание файла
            SpreadsheetDocument ssd = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);

            //Добавление книги
            WorkbookPart wbp = ssd.AddWorkbookPart();
            wbp.Workbook = new Workbook();
            WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
            wsp.Worksheet = new Worksheet(new SheetData());

            //Добавление страницы
            Sheets sheets = ssd.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
            Sheet sheet = new Sheet()
            {
                Id = ssd.WorkbookPart.
                GetIdOfPart(wsp),
                SheetId = 1,
                Name = "Log"
            };
            sheets.Append(sheet);
            wbp.Workbook.Save();

            //Добавить данные в файл
            //Добавление заголовка
            Cell c;
            c = InsertCellInWorksheet("A", 1, wsp);
            c.CellValue = new CellValue(Locale.PathTitle);
            c = InsertCellInWorksheet("B", 1, wsp);
            c.CellValue = new CellValue(Locale.OldCreationTimeTitle);
            c = InsertCellInWorksheet("C", 1, wsp);
            c.CellValue = new CellValue(Locale.NewCreationTimeTitle);
            c = InsertCellInWorksheet("D", 1, wsp);
            c.CellValue = new CellValue(Locale.StatusTitle);
            //Добавление данных
            for (uint i = 0; i < resultList.Count; i++)
            {
                c = InsertCellInWorksheet("A", i + 2, wsp);
                c.CellValue = new CellValue(resultList[(int)i].Path);
                c = InsertCellInWorksheet("B", i + 2, wsp);
                c.CellValue = new CellValue(resultList[(int)i].OldTime.ToShortDateString());
                c = InsertCellInWorksheet("C", i + 2, wsp);
                c.CellValue = new CellValue(resultList[(int)i].NewTime.ToShortDateString());
                c = InsertCellInWorksheet("D", i + 2, wsp);
                c.CellValue = new CellValue((resultList[(int)i].Success ? Locale.StatusSuccessed : Locale.StatusFailed));
            }

            // Закрыть файл
            wbp.Workbook.Save();
            ssd.Close();
            return true;
        }

        /// <summary>
        /// Запись в ячейку Excell
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <param name="worksheetPart"></param>
        /// <returns></returns>
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }
    }
}
