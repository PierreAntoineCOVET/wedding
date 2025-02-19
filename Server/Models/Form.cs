namespace Server.Models
{
    public enum MealChoices
    {
        None,
        Meat,
        Cheese
    }

    public class Form
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public Days ConfirmedDays { get; set; }

        public MealChoices MealChoice { get; set; }

        public string? FoodAllergy { get; set; }

        public string? MusicRecommendation { get; set; }

        public string? Other { get; set; }

        public User? User { get; set; }
    }
}
