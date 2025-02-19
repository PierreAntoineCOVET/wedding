using Server.Models;
using FormModel = Server.Models.Form;

namespace Server.DTOs
{
    public class Form
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public required string ConfirmedDays { get; set; }

        public MealChoices MealChoice { get; set; }

        public string? FoodAllergy { get; set; }

        public string? MusicRecommendation { get; set; }

        public string? Other { get; set; }

        public static Form FromModel(FormModel formModel)
        {
            return new Form
            {
                Id = formModel.Id,
                UserId = formModel.UserId,
                ConfirmedDays = Convert.ToString((int)formModel.ConfirmedDays, 2),
                FoodAllergy = formModel.FoodAllergy,
                MealChoice = formModel.MealChoice,
                MusicRecommendation = formModel.MusicRecommendation,
                Other = formModel.Other
            };
        }

        public static FormModel ToModel(Form form)
        {
            return new FormModel
            {
                Id = form.Id ?? 0,
                UserId = form.UserId,
                ConfirmedDays = (Models.Days)Convert.ToInt32(form.ConfirmedDays, 2),
                FoodAllergy = form.FoodAllergy,
                MealChoice = form.MealChoice,
                MusicRecommendation = form.MusicRecommendation,
                Other = form.Other
            };
        }
    }
}
