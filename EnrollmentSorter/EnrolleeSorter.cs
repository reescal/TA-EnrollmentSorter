using System.Collections.Generic;
using System.Linq;

namespace EnrollmentSorter
{
    public class EnrolleeSorter
    {
        private IEnumerable<Enrollee> UnsortedEnrollees { get; set; }
        public IEnumerable<Enrollee> SortedEnrollees { get; private set; }
        public EnrolleeSorter(IEnumerable<Enrollee> enrollees)
        {
            UnsortedEnrollees = enrollees;
            Sort();
        }

        private void Sort()
        {
            SortedEnrollees = UnsortedEnrollees
                                            .GroupBy(x => x.InsuranceCompany)
                                            .SelectMany(x => x
                                                            .OrderByDescending(y => y.Version)
                                                            .GroupBy(y => y.UserId)
                                                            .Select(y => y.First())
                                                            .OrderBy(y => y.FullName));
        }
    }
}
