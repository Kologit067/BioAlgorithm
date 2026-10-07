using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSet.Greedy;
using RepresentativesSet;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Linq;
using CommonLibrary.Helpers;
using RepresentativesSetTest.Base;
using System.Numerics;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace RepresentativesSetTest.Greedy
{
    [TestClass]
    public class RepresentativesGreedyAdvStepTests
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CombinationAlgorithmCompareTest()
        {
            // arrange
            for (int n = 2; n < 256; n++)
                for (int m = 1; m < n && m < 10; m++)
                {
                    // act
                    long combBigNumber = Combinatorics.CombinationByBigNumber(n, m);
                    long combRecrusive = Combinatorics.CombinationRec(n, m);
                    // assert
                    Assert.AreEqual(combBigNumber, combRecrusive);
                }

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void CombinationAlgorithmCompare_22_19_Test()
        {
            // arrange
            int n = 22;
            int m = 19;

            // act
            long combBigNumber = Combinatorics.CombinationByBigNumber(n, m);
            long combRecrusive = Combinatorics.CombinationRec(n, m);
            // assert
            Assert.AreEqual(combBigNumber, combRecrusive);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImp78Test()
        {
            // arrange
            int сardinality = 8;
            int length = 7;
            EnumerateRepresentativesGreedyAdvStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyAdvStepGreedyImpCompare(сardinality, length, 100000, 500, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.Combination(n, length);
        }
        [TestMethod]
        public void GetFirstPositionTest_511_9_216880080648403998()
        {
            // arrange
            int n = 511;
            int m = 9;
            long number = 216880080648403998;
            Combinatorics.SetCombinationMatrix(511, 9);
            // act
            Combinatorics.GetFirstPosition(n,m,number);
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImp99Test()
        {
            // arrange
            int сardinality = 9;
            int length = 9;
            EnumerateRepresentativesGreedyAdvStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyAdvStepGreedyImpCompare(сardinality, length, 50000, 2000, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.CombinationByBigNumber(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpBigIntTest()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare(сardinality, length, 100000, 2000, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            BigInteger comb = Combinatorics.BigIntegerCombination(n, length);
        }
        ////--------------------------------------------------------------------------------------
        //[TestMethod]
        //public void BranchAndBoundCompareGreedyImpBigInt11_11Test()
        //{
        //    // arrange
        //    int сardinality = 10;
        //    int length = 11;
        //    EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare enumeration =
        //        new EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare(сardinality, length, 100000, 2000, true);
        //    // act
        //    enumeration.Execute();
        //    // assert
        //    int n = 1 << сardinality;
        //    BigInteger comb = Combinatorics.BigIntegerCombination(n, length);
        //}
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpBigInt55Test()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare enumeration =
                new EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare(сardinality, length, 1000, 200, true);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            BigInteger comb = Combinatorics.BigIntegerCombination(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void JustSkipBigInt99Test()
        {
            // arrange
            int сardinality = 9;
            int length = 9;
            int limit = 1 << сardinality;
            long maxCount = 100000;
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            BigInteger number = Combinatorics.BigIntegerCombination(limit, length);
            BigInteger step = BigInteger.Divide(number, maxCount);
            // act
            int i = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (BigInteger counter = step; counter < number && i < 10000; counter += step)
            {
                var currentSet = Combinatorics.SkipEnumerationBigInteger(limit, length, counter);
                i++;
            }
            stopWatch.Stop();
            long time = stopWatch.ElapsedMilliseconds;
            double avg = time * 1.0 / i;
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void JustSkipBigInt1010Test()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            int limit = 1 << сardinality;
            long maxCount = 100000;
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            BigInteger number = Combinatorics.BigIntegerCombination(limit, length);
            BigInteger step = BigInteger.Divide(number, maxCount);
            // act
            int i = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (BigInteger counter = step; counter < number && i < 10000; counter += step)
            {
                var currentSet = Combinatorics.SkipEnumerationBigInteger(limit, length, counter);
                i++;
            }
            stopWatch.Stop();
            long time = stopWatch.ElapsedMilliseconds;
            double avg = time * 1.0 / i;
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void JustSkip99Test()
        {
            // arrange
            int сardinality = 9;
            int length = 9;
            int limit = 1 << сardinality;
            long maxCount = 100000;
            long number = Combinatorics.CombinationByBigNumber(limit, length);
            long step = number/ maxCount;
            Combinatorics.SetCombinationMatrix(limit, length);
            // act
            int i = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (long counter = step; counter < number && i < 10000; counter += step)
            {
                var currentSet = Combinatorics.SkipEnumeration(limit, length, counter);
                i++;
            }
            stopWatch.Stop();
            long time = stopWatch.ElapsedMilliseconds;
            double avg = time * 1.0 / i;
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void JustSkipEx1010Test()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            int limit = 1 << сardinality;
            long maxCount = 10000;
            BigInteger number = Combinatorics.BigIntegerCombination(limit, length);
            BigInteger step = BigInteger.Divide(number, maxCount);
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            Combinatorics.CreateCountForPositionMatrix(limit, length);
            // act
            int i = 0;
            long allTime = 0;
            long maxTime = 0;
            long minTime = 10000000000;
            long allTimeNoRec = 0;
            long maxTimeNoRec = 0;
            long minTimeNoRec = 10000000000;
            long allTimeSaveFP = 0;
            long maxTimeSaveFP = 0;
            long minTimeSaveFP = 10000000000;
            long allTimeSaveFPImp = 0;
            long maxTimeSaveFPImp = 0;
            long minTimeSaveFPImp = 10000000000;
            List<string> lines = new List<string>();
            int? startn = null;
            int? startm = null;
            for (BigInteger counter = step; counter < number && i < 10000; counter += step)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var currentSet = Combinatorics.SkipEnumerationBigInteger(limit, length, counter);
                string showAsString =string.Join(",", currentSet);
                stopWatch.Stop();
                long time = stopWatch.ElapsedMilliseconds;
                allTime += time;
                if (time > maxTime)
                    maxTime = time;
                if (time < minTime)
                    minTime = time;

                Stopwatch stopWatchNoRec = new Stopwatch();
                stopWatchNoRec.Start();
                var currentSetNoRec = Combinatorics.SkipEnumerationNoRecBigInteger(limit, length, counter);
                string showAsStringNoRec = string.Join(",", currentSetNoRec);
                stopWatchNoRec.Stop();
                long timeNoRec = stopWatchNoRec.ElapsedMilliseconds;
                allTimeNoRec += timeNoRec;
                if (timeNoRec > maxTimeNoRec)
                    maxTimeNoRec = timeNoRec;
                if (timeNoRec < minTimeNoRec)
                    minTimeNoRec = timeNoRec;

                Stopwatch stopWatchSaveFP = new Stopwatch();
                stopWatchSaveFP.Start();
                int[] currentSetSaveFP = Combinatorics.SkipEnumerationSaveFPBigInteger(limit, length, counter);
                string showAsStringSaveFP = string.Join(",", currentSetSaveFP);
                stopWatchSaveFP.Stop();
                long timeSaveFP = stopWatchSaveFP.ElapsedMilliseconds;
                allTimeSaveFP += timeSaveFP;
                if (timeSaveFP > maxTimeSaveFP)
                    maxTimeSaveFP = timeSaveFP;
                if (timeSaveFP < minTimeSaveFP)
                    minTimeSaveFP = timeSaveFP;

                Stopwatch stopWatchSaveFPImp = new Stopwatch();
                stopWatchSaveFPImp.Start();
                int[] currentSetSaveFPImp = Combinatorics.SkipEnumerationSaveFPImpBigInteger(limit, length, counter, startn, startm);
                (startn, startm) = currentSetSaveFPImp.Select((f,ind) => (f,ind)).FirstOrDefault( a => a.f > a.ind+1 );
                if (startm.HasValue)
                    startm += 1;
                string showAsStringSaveFPImp = string.Join(",", currentSetSaveFPImp);
                stopWatchSaveFPImp.Stop();
                long timeSaveFPImp = stopWatchSaveFPImp.ElapsedMilliseconds;
                allTimeSaveFPImp += timeSaveFPImp;
                if (timeSaveFPImp > maxTimeSaveFPImp)
                    maxTimeSaveFPImp = timeSaveFPImp;
                if (timeSaveFPImp < minTimeSaveFPImp)
                    minTimeSaveFPImp = timeSaveFPImp;

                Assert.AreEqual(showAsString, showAsStringNoRec);
                Assert.AreEqual(showAsString, showAsStringSaveFP);
                Assert.AreEqual(showAsString, showAsStringSaveFPImp);

                lines.Add($"{i.ToString().PadLeft(10)} {counter.ToString().PadLeft(30)}  {showAsString.PadLeft(60)}  {time.ToString().PadLeft(10)}  {timeNoRec.ToString().PadLeft(10)}  {timeSaveFP.ToString().PadLeft(10)}  {timeSaveFPImp.ToString().PadLeft(10)}");
                i++;
            }
            double avg = allTime * 1.0 / i;
            lines.Add($"minTime: {minTime.ToString()}    maxTime: {maxTime.ToString()}    avg: {avg.ToString()} ");
            double avgNoRec = allTimeNoRec * 1.0 / i;
            lines.Add($"NoRecminTime: {minTimeNoRec.ToString()}    maxTime: {maxTimeNoRec.ToString()}    avg: {avgNoRec.ToString()} ");
            double avgSaveFP = allTimeSaveFP * 1.0 / i;
            lines.Add($"SaveFPminTime: {minTimeSaveFP.ToString()}    maxTime: {maxTimeSaveFP.ToString()}    avg: {avgSaveFP.ToString()} ");
            double avgSaveFPImp = allTimeSaveFPImp * 1.0 / i;
            lines.Add($"SaveFPminTime: {minTimeSaveFPImp.ToString()}    maxTime: {maxTimeSaveFPImp.ToString()}    avg: {avgSaveFPImp.ToString()} ");
            File.WriteAllLines("JustSkipEx1010Test.txt", lines);
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void JustSkipEx1010_500Test()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            int limit = 1 << сardinality;
            long maxCount = 500;
            BigInteger number = Combinatorics.BigIntegerCombination(limit, length);
            BigInteger step = BigInteger.Divide(number, maxCount);
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            Combinatorics.CreateCountForPositionMatrix(limit, length);
            // act
            int i = 0;
            long allTime = 0;
            long maxTime = 0;
            long minTime = 10000000000;
//            long allTimeNoRec = 0;
//            long maxTimeNoRec = 0;
//            long minTimeNoRec = 10000000000;
//            long allTimeSaveFP = 0;
//            long maxTimeSaveFP = 0;
//            long minTimeSaveFP = 10000000000;
            long allTimeSaveFPImp = 0;
            long maxTimeSaveFPImp = 0;
            long minTimeSaveFPImp = 10000000000;
            List<string> lines = new List<string>();
            int? startn = null;
            int? startm = null;
            for (BigInteger counter = step; counter < number && i < 500; counter += step)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var currentSet = Combinatorics.SkipEnumerationBigInteger(limit, length, counter);
                string showAsString = string.Join(",", currentSet);
                stopWatch.Stop();
                long time = stopWatch.ElapsedMilliseconds;
                allTime += time;
                if (time > maxTime)
                    maxTime = time;
                if (time < minTime)
                    minTime = time;

                Stopwatch stopWatchSaveFPImp = new Stopwatch();
                stopWatchSaveFPImp.Start();
                int[] currentSetSaveFPImp = Combinatorics.SkipEnumerationSaveFPImpBigInteger(limit, length, counter, startn, startm);
                (startn, startm) = currentSetSaveFPImp.Select((f, ind) => (f, ind)).FirstOrDefault(a => a.f > a.ind + 1);
                if (startm.HasValue)
                    startm += 1;
                string showAsStringSaveFPImp = string.Join(",", currentSetSaveFPImp);
                stopWatchSaveFPImp.Stop();
                long timeSaveFPImp = stopWatchSaveFPImp.ElapsedMilliseconds;
                allTimeSaveFPImp += timeSaveFPImp;
                if (timeSaveFPImp > maxTimeSaveFPImp)
                    maxTimeSaveFPImp = timeSaveFPImp;
                if (timeSaveFPImp < minTimeSaveFPImp)
                    minTimeSaveFPImp = timeSaveFPImp;

                Assert.AreEqual(showAsString, showAsStringSaveFPImp);

                lines.Add($"{i.ToString().PadLeft(10)} {counter.ToString().PadLeft(30)}  {showAsString.PadLeft(60)}  {time.ToString().PadLeft(10)}  {timeSaveFPImp.ToString().PadLeft(10)}");
                i++;
            }
            double avg = allTime * 1.0 / i;
            lines.Add($"minTime: {minTime.ToString()}    maxTime: {maxTime.ToString()}    avg: {avg.ToString()} ");
            double avgSaveFPImp = allTimeSaveFPImp * 1.0 / i;
            lines.Add($"SaveFPminTime: {minTimeSaveFPImp.ToString()}    maxTime: {maxTimeSaveFPImp.ToString()}    avg: {avgSaveFPImp.ToString()} ");
            File.WriteAllLines("JustSkipEx1010Test.txt", lines);
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SkipEnumerationBigInteger1010Test()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            int limit = 1 << сardinality;
            long maxCount = 100000;
            BigInteger number = Combinatorics.BigIntegerCombination(limit, length);
            BigInteger step = BigInteger.Divide(number, maxCount);
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            // act
            BigInteger counter = step;
            counter = BigInteger.Multiply(counter, 973);
            var currentSet = Combinatorics.SkipEnumerationNoRecBigInteger(limit, length, counter);
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void GetCountForPositionNoRecBigIntegerCompareTest()
        {
            // arrange
            int length = 8;
            //            int limit = 1 << сardinality;
            int limit = 20;
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            // act
            var countForPosition = Combinatorics.GetCountForPositionBigInteger(limit, length, 11, 4);
            var countForPositionNoRec = Combinatorics.GetCountForPositionNoRecBigInteger(limit, length, 11, 4);
            // assert
            Assert.AreEqual(countForPosition, countForPositionNoRec);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SkipEnumerationSaveFPImpBigIntegerTest()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            int limit = 1 << сardinality;
            //int limit = 20;
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            Combinatorics.CreateCountForPositionMatrix(limit, length);           // act
            var s = Combinatorics.SkipEnumerationSaveFPImpBigInteger(limit, length, BigInteger.Parse( "3309232088236359241539" ), null, 1);
            // assert
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void SkipEnumerationSaveFPBigIntegerTest()
        {
            // arrange
            int сardinality = 10;
            int length = 10;
            int limit = 1 << сardinality;
            //int limit = 20;
            Combinatorics.SetCombinationBigIntegerMatrix(limit, length);
            Combinatorics.CreateCountForPositionMatrix(limit, length);           // act
            var s = Combinatorics.SkipEnumerationSaveFPBigInteger(limit, length, BigInteger.Parse("3309232088236359241539"));
            var s0 = Combinatorics.SkipEnumerationSaveFPImpBigInteger(limit, length, BigInteger.Parse("3309232088236359241539"), null, 1);
            // assert
            for (int i = 0; i < s.Length; i++)
                Assert.AreEqual(s[i], s0[i]);
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateRepresentativesGreedyAdvStepGreedyImpCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateRepresentativesGreedyAdvStepGreedyImpCompare : EnumerateRepresentativesAdvStepTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesGreedy representativesGreedyImp;
        private RepresentativesGreedy representativesGreedyImpRD;
        private RepresentativesBranchAndBoundByValue branchAndBound;

        protected long _maxCount;
        protected long number;
        //--------------------------------------------------------------------------------------
        protected long _step;
        public long Step
        {
            get
            {
                return _step;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateRepresentativesGreedyAdvStepGreedyImpCompare(int pCardinality, int pLength, long maxCount, int bufferSize, bool isSave, int pMinimumValue = 1, int pForwardAdditive = 1)
        {
            _fLimit = (1 << pCardinality) - 1;
            _fSize = pLength;
            _fCardinality = pCardinality;
            _maxCount = maxCount;
            number = Combinatorics.CombinationByBigNumber(_fLimit, _fSize);
            _step = (long)(number / _maxCount);

            representativesGreedy = new RepresentativesGreedySimple();
            representativesGreedyImp = new RepresentativesGreedyImprove();
            representativesGreedyImpRD = new RepresentativesGreedyImproveRD();
            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);


            if (isSave)
            {
                _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _statisticAccumulator.Delete(branchAndBound.AlgorithmName, pLength, pCardinality, _step);
                _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyStatisticAccumulator.Delete(representativesGreedy.AlgorithmName, pLength, pCardinality, _step);
                _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyImpStatisticAccumulator.Delete(representativesGreedyImp.AlgorithmName, pLength, pCardinality, _step);
                _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, _step, bufferSize);
                _greedyImpRDStatisticAccumulator.Delete(representativesGreedyImpRD.AlgorithmName, pLength, pCardinality, _step);

                representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
                representativesGreedyImp.StatisticAccumulator = _greedyImpStatisticAccumulator;
                representativesGreedyImpRD.StatisticAccumulator = _greedyImpRDStatisticAccumulator;
                branchAndBound.StatisticAccumulator = _statisticAccumulator;
            }
            Combinatorics.SetCombinationMatrix(_fLimit, _fSize);
        }
        //--------------------------------------------------------------------------------------
        public bool Execute()
        {
            for (long counter = _step; counter < number; counter += _step)
            {
                _fCurrentSet = Combinatorics.SkipEnumeration(_fLimit, _fSize, counter);
                int[][] listOfSet = GetAndTestListOfSet();
                if (listOfSet != null)
                {
                    // act
                    ActAction(listOfSet);
                    // assert
                    AssertAction();
                }
            }
            PostAction();
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected void ActAction(int[][] listOfSet)
        {
            branchAndBound.Execute(listOfSet);
            representativesGreedy.Execute(listOfSet);
            representativesGreedyImp.Execute(listOfSet);
            representativesGreedyImpRD.Execute(listOfSet);
            branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            representativesGreedyImp.Solution = representativesGreedyImp.Solution.OrderBy(s => s).ToList();
            representativesGreedyImpRD.Solution = representativesGreedyImpRD.Solution.OrderBy(s => s).ToList();

        }
        //--------------------------------------------------------------------------------------
        protected void AssertAction()
        {
            if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
            {
                String solutionAsString = representativesGreedy.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImp.Solution.Count)
            {
                String solutionAsString = representativesGreedyImp.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImpRD.Solution.Count)
            {
                String solutionAsString = representativesGreedyImpRD.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpRDCount++;
            }

        }
        //--------------------------------------------------------------------------------------
        protected void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
            _greedyImpStatisticAccumulator.SaveRemain();
            _greedyImpRDStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Length > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i));
                return "Empty";
            }
        }        
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare : EnumerateRepresentativesAdvStepTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesGreedy representativesGreedyImp;
        private RepresentativesGreedy representativesGreedyImpRD;
        private RepresentativesBranchAndBoundByValue branchAndBound;

        protected BigInteger _maxCount;
        protected BigInteger number;
        //--------------------------------------------------------------------------------------
        protected BigInteger _step;
        public BigInteger Step
        {
            get
            {
                return _step;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateRepresentativesGreedyBigIntStepGreedyImpCompare(int pCardinality, int pLength, long maxCount, int bufferSize, bool isSave, int pMinimumValue = 1, int pForwardAdditive = 1)
        {
            _fLimit = (1 << pCardinality) - 1;
            _fSize = pLength;
            _fCardinality = pCardinality;
            _maxCount = maxCount;
            number = Combinatorics.BigIntegerCombination(_fLimit, _fSize);
            _step = BigInteger.Divide(number, _maxCount);

            representativesGreedy = new RepresentativesGreedySimple();
            representativesGreedyImp = new RepresentativesGreedyImprove();
            representativesGreedyImpRD = new RepresentativesGreedyImproveRD();
            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);


            if (isSave)
            {
                _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _statisticAccumulator.Delete(branchAndBound.AlgorithmName, pLength, pCardinality, (decimal)_step);
                _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _greedyStatisticAccumulator.Delete(representativesGreedy.AlgorithmName, pLength, pCardinality, (decimal)_step);
                _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _greedyImpStatisticAccumulator.Delete(representativesGreedyImp.AlgorithmName, pLength, pCardinality, (decimal)_step);
                _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, (decimal)_step, bufferSize);
                _greedyImpRDStatisticAccumulator.Delete(representativesGreedyImpRD.AlgorithmName, pLength, pCardinality, (decimal)_step);

                representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
                representativesGreedyImp.StatisticAccumulator = _greedyImpStatisticAccumulator;
                representativesGreedyImpRD.StatisticAccumulator = _greedyImpRDStatisticAccumulator;
                branchAndBound.StatisticAccumulator = _statisticAccumulator;
            }
            Combinatorics.SetCombinationBigIntegerMatrix(_fLimit, _fSize);
            Combinatorics.CreateCountForPositionMatrix(_fLimit, _fSize);
        }
        //--------------------------------------------------------------------------------------
        public bool Execute()
        {
            int? startn = null;
            int? startm = null;
            for (BigInteger counter = _step; counter < number; counter += _step)
            {
                _fCurrentSet = Combinatorics.SkipEnumerationSaveFPImpBigInteger(_fLimit, _fSize, counter, startn, startm);
                (startn, startm) = _fCurrentSet.Select((f, ind) => (f, ind)).FirstOrDefault(a => a.f > a.ind + 1);
                if (startm.HasValue)
                    startm += 1;
                int[][] listOfSet = GetAndTestListOfSet();
                if (listOfSet != null)
                {
                    // act
                    ActAction(listOfSet);
                    // assert
                    AssertAction();
                }
            }
            PostAction();
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected void ActAction(int[][] listOfSet)
        {
            branchAndBound.Execute(listOfSet);
            representativesGreedy.Execute(listOfSet);
            representativesGreedyImp.Execute(listOfSet);
            representativesGreedyImpRD.Execute(listOfSet);
            branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            representativesGreedyImp.Solution = representativesGreedyImp.Solution.OrderBy(s => s).ToList();
            representativesGreedyImpRD.Solution = representativesGreedyImpRD.Solution.OrderBy(s => s).ToList();

        }
        //--------------------------------------------------------------------------------------
        protected void AssertAction()
        {
            if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
            {
                String solutionAsString = representativesGreedy.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImp.Solution.Count)
            {
                String solutionAsString = representativesGreedyImp.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpCount++;
            }
            if (branchAndBound.CurrentMinimum == representativesGreedyImpRD.Solution.Count)
            {
                String solutionAsString = representativesGreedyImpRD.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
            }
            else
            {
                _wrongResultImpRDCount++;
            }

        }
        //--------------------------------------------------------------------------------------
        protected void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
            _greedyImpStatisticAccumulator.SaveRemain();
            _greedyImpRDStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Length > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
    }

}
