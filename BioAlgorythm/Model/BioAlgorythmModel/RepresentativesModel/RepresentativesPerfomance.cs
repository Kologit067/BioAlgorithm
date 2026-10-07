using System;

namespace BioAlgorithmModel.RepresentativesModel
{
    public class RepresentativesPerfomance
    {
        public long RepresentativesPerfomanceId { get; set; }
        public string Algorithm { get; set; }
        public int NumberOfSet { get; set; }
        public int Dimension { get; set; }
        public long Step { get; set; }
        public string InputLen { get; set; }
        public string InputLenSort { get; set; }
        public int InputLenAvg { get; set; }
        public string InputData { get; set; }
        public string InputDataShort { get; set; }
        public long NumberOfIteration { get; set; }
        public long Duration { get; set; }
        public long DurationMilliSeconds { get; set; }
        public DateTime DateComplete { get; set; }
        public bool IsComplete { get; set; }
        public string LastRoute { get; set; }
        public string OptimalRoute { get; set; }
        public int CountTerminal { get; set; }
        public int BestValue { get; set; }
        public int UpdateOptcount { get; set; }
        public int ElemenationCount { get; set; }
        public string Isomorphic { get; set; }
    }
}
