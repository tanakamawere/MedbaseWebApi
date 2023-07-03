namespace MedbaseApi.Models;

public record Corrections(int Id, int QuestionId, string QuestionChild, bool SuggestedAnswer, string SuggestedExplanation);