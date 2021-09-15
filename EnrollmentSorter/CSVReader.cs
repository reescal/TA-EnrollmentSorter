using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnrollmentSorter
{
    public class CSVReader
    {
        public IEnumerable<Enrollee> Enrollees => CsvFileLines.Select(x => new Enrollee(x));
        private DirectoryInfo Path { get; set; }
        private FileInfo[] CsvFiles { get; set; }
        private List<string> CsvFileLines { get; set; }

        public CSVReader(DirectoryInfo path)
        {
            Path = path;
        }

        public void Read()
        {
            CsvFiles = GetCsvFiles();

            CsvFileLines = GetAllLines();
        }

        private FileInfo[] GetCsvFiles()
        {
            var csvFiles = Path.GetFiles("*.csv");
            if (csvFiles.Length == 0)
                throw new FileNotFoundException("No .csv files found.");
            else
                return csvFiles;
        }

        private List<string> GetAllLines()
        {
            List<string> allLines = new();

            foreach (var csvFile in CsvFiles)
            {
                allLines.AddRange(File.ReadAllLines(csvFile.FullName)[1..]);
            }

            var badDataCount = allLines.RemoveAll(x => x.Split(',').Length != 4 ||
                                                    x.Split(',').Any(x => string.IsNullOrEmpty(x)));
            
            Console.WriteLine($"Number of incorrect data found: {badDataCount}");

            return allLines;
        }
    }
}
