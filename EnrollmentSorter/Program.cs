using System;
using System.IO;

namespace EnrollmentSorter
{
    class Program
    {
        private static string Folder = $"Data";
        private static DirectoryInfo Path = new DirectoryInfo($"{Environment.CurrentDirectory}/{Folder}");
        private static DirectoryInfo OutputPath = new DirectoryInfo($"{Environment.CurrentDirectory}/{Folder}/Output");
        static void Main(string[] args)
        {
            try
            {
                CSVReader Reader = new(Path);
                Reader.Read();

                EnrolleeSorter Sorter = new(Reader.Enrollees);

                if (Sorter.SortedEnrollees != null)
                {
                    CSVWriter Writer = new(Sorter.SortedEnrollees);
                    Writer.Write(OutputPath);
                }

            }
            catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
