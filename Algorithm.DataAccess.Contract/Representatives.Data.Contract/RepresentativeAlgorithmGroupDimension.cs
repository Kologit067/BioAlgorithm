using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.DataAccess.Contract.Representatives.Data.Contract
{
    public class RepresentativeAlgorithmGroupDimension
    {
        public string Algorithm { get; set; }
        public int NumberOfSet { get; set; }
        public int Dimension { get; set; }
        public long Step { get; set; }
        public long TotalCount { get; set; }
        public long NumberOfIteration { get; set; }
        public long TotalDuration { get; set; }
        public long AverageDuration { get; set; }
    }
}
