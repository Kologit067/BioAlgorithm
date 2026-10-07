using BioAlgorithmViewModel.Representatives;
using BioAlgorithmModel.RepresentativesModel;
using System.Collections.Generic;
using System.Linq;

namespace BioAlgorithmViewModel.Mappings
{
    public static class RepresentativesPerfomanceFilterMappings
    {
        public static RepresentativesPerfomanceFilter Map(this RepresentativesPerfomanceFilterViewModel input)
        {
            return input == null
            ? null
                : new RepresentativesPerfomanceFilter()
                {
                    Top = input.Top,
                    Algorithm = input.Algorithm,
                    NumberOfSet = input.NumberOfSet,
                    Dimension = input.Dimension,
                    Step = input.Step,
                    InputLen = input.InputLen,
                    InputLenSort = input.InputLenSort,
                    NumberOfIterationFrom = input.NumberOfIterationFrom,
                    NumberOfIterationTo = input.NumberOfIterationTo,
                    DurationFrom = input.DurationFrom,
                    DurationTo = input.DurationTo,
                    IsComplete = input.IsComplete,
                    CountTerminalFrom = input.CountTerminalFrom,
                    CountTerminalTo = input.CountTerminalTo,
                    BestValue = input.BestValue
                };
        }

        public static IEnumerable<RepresentativesPerfomanceFilter> Map(this IEnumerable<RepresentativesPerfomanceFilterViewModel> input) => input?.Select(i => i.Map()).ToList();

        public static RepresentativesPerfomanceFilterViewModel ToEntity(this RepresentativesPerfomanceFilter input)
        {
            return input == null
                ? null
                : new RepresentativesPerfomanceFilterViewModel()
                {
                    Top = input.Top,
                    Algorithm = input.Algorithm,
                    NumberOfSet = input.NumberOfSet,
                    Dimension = input.Dimension,
                    InputLen = input.InputLen,
                    InputLenSort = input.InputLenSort,
                    NumberOfIterationFrom = input.NumberOfIterationFrom,
                    NumberOfIterationTo = input.NumberOfIterationTo,
                    DurationFrom = input.DurationFrom,
                    DurationTo = input.DurationTo,
                    IsComplete = input.IsComplete,
                    CountTerminalFrom = input.CountTerminalFrom,
                    CountTerminalTo = input.CountTerminalTo,
                    BestValue = input.BestValue
                };
        }
        public static IEnumerable<RepresentativesPerfomanceFilterViewModel> ToEntity(this IEnumerable<RepresentativesPerfomanceFilter> input) => input?.Select(i => i.ToEntity()).ToList();

    }

}
