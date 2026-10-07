using BioAlgorithmViewModel.Representatives.Dto;
using BioAlgorithmModel.RepresentativesModel;
using System.Collections.Generic;
using System.Linq;

namespace BioAlgorithmViewModel.Mappings
{
   
    public static class RepresentativesPerfomanceMappings
    {
        public static RepresentativesPerfomance ToEntity(this RepresentativesPerfomanceDto input)
        {
            return input == null
            ? null
                : new RepresentativesPerfomance()
                {
                    Algorithm = input.Algorithm,
                    NumberOfSet = input.NumberOfSet,
                    Dimension = input.Dimension,
                    Step = input.Step,
                    InputLen = input.InputLen,
                    InputLenSort = input.InputLenSort,
                    InputLenAvg = int.Parse(input.InputLenAvg),
                    InputData = input.InputData,
                    InputDataShort = input.InputDataShort,
                    NumberOfIteration = input.NumberOfIteration,
                    Duration = input.Duration,
                    DurationMilliSeconds = input.DurationMilliSeconds,
                    DateComplete = input.DateComplete,
                    IsComplete = input.IsComplete,
                    LastRoute = input.LastRoute,
                    OptimalRoute = input.OptimalRoute,
                    CountTerminal = int.Parse(input.CountTerminal),
                    BestValue = int.Parse(input.BestValue),
                    UpdateOptcount = int.Parse(input.UpdateOptcount),
                    ElemenationCount = int.Parse(input.BestValue),
                    Isomorphic = input.Isomorphic
                };
        }

        public static IEnumerable<RepresentativesPerfomance> ToEntity(this IEnumerable<RepresentativesPerfomanceDto> input) => input?.Select(i => i.ToEntity()).ToList();

        public static RepresentativesPerfomanceDto Map(this RepresentativesPerfomance input)
        {
            return input == null
                ? null
                : new RepresentativesPerfomanceDto()
                {
                    Algorithm = input.Algorithm,
                    NumberOfSet = input.NumberOfSet,
                    Dimension = input.Dimension,
                    Step = input.Step,
                    InputLen = input.InputLen,
                    InputLenSort = input.InputLenSort,
                    InputLenAvg = input.InputLenAvg.ToString(),
                    InputData = input.InputData,
                    InputDataShort = input.InputDataShort,
                    NumberOfIteration = input.NumberOfIteration,
                    Duration = input.Duration,
                    DurationMilliSeconds = input.DurationMilliSeconds,
                    DateComplete = input.DateComplete,
                    IsComplete = input.IsComplete,
                    LastRoute = input.LastRoute,
                    OptimalRoute = input.OptimalRoute,
                    CountTerminal = input.CountTerminal.ToString(),
                    BestValue = input.BestValue.ToString(),
                    UpdateOptcount = input.UpdateOptcount.ToString(),
                    ElemenationCount = input.BestValue.ToString(),
                    Isomorphic = input.Isomorphic
                };
        }
        public static IEnumerable<RepresentativesPerfomanceDto> Map(this IEnumerable<RepresentativesPerfomance> input) => input?.Select(i => i.Map()).ToList();

    }

}
