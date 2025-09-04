namespace BookOrganizer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? description { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; }
        public string Genre { get; set; } = string.Empty;   
        public int actualPage { get; set; }
        public int TotalPages { get; set; }


        public int GetPercentageCompleted()
        {
            if (actualPage == 0) return 0;
            return (int)Math.Round((double)(actualPage * 100) / TotalPages);
        }
    }
}
