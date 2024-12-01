using UpcomingEventsHLTV_App.Models;
using word = Microsoft.Office.Interop.Word;

namespace UpcomingEventsHLTV_App
{
    internal class Word_Helper
    {
        private Tournament tournament;
        private List<Team> teams;
        private string PATH;
        public Word_Helper(Tournament tournament, List<Team> teams, string PATH)
        {
            this.tournament = tournament;
            this.teams = teams;
            this.PATH = PATH;
        }

        public void Create()
        {
            // Создание экземпляра приложения Word
            var app = new word.Application();
            // Создание нового документа
            word.Document document = app.Documents.Add();
            // Создание нового абзаца
            word.Paragraph NameParagraph = document.Paragraphs.Add();
            // Получение диапазона (Range) для абзаца
            word.Range NameRange = NameParagraph.Range;
            // Установка текста в диапазон
            NameRange.Text = tournament.Name;
            // Установка стиля для абзаца
            // NameParagraph.set_Style("Заголовок");
            // Добавление нового абзаца после текущего
            NameRange.InsertParagraphAfter();

            word.Paragraph LocationParagraph = document.Paragraphs.Add();
            word.Range LocationRange = LocationParagraph.Range;
            LocationRange.Text = "Location: " + tournament.Location;
            LocationRange.InsertParagraphAfter();

            word.Paragraph DatesParagraph = document.Paragraphs.Add();
            word.Range DatesRange = DatesParagraph.Range;
            DatesRange.Text = "Dates: " + tournament.Date;
            DatesRange.InsertParagraphAfter();

            word.Paragraph PrizePoolParagraph = document.Paragraphs.Add();
            word.Range PrizePoolRange = PrizePoolParagraph.Range;
            if (tournament.PrizePool != "Other")
                PrizePoolRange.Text = "Prize pool: " + tournament.PrizePool + "$";
            else
                PrizePoolRange.Text = "Prize pool: " + tournament.PrizePool;
            PrizePoolRange.InsertParagraphAfter();

            word.Paragraph AmountOfTeamsParagraph = document.Paragraphs.Add();
            word.Range AmountOfTeamsRange = AmountOfTeamsParagraph.Range;
            AmountOfTeamsRange.Text = "Amount of teams: " + tournament.TeamAmount.ToString();
            AmountOfTeamsRange.InsertParagraphAfter();

            word.Paragraph TableParagraph = document.Paragraphs.Add();
            word.Range TableRange = TableParagraph.Range;
            word.Table TeamsTable = document.Tables.Add(TableRange, teams.Count + 1, 2);
            TeamsTable.Borders.InsideLineStyle = TeamsTable.Borders.OutsideLineStyle
                = word.WdLineStyle.wdLineStyleSingle;
            TeamsTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            word.Range cellRange;
            cellRange = TeamsTable.Cell(1, 1).Range;
            cellRange.Text = "Team name";
            cellRange = TeamsTable.Cell(1, 2).Range;
            cellRange.Text = "World ranking";

            TeamsTable.Rows[1].Range.Bold = 1;
            TeamsTable.Rows[1].Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < teams.Count(); i++)
            {
                TeamsTable.Cell(i+2, 1).Range.Text = teams[i].Name;
                TeamsTable.Cell(i+2, 2).Range.Text = teams[i].Rating.ToString();
            }

            document.SaveAs2(PATH);
            document.Close();
            app.Quit();
        }
    }   
}
