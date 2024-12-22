namespace SharedClassLibrary.Interfaces;
using SharedClassLibrary.Models;
public interface IFileService
{
    public AnswerOutcome<string> SaveToFile(string content);
    public AnswerOutcome<string> GetFromFile();
}