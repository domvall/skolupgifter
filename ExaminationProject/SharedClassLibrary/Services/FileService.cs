using SharedClassLibrary.Interfaces;
using SharedClassLibrary.Models;

namespace SharedClassLibrary.Services
{
    public class FileService : IFileService
    {
        private readonly string _filePath;
        public FileService(string filePath)
        {
            _filePath = filePath;
        }

        public AnswerOutcome<string> GetFromFile()
        {
            try
            {

                if (!File.Exists(_filePath))
                {
                    throw new FileNotFoundException("[Error] File not found.");
                    //return new AnswerOutcome { Statement = false, Response = "Filen hittades inte." };
                }

                using var sr = new StreamReader(_filePath);
                var content = sr.ReadToEnd();

                return new AnswerOutcome<string> { Statement = true, Content = content };
            }
            catch (Exception ex)
            {
                return new AnswerOutcome<string> { Statement = false, Response = ex.Message };
            }
        }

        public AnswerOutcome<string> SaveToFile(string content)
        {
            try
            {
                using var sw = new StreamWriter(_filePath, false);
                sw.WriteLine(content);
                return new AnswerOutcome<string> { Statement = true };
            }
            catch (Exception ex)
            {
                return new AnswerOutcome<string> { Statement = false, Response = ex.Message };
            }
        }
    }
}
