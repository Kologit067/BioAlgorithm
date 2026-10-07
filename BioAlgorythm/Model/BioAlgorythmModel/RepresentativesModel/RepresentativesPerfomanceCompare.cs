
namespace BioAlgorithmModel.RepresentativesModel
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesPerfomanceCompare
    //--------------------------------------------------------------------------------------
    public class RepresentativesPerfomanceCompare
    {
        public int NumberOfSet { get; set; }
        public int Dimension { get; set; }
        public long Step { get; set; }
        public string InputData { get; set; }
        public string InputDataShort { get; set; }
        //public string InputLen { get; set; }
        //public string InputLenSort { get; set; }
        //public int InputLenAvg { get; set; }
        public string Algorithm1 { get; set; }
        public string Algorithm2 { get; set; }
        public int BestValue1 { get; set; }
        public int BestValue2 { get; set; }
        public string OptimalRoute1 { get; set; }
        public string OptimalRoute2 { get; set; }
        public long NumberOfIteration1 { get; set; }
        public long NumberOfIteration2 { get; set; }
        public long Duration1 { get; set; }
        public long Duration2 { get; set; }
        public int ElemenationCount1 { get; set; }
        public int ElemenationCount2 { get; set; }


        //public long DurationMilliSeconds { get; set; }
        //public int CountTerminal { get; set; }
        //public int UpdateOptcount { get; set; }

    }
    //--------------------------------------------------------------------------------------
}
