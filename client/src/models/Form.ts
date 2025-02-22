export enum MealChoices {
  None,
  Meat,
  Cheese
}

export type Form = {
  id: number,
  userId: number,
  confirmedDays: string,
  mealChoice: MealChoices,
  foodAllergy: string,
  musicRecommendation: string,
  other: string
}
