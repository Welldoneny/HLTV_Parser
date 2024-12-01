using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingEventsHLTV_App.Models;
using System.Collections.ObjectModel;

namespace UpcomingEventsHLTV_App
{
    internal class Parser
    {
        private List<string> listOfTeams = new List<string>();

        public Parser(string url, List<Tournament> tournaments, List<Team> teams)
        {
            // очищаем базу данных
            using (var db = new HLTV_Context())
            {
                foreach (Tournament item in db.Tournaments.ToList())
                    db.Remove(item);

                foreach (Team item in db.Teams.ToList())
                    db.Remove(item);

                foreach (Tournament_Team item in db.Tournament_Teams.ToList())
                    db.Remove(item);
                db.SaveChanges();

                listOfTeams.Clear();
            }
            // создаем бота селениум и устанавливаем ему ссылку на веб-страницу
            IWebDriver driver = new ChromeDriver();
            driver.Url = url;
            // парсим веб страницу
            var BigEventsNames = driver.FindElements(By.XPath(".//div[@class='big-event-info']//div[@class='big-event-name']"));
            var BigEventsDates = driver.FindElements(By.XPath(".//div[@class='big-event-info']//td[@class='col-value col-date']"));
            var BigEventsPrizes = driver.FindElements(By.XPath(".//td[@class='col-value' and position()=2]"));
            var BigEventsTeams = driver.FindElements(By.XPath(".//td[@class='col-value' and position()=3]"));
            var BigEventsLocations = driver.FindElements(By.XPath(".//span[@class='big-event-location']"));
            var BigEventsLinks = driver.FindElements(By.XPath(".//a[@class='a-reset standard-box big-event']"));
            // добавляем полученные данные в БД
            using (var db = new HLTV_Context())
            {
                for (int i = 0; i < BigEventsNames.Count; i++)
                {
                    string name = BigEventsNames[i].Text;
                    string dates = BigEventsDates[i].Text;
                    string prize = BigEventsPrizes[i].Text;
                    string location = BigEventsLocations[i].Text;
                    int? teamsAmount;
                    if (BigEventsTeams[i].Text == "TBA" || BigEventsTeams[i].Text == "-")
                        teamsAmount = null;
                    else
                        teamsAmount = Int32.Parse(BigEventsTeams[i].Text);
                    // создаем турнир и запихиваем его в БД
                    Tournament tournament = new Tournament
                    {
                        Id = i + 1,
                        Name = name,
                        Date = dates,
                        PrizePool = prize,
                        TeamAmount = teamsAmount,
                        Location = location
                    };
                    tournaments.Add(tournament);
                    db.Tournaments.Add(tournament);
                    db.SaveChanges();
                    string BigEventUrl = BigEventsLinks[i].GetAttribute("href");
                    IWebDriver driverHelper = new ChromeDriver();
                    driverHelper.Url = BigEventUrl;
                    var TeamsNames = driverHelper.FindElements(By.XPath(".//div[@class='teams-attending grid']//div[@class='text']"));
                    var TeamsRankings = driverHelper.FindElements(By.XPath(".//div[@class='event-world-rank']"));
                    
                    for (int j = 0; j < TeamsNames.Count(); j++)
                    {
                        if (!listOfTeams.Contains(TeamsNames[j].Text))
                        {
                            string rating = TeamsRankings[j].Text.Replace('#', ' ').Trim();
                            Team team = new Team
                            {
                                Id = (i + 1) * 100 + j,
                                Name = TeamsNames[j].Text,
                                Rating = Int32.Parse(rating)
                            };
                            listOfTeams.Add(TeamsNames[j].Text);
                            db.Teams.Add(team);
                            teams.Add(team);
                            Tournament_Team tournament_Team = new Tournament_Team
                            {
                                TeamId = team.Id,
                                Team = team,
                                tournament = tournament,
                                TournamentId = tournament.Id
                            };
                            db.Tournament_Teams.Add(tournament_Team);
                            db.SaveChanges();
                        }
                    }
                    driverHelper.Quit();
                }
            }
            driver.Dispose();
        }
    }
}
