
namespace BioAlgorithmModel.RepresentativesModel
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesPerfomanceCompareFilter
    //--------------------------------------------------------------------------------------
    public class RepresentativesPerfomanceCompareFilter
    {
        public int? Top { get; set; }
        public string Algorithm1 { get; set; }
        public string Algorithm2 { get; set; }
        public int? NumberOfSet { get; set; }
        public int? Dimension { get; set; }
        public long? Step { get; set; }
        public string BestValueCompare { get; set; }
        public string DurationCompare { get; set; }
        public string NumberIterationCompare { get; set; }
        public string ElemenationCountCompare { get; set; }

    }
    //--------------------------------------------------------------------------------------
}
