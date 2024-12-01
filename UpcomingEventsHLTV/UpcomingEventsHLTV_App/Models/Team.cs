namespace UpcomingEventsHLTV_App.Models
{
    class Team
    {
        public int Id { get; set; }  // Уникальный идентификатор команды
        public required string Name { get; set; }  // Название команды
        public int? Rating { get; set; }  // Рейтинг команды
        //public string? Coach { get; set; }  // Тренер (может быть NULL)

        // Навигационное свойство для игроков в команде
        public ICollection<Player> Players { get; set; }

        // Навигационное свойство для связи многие-к-многим с турнирами
        public ICollection<Tournament_Team> Tournament_Teams { get; set; }
    }
}
