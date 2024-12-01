namespace UpcomingEventsHLTV_App.Models
{
    class Tournament
    {
        public int Id { get; set; }  // Уникальный идентификатор турнира
        public required string Name { get; set; }  // Название турнира
        public string Date { get; set; }  // Дата проведения турнира
        public string Location { get; set; }  // Место проведения (может быть NULL)
        public string PrizePool { get; set; }  // Призовой фонд (может быть NULL)
        public int? TeamAmount { get; set; }  // Количество команд (может быть NULL)

        // Навигационное свойство для обеспечения связи многие-к-многим с командами
        public ICollection<Tournament_Team> Tournament_Teams { get; set; }
    }
}
