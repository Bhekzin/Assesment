using System;
using System.Collections.Generic;
using System.IO;



namespace GetTopScorerApps
{
    class Program
    {
        static void Main(string[] args)
        {

            // should change this to your csv pc local path string
            string filePath = @"C:\\TestData.csv";



            string csvInput = ReadCSVFromFile(filePath);
            List<ScoreEntry> data = ParseCSV(csvInput);
            List<ScoreEntry> topScorers = FindTopScorers(data);



            Console.WriteLine("Top Scorers:");
            foreach (ScoreEntry scorer in topScorers)
            {
                Console.WriteLine($"{scorer.Name} {scorer.SecondName} - {scorer.Score}");
            }
        }



        static string ReadCSVFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }



        static List<ScoreEntry> ParseCSV(string csvString)
        {
            List<ScoreEntry> data = new List<ScoreEntry>();
            string[] lines = csvString.Trim().Split('\n');



            string[] header = lines[0].Split(',');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                ScoreEntry entry = new ScoreEntry
                {
                    Name = values[0],
                    SecondName = values[1],
                    Score = int.Parse(values[2])
                };
                data.Add(entry);
            }



            return data;
        }



        static List<ScoreEntry> FindTopScorers(List<ScoreEntry> data)
        {
            int topScore = int.MinValue;
            List<ScoreEntry> topScorers = new List<ScoreEntry>();



            foreach (ScoreEntry entry in data)
            {
                if (entry.Score > topScore)
                {
                    topScore = entry.Score;
                    topScorers.Clear();
                    topScorers.Add(entry);
                }
                else if (entry.Score == topScore)
                {
                    topScorers.Add(entry);
                }
            }



            topScorers.Sort((a, b) => string.Compare($"{a.Name} {a.SecondName}", $"{b.Name} {b.SecondName}", StringComparison.Ordinal));
            return topScorers;
        }
    }



    class ScoreEntry
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public int Score { get; set; }
    }
}
