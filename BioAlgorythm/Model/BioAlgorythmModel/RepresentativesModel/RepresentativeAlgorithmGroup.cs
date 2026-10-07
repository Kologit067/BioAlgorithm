
namespace BioAlgorithmModel.RepresentativesModel
{
    public class RepresentativeAlgorithmGroup
    {
        public string Algorithm { get; set; }
        public long TotalCount { get; set; }
        public long CountByDimension { get; set; }
        public long Step { get; set; }

        public long NumberOfIteration { get; set; }

        public long TotalDuration { get; set; }

        public long AverageDuration { get; set; }
    }
}
