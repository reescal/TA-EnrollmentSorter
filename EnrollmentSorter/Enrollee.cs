using System;
using System.Linq;

namespace EnrollmentSorter
{
    public class Enrollee
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int Version { get; set; }
        public string InsuranceCompany { get; set; }

        public Enrollee()
        {

        }
        public Enrollee(string line)
        {
            string[] fields = line.Split(',');
            UserId = fields[0];
            FullName = fields[1];
            Version = Int32.Parse(fields[2]);
            InsuranceCompany = fields[3];
        }

        public override string ToString() => $"{UserId},{FullName},{Version},{InsuranceCompany}";
    }
}
