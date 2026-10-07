using CommonLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContract
{
    public interface IRepresentativesStatisticAccumulator
    {
        //--------------------------------------------------------------------------------------------------------------------
        void CreateStatistics(int[][] listOfSet, string inputDataShort, string algorithm);
        //--------------------------------------------------------------------------------------------------------------------
        void SaveStatisticData(long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, List<string> optimalSets, int bestValue);
        //--------------------------------------------------------------------------------------------------------------------
        void IterationCountInc();
        //--------------------------------------------------------------------------------------------------------------------
        void TerminalCountInc();
        //--------------------------------------------------------------------------------------------------------------------
        void UpdateOptcountInc();
        //--------------------------------------------------------------------------------------------------------------------
        void ElemenationCountInc();
        //--------------------------------------------------------------------------------------------------------------------
        void SaveRemain();
        //--------------------------------------------------------------------------------------------------------------------
        string Delete(string algorithm, int? numberOfSet = null, int? dimension = null, decimal? step = null);
        //--------------------------------------------------------------------------------------------------------------------
    }
}
