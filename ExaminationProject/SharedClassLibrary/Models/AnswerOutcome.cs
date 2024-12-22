namespace SharedClassLibrary.Models
{
    public class AnswerOutcome<T> where T : class
    {
        public bool Statement { get; set; }
        public string? Response { get; set; }
        public T? Content { get; set; }
    }
}
