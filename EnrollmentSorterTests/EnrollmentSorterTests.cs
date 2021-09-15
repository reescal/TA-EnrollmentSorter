using EnrollmentSorter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnrollmentSorterTests
{
    [TestClass]
    public class EnrollmentSorterTests
    {
        [TestMethod]
        public void Folder_Not_Found()
        {
            //arrange
            DirectoryInfo Path = new DirectoryInfo($"{Environment.CurrentDirectory}/DoesntExist");
            CSVReader Reader = new(Path);

            //act & assert
            Assert.ThrowsException<DirectoryNotFoundException>(() => Reader.Read());
        }

        [TestMethod]
        public void No_CSV_Files_In_Folder()
        {
            //arrange
            DirectoryInfo Path = new DirectoryInfo($"{Environment.CurrentDirectory}/ref");
            CSVReader Reader = new(Path);

            //act & assert
            Assert.ThrowsException<FileNotFoundException>(() => Reader.Read());
        }

        [TestMethod]
        public void Incorrect_Data_CSV_File()
        {
            //arrange
            DirectoryInfo Path = new DirectoryInfo($"{Environment.CurrentDirectory}/BadData");
            CSVReader Reader = new(Path);

            //act
            Reader.Read();
            var validDataCount = Reader.Enrollees.Count();

            //assert
            Assert.AreEqual(1, validDataCount);
        }

        [TestMethod]
        public void Only_Latest_Version_After_Sort()
        {
            //arrange & act
            List<Enrollee> enrollees = new()
            {
                new() { UserId = "1", FullName = "A", InsuranceCompany = "Z", Version = 1 },
                new() { UserId = "1", FullName = "A", InsuranceCompany = "Z", Version = 2 }
            };

            EnrolleeSorter sorter = new(enrollees);

            //assert
            Assert.IsFalse(sorter.SortedEnrollees.Any(x => x.Version == 1));
        }

        [TestMethod]
        public void Content_Is_Sorted_By_Name_Ascending()
        {
            //arrange
            List<Enrollee> enrollees = new()
            {
                new() { UserId = "1", FullName = "B", InsuranceCompany = "Z", Version = 1 },
                new() { UserId = "2", FullName = "A", InsuranceCompany = "Z", Version = 1 }
            };

            EnrolleeSorter sorter = new(enrollees);

            //act & assert
            Assert.AreEqual("A", sorter.SortedEnrollees.First().FullName);
        }

        [TestMethod]
        public void Output_File_Count_Equals_Number_Of_Insurers()
        {
            //arrange
            DirectoryInfo OutputPath = new DirectoryInfo($"{Environment.CurrentDirectory}/Data/Output");
            
            List<Enrollee> enrollees = new()
            {
                new() { UserId = "1", FullName = "B", InsuranceCompany = "A", Version = 1 },
                new() { UserId = "2", FullName = "A", InsuranceCompany = "B", Version = 1 }
            };
            
            CSVWriter writer = new(enrollees);

            //act
            writer.Write(OutputPath);
            var csvFileCount = OutputPath.GetFiles("*.csv").Count();

            //assert
            Assert.AreEqual(2, csvFileCount);
        }
    }
}
