using BioAlgorithmViewModel.Representatives.Dto;
using System.Collections.Generic;

namespace BioAlgorithmModel.RepresentativesModel
{
    public class RepresentativesPerfomanceAsGroup
    {
        public string ColumnGroupName { get; set; }
        public string ColumnGroupValue { get; set; }
        public List<RepresentativesPerfomanceDto> RepresentativesPerfomanceList { get; set; }
    }
}
