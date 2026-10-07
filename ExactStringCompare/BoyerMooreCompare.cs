#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class BoyerMooreCompare 
    //--------------------------------------------------------------------------------------
    public class BoyerMooreComparer : StringPreprocessing
    {
        public static readonly string AlgorithmName = "BMC";
        public static readonly string AlgorithmNameBadSymbolAdv = "BMCBSA";
        public static readonly string AlgorithmNameBadSymbol = "BMCBS";
        public static readonly string AlgorithmNameGoodSuffix = "BMCGS";
        public static readonly string AlgorithmNameGoodSuffixBadSymbol = "BMCGSBS";
        //--------------------------------------------------------------------------------------
        private List<int> result = new List<int>();
        //--------------------------------------------------------------------------------------
#if (DEBUG)

        //--------------------------------------------------------------------------------------
        private long coreProcess;
        public long CoreProcess
        {
            get
            {
                return coreProcess;
            }
        }
        //--------------------------------------------------------------------------------------
        private long dictionaryProcess;
        public long DictionaryProcess
        {
            get
            {
                return dictionaryProcess;
            }
        }
        //--------------------------------------------------------------------------------------
        private long outerLoop;
        public long OuterLoop
        {
            get
            {
                return outerLoop;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long coreElapsedTicks;
        public long CoreElapsedTicks
        {
            get
            {
                return coreElapsedTicks;
            }
        }
#endif
        //--------------------------------------------------------------------------------------
//        public List<int> FindSubstringBadSymbolAdv(string text, string pattern)
//        {
//#if (DEBUG)
//            elapsedTicksList.Clear();
//#endif
//            stopwatch = new Stopwatch();
//            stopwatch.Start();

//#if (DEBUG)
//            elapsedTicksList.Add(stopwatch.ElapsedTicks);
//            coreProcess = 0;
//            dictionaryProcess = 0;
//            outerLoop = 0;
//#endif

//        int dictAppl = 0;

//            StatisticAccumulator.CreateStatistics(text, pattern);
//#if (DEBUG)
//            elapsedTicksList.Add(stopwatch.ElapsedTicks);
//#endif
//            result.Clear();
//#if (DEBUG)
//            elapsedTicksList.Add(stopwatch.ElapsedTicks);
//#endif
//            BadSymbolAdvPreprocessString(pattern);
//            int lenPattern = pattern.Length;
//            int i = 0;
//#if (DEBUG)
//            elapsedTicksList.Add(stopwatch.ElapsedTicks);
//#endif
//            while (i <= text.Length - pattern.Length)
//            {
//                int j = pattern.Length - 1;
//                int j0 = j + i;
//                StatisticAccumulator.IterationCountInc(2);
//#if (DEBUG)
//                outerLoop++;
//#endif
//                while (j >= 0)
//                {
//                    StatisticAccumulator.NumberOfComparisonInc();
//                    StatisticAccumulator.IterationCountInc();
//                    if (pattern[j] != text[j0])
//                        break;
//                    j--;
//                    j0--;
//                    StatisticAccumulator.IterationCountInc(2);
//#if (DEBUG)
//                    coreProcess++;
//#endif
//                }
//                if (j < 0)
//                {
//                    result.Add(i);
//                    i++;
//                    StatisticAccumulator.IterationCountInc(2);
//                }
//                else
//                {
//                    StatisticAccumulator.IterationCountInc();
//                    if (rAdvValue.ContainsKey(text[j0]))
//                    {
//                        int l = 0;
//                        StatisticAccumulator.IterationCountInc(3);
//                        dictAppl++;
//                        while (l < rAdvValue[text[j0]].Count && rAdvValue[text[j0]][l] < j)
//                        {
//#if (DEBUG)
//                            dictionaryProcess++;
//#endif
//                            dictAppl++;
//                            dictAppl++;
//                            l++;
//                            StatisticAccumulator.IterationCountInc(3);
//                        }
//                        if (l > 0)
//                        {
//                            l--;
//                            StatisticAccumulator.IterationCountInc();
//                        }
//                        StatisticAccumulator.IterationCountInc(2);
//                        dictAppl++;
//                        dictAppl++;
//                        int maxPos = rAdvValue[text[j0]][l];
//                        if (rAdvValue[text[j0]][l] >= j)
//                        {
//                            i += j + 1;
//                        }
//                        else
//                        {
//                            i += j - maxPos;
//                        }
//                    }
//                    else
//                    {
//                        StatisticAccumulator.IterationCountInc();
//                        i = j0 + 1;
//                    }
//                }
//#if (DEBUG)
//                elapsedTicksList.Add(stopwatch.ElapsedTicks);
//#endif
//            }

//            stopwatch.Stop();
//            elapsedTicks = stopwatch.ElapsedTicks;
//            durationMilliSeconds = stopwatch.ElapsedMilliseconds;
//            _outputPresentation = string.Join(",", result.Select(p => p.ToString()));
//            coreElapsedTicks = durationMilliSeconds - preprocessingElapsedTicks;

//            additionalInfo = $"Pre({PreprocessingElapsedTicks}) Core({CoreElapsedTicks}) ({string.Join(",", elapsedTicksList.Select(p => p.ToString()))})";

//            StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, additionalInfo);
//            ReturnToPool();
//            return result;
//        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringBadSymbolAdv(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

#if (DEBUG)
//            elapsedTicksList.Clear();
            coreProcess = 0;
            dictionaryProcess = 0;
            outerLoop = 0;
#endif

            int dictAppl = 0;

            result.Clear();
#if (DEBUG)
            //elapsedTicksList.Add(stopwatch.ElapsedTicks);
#endif
            BadSymbolAdvPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
#if (DEBUG)
           // elapsedTicksList.Add(stopwatch.ElapsedTicks);
#endif
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
#if (DEBUG)
                outerLoop++;
#endif
                while (j >= 0)
                {
                    StatisticAccumulator.NumberOfComparisonInc();
                    StatisticAccumulator.IterationCountInc();
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                    StatisticAccumulator.IterationCountInc(2);
#if (DEBUG)
                    coreProcess++;
#endif
                }
                if (j < 0)
                {
                    result.Add(i);
                    i++;
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    StatisticAccumulator.IterationCountInc();
                    List<int> rAdv = null;
                    
                    if (rAdvValue.TryGetValue(text[j0], out rAdv))
                    {
                        int l = 0;
                        StatisticAccumulator.IterationCountInc(3);
                        dictAppl++;
                        while (l < rAdv.Count && rAdv[l] < j)
                        {
#if (DEBUG)
                            dictionaryProcess++;
#endif
                            dictAppl++;
                            dictAppl++;
                            l++;
                            StatisticAccumulator.IterationCountInc(3);
                        }
                        if (l > 0)
                        {
                            l--;
                            StatisticAccumulator.IterationCountInc();
                        }
                        StatisticAccumulator.IterationCountInc(2);
                        dictAppl++;
                        dictAppl++;
                        int maxPos = rAdv[l];
                        if (maxPos >= j)
                        {
                            i += j + 1;
                        }
                        else
                        {
                            i += j - maxPos;
                        }
                    }
                    else
                    {
                        StatisticAccumulator.IterationCountInc();
                        i = j0 + 1;
                    }
                }
#if (DEBUG)
               // elapsedTicksList.Add(stopwatch.ElapsedTicks);
#endif
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                elapsedTicks = stopwatch.ElapsedTicks;
                durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            ReturnToPool();
            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringBadSymbol(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            result.Clear();
            BadSymbolPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    StatisticAccumulator.IterationCountInc(2);
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    StatisticAccumulator.IterationCountInc(2);
                    result.Add(i);
                    i++;
                }
                else
                {
                    StatisticAccumulator.IterationCountInc(2);
                    if (rValue.ContainsKey(text[j0]))
                    {
                        StatisticAccumulator.IterationCountInc();
                        if (rValue[text[j0]] >= j)
                            i++;
                        else
                        {
                            i += j - rValue[text[j0]];
                        }
                    }
                    else
                        i = j0 + 1;
                }
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                elapsedTicks = stopwatch.ElapsedTicks;
                durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringGoodSuffix(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            result.Clear();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    StatisticAccumulator.IterationCountInc(2);
                    j--;
                    j0--;
                }
                if (j < 0)
                {
                    result.Add(i);
                    i += lenPattern - llisValue[1];
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    StatisticAccumulator.IterationCountInc();
                    if (j < pattern.Length - 1)
                    {
//                        j++;
                        StatisticAccumulator.IterationCountInc();
                        if (lisValue[j] > 0)
                        {
                            i += lenPattern - lisValue[j] - 1;
                        }
                        else
                        {
                            i += lenPattern - llisValue[j];
                        }
                    }
                    else
                        i++;
                }
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                long elapsedTicks = stopwatch.ElapsedTicks;
                long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else 
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstring(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            result.Clear();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            BadSymbolPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                    StatisticAccumulator.IterationCountInc(2);
                }
                if (j < 0)
                {
                    result.Add(i);
                    i += lenPattern - llisValue[1];
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    int suffixStiff = 1;
                    int symbolStiff = 1;
                    StatisticAccumulator.IterationCountInc(3);
                    if (rValue.ContainsKey(text[j0]))
                    {
                        if (rValue[text[j0]] < j)
                        {
                            symbolStiff  = j - rValue[text[j0]];
                            StatisticAccumulator.IterationCountInc();
                        }
                    }
                    else
                        symbolStiff = j0 + 1 - i;
 
                    if (j < pattern.Length - 1)
                    {
                        StatisticAccumulator.IterationCountInc(2);
//                        j++;
                        if (lisValue[j] > 0)
                        {
                            suffixStiff = lenPattern - lisValue[j] - 1;
                        }
                        else
                        {
                            suffixStiff = lenPattern - llisValue[j];
                        }

                    }

                    StatisticAccumulator.IterationCountInc(2);
                    int stiff = Math.Max(symbolStiff, suffixStiff);
                    i += stiff;
                }
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                long elapsedTicks = stopwatch.ElapsedTicks;
                long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
        public List<int> FindSubstringByGoodSuffixBadSymbolAdv(string text, string pattern, bool isSaveStatisticsForEmpty = true)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            StatisticAccumulator.CreateStatistics(text, pattern);

            result.Clear();
            LliPreprocessString(pattern);
            LiByNPreprocessString(pattern);
            BadSymbolAdvPreprocessString(pattern);
            int lenPattern = pattern.Length;
            int i = 0;
            while (i <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                int j0 = j + i;
                StatisticAccumulator.IterationCountInc(2);
                while (j >= 0)
                {
                    StatisticAccumulator.IterationCountInc();
                    StatisticAccumulator.NumberOfComparisonInc();
                    if (pattern[j] != text[j0])
                        break;
                    j--;
                    j0--;
                    StatisticAccumulator.IterationCountInc(2);
                }
                if (j < 0)
                {
                    result.Add(i);
                    i += lenPattern - llisValue[1];
                    StatisticAccumulator.IterationCountInc(2);
                }
                else
                {
                    int suffixStiff = 1;
                    int symbolStiff = 1;

                    StatisticAccumulator.IterationCountInc(4);
                    List<int> rAdv = null;

                    if (rAdvValue.TryGetValue(text[j0], out rAdv))
                    {
                        StatisticAccumulator.IterationCountInc(3);
                        int l = 0;
                        while (l < rAdv.Count && rAdv[l] < j)
                        {
                            l++;
                            StatisticAccumulator.IterationCountInc(3);
                        }
                        if (l > 0)
                        {
                            l--;
                            StatisticAccumulator.IterationCountInc();
                        }
                        StatisticAccumulator.IterationCountInc(3);
                        int maxPos = rAdv[l];
                        if (maxPos >= j)
                        {
                            symbolStiff = j + 1;
                        }
                        else
                        {
                            symbolStiff = j - maxPos;
                        }
                    }
                    else
                        symbolStiff = j0 + 1 -i;

                    if (j < pattern.Length - 1)
                    {
                        StatisticAccumulator.IterationCountInc(3);
//                        j++;
                        if (lisValue[j] > 0)
                        {
                            suffixStiff = lenPattern - lisValue[j] - 1;
                        }
                        else
                        {
                            suffixStiff = lenPattern - llisValue[j];
                        }

                    }

                    StatisticAccumulator.IterationCountInc(2);
                    int stiff = Math.Max(symbolStiff, suffixStiff);
                    i += stiff;
                }
            }

            stopwatch.Stop();
            if (result.Count > 0 || isSaveStatisticsForEmpty)
            {
                long elapsedTicks = stopwatch.ElapsedTicks;
                long durationMilliSeconds = stopwatch.ElapsedMilliseconds;
                _outputPresentation = string.Join(",", result.Select(p => p.ToString()));

                StatisticAccumulator.SaveStatisticData(_outputPresentation, elapsedTicks, durationMilliSeconds, DateTime.Now, null);
            }
            else
            {
                StatisticAccumulator.RemoveStatisticData();
            }

            return result;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
