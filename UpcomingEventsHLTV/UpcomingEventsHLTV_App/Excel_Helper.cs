using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using UpcomingEventsHLTV_App.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace UpcomingEventsHLTV_App
{
    internal class Excel_Helper
    {
        private List<Tournament> tournaments;
        private string PATH = string.Empty;
        public Excel_Helper(List<Tournament> tournaments, string PATH) 
        {
            this.tournaments = tournaments;
            this.PATH = PATH;
        }

        public void Create()
        {
            // Создайте новый экземпляр Excel
            var excelApp = new Excel.Application();
            // excelApp.Visible = true; // Сделать Excel видимым

            // Создайте новую книгу
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
            Excel.Worksheet sheet = (Excel.Worksheet)excelApp.Worksheets.get_Item(1);
            sheet.Name = "Диаграмма призовых";

            // Заполните данные для диаграммы
            worksheet.Cells[1, 1] = "Серия турниров";
            worksheet.Cells[1, 2] = "Призовой";
            int Blastprize = 0;
            int IEMPrize = 0;
            int PGLPrize = 0;
            int MajorPrizes = 0;
            int OtherPrizes = 0;
            foreach (Tournament item in tournaments)
            {
                string str = item.PrizePool.Replace('$', ' ').Replace(',', ' ').Trim();
                if (item.Name.Contains("Blast") && str != "Other")
                    Blastprize += Int32.Parse(str.Replace(" ", ""));

                else if (item.Name.Contains("IEM") && str != "Other")
                    IEMPrize += Int32.Parse(str.Replace(" ", ""));

                else if (item.Name.Contains("PGL") && str != "Other")
                    PGLPrize += Int32.Parse(str.Replace(" ", ""));

                else if (item.Name.Contains("Major") && str != "Other")
                    MajorPrizes += Int32.Parse(str.Replace(" ", ""));

                else if (str != "Other")
                    OtherPrizes += Int32.Parse(str.Replace(" ", ""));
            }
            worksheet.Cells[2, 1] = "BLAST";
            worksheet.Cells[2, 2] = Blastprize;

            worksheet.Cells[3, 1] = "IEM";
            worksheet.Cells[3, 2] = IEMPrize;

            worksheet.Cells[4, 1] = "PGL";
            worksheet.Cells[4, 2] = PGLPrize;

            worksheet.Cells[5, 1] = "Major";
            worksheet.Cells[5, 2] = MajorPrizes;

            worksheet.Cells[6, 1] = "Other";
            worksheet.Cells[6, 2] = OtherPrizes;

            Excel.Range range1 = sheet.get_Range("A1", "A5");
            range1.EntireColumn.ColumnWidth = 25;
            Excel.Range range2 = sheet.get_Range("B1", "B5");
            range2.EntireColumn.ColumnWidth = 15;
            // Создание круговой диаграммы
            Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet.ChartObjects();
            Excel.ChartObject chartObject = chartObjects.Add(250, 50, 300, 300);
            Excel.Chart chart = chartObject.Chart;

            // Укажите источник данных для диаграммы
            chart.SetSourceData(worksheet.Range["A1:B6"]);

            // Установите тип диаграммы на круговую
            chart.ChartType = Excel.XlChartType.xlPie;

            workbook.SaveAs(PATH);
            // Освобождение ресурсов
            Marshal.ReleaseComObject(chart);
            Marshal.ReleaseComObject(chartObject);
            Marshal.ReleaseComObject(chartObjects);
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);

            // Закрыть Excel 
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
        }
    }
}
