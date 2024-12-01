namespace UpcomingEventsHLTV_App.Models
{
    class Tournament_Team
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }  // Связь с командой

        public int TournamentId { get; set; }
        public Tournament tournament { get; set; }  // Связь с турниром
    }
}
