using BioAlgorythmModel.RepresentativesModel;
using GraphLib;
using Representatives.Data;
using System;
using System.Collections.Generic;
using IsomorphismGraph;

namespace RepresentativeServices
{
    public class RepresentativeService
    {
        private RepresentativesRepository representativesRepository;
        public RepresentativeService(RepresentativesRepository representativesRepository) 
        { 
            this.representativesRepository = representativesRepository;
        }
        public void TestIsomorphism(string algorithmName, int dimension, int numberOfSet, long step)
        {
            RepresentativesPerfomanceFilter representativesPerfomanceFilterDto = new RepresentativesPerfomanceFilter
            {
                Algorithm = algorithmName,
                Dimension = dimension,
                NumberOfSet = numberOfSet,
                Step = step
            };
            representativesRepository.ClearIsomorphic(representativesPerfomanceFilterDto);
            List<RepresentativesPerfomance> items = representativesRepository.GetRepresentativePerformanceList(representativesPerfomanceFilterDto, "InputDataShort");
            items.ForEach(item =>
            {
                item.Isomorphic = null;
            });
            for (int i = 0; i < items.Count; i++)
            {
                RepresentativesPerfomance itemOut = items[i];
                if (itemOut.Isomorphic == null)
                {
                    for (int j = i; j < items.Count; j++)
                    {
                        RepresentativesPerfomance itemIn = items[j];
                        if (itemIn.Isomorphic == null)
                        {
                            bool result = true;
                            if (i != j)
                            {
                                MultiGraph graph1 = new MultiGraph(itemOut.InputData);
                                MultiGraph graph2 = new MultiGraph(itemIn.InputData);
                                IsomorphismMultiGraph algorithm = new IsomorphismMultiGraph(graph1, graph2);
                                result = algorithm.IsIsomorphic();
                                if (result)
                                {
                                    itemIn.Isomorphic = itemOut.InputData;
                                    representativesRepository.UpdateIsomorphic(itemIn.RepresentativesPerfomanceId, itemOut.InputData,false);
                                }
                            }
                            else
                                representativesRepository.UpdateIsomorphic(itemIn.RepresentativesPerfomanceId, itemIn.InputData, false);
                        }
                    }
                }
            }
            representativesRepository.CompleteUpdateIsomorphic(false);
        }
    }
}
