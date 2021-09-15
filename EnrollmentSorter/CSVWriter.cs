using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnrollmentSorter
{
    public class CSVWriter
    {
        private IEnumerable<Enrollee> SortedEnrollees { get; set; }
        public CSVWriter()
        {

        }
        public CSVWriter(IEnumerable<Enrollee> sortedEnrollees)
        {
            SortedEnrollees = sortedEnrollees;
        }

        public void Write(DirectoryInfo outputPath)
        {
            if (!outputPath.Exists) Directory.CreateDirectory(outputPath.FullName);
            foreach(var insurerGroup in SortedEnrollees.GroupBy(x => x.InsuranceCompany))
            {
                File.WriteAllLines($"{outputPath}/{insurerGroup.Key}.csv", insurerGroup.Select(x => x.ToString()));
            }
        }
    }
}
