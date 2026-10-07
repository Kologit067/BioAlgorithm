using CommonLibrary;
using CommonLibrary.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepresentativesSetTest.Base;
using StatisticsStorage.Accumulators;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepresentativesSet.Greedy.Tests
{
    [TestClass()]
    public class RepresentativesGreedyTests
    {
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase1()
        {
            // arrange
            List<int> list = new List<int>() { 3, 5, 6, 7, 16 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedySimple();

            // act
            representativesGreedy.Execute(listOfSet);
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();

            // assert


        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase2()
        {
            // arrange
            List<int> list = new List<int>() { 3, 5, 7, 10, 24 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedyImprove representativesGreedy = new RepresentativesGreedyImprove();
            string solutionAsStringExpected = "0,3";

            // act
            representativesGreedy.Execute(listOfSet);
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);    

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase3()
        {
            // arrange
            List<int> list = new List<int>() { 5, 11, 12, 18, 21 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedyImprove();
            string solutionAsStringExpected = "1,2";

            // act
            representativesGreedy.Execute(listOfSet);
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyCase4()
        {
            // arrange
            List<int> list = new List<int>() { 3, 5, 7, 10, 20 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedySimple();
            string solutionAsStringExpected = "1,2";

            // act
            representativesGreedy.Execute(listOfSet);
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void RepresentativesGreedyImroveRDCase1()
        {
            // arrange
            List<int> list = new List<int>() { 6, 7, 9, 10, 20 };
            int[][] listOfSet = list.Select(t => BruteForceRepresentativesBinaryNumbders.GetAsElementNumbers(t, 5).ToArray()).ToArray();
            RepresentativesGreedy representativesGreedy = new RepresentativesGreedyImproveRD();
            string solutionAsStringExpected = "2,3";

            // act
            representativesGreedy.Execute(listOfSet);
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
            string solutionAsString = string.Join(",", representativesGreedy.Solution);

            // assert
            Assert.AreEqual(solutionAsStringExpected, solutionAsString);

        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyTest()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesGreedyCompare enumeration = new EnumerateIntegerTrangleForRepresentativesGreedyCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.Combination(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpTest()
        {
            // arrange
            int сardinality = 5;
            int length = 5;
            EnumerateIntegerTrangleForRepresentativesGreedyImpCompare enumeration = new EnumerateIntegerTrangleForRepresentativesGreedyImpCompare(сardinality, length);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.Combination(n, length);
        }
        //--------------------------------------------------------------------------------------
        [TestMethod]
        public void BranchAndBoundCompareGreedyImpOnlyDifTest1()
        {
            // arrange
            int сardinality = 6;
            int length = 6;
            EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif enumeration = 
                new EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif(сardinality, length, 10000);
            // act
            enumeration.Execute();
            // assert
            int n = 1 << сardinality;
            long comb = Combinatorics.Combination(n, length);
        }
    }

    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyCompare : EnumerateRepresentativesTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesBranchAndBoundByValue branchAndBound;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesGreedyCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pCardinality, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;

            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);
            representativesGreedy = new RepresentativesGreedySimple();

            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.Delete(branchAndBound.AlgorithmName, pLength, pCardinality, 1);
            _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyStatisticAccumulator.Delete(representativesGreedy.AlgorithmName, pLength, pCardinality, 1);

            representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
            branchAndBound .StatisticAccumulator = _statisticAccumulator;
        }
        //--------------------------------------------------------------------------------------
        protected override void ActAction(int[][] listOfSet)
        {
            branchAndBound.Execute(listOfSet);
            representativesGreedy.Execute(listOfSet);
            branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();
        }
        //--------------------------------------------------------------------------------------
        protected override void AssertAction()
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

        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompare
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyImpCompare : EnumerateRepresentativesTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpStatisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyImpRDStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesGreedy representativesGreedyImp;
        private RepresentativesGreedy representativesGreedyImpRD;
        private RepresentativesBranchAndBoundByValue branchAndBound;
        //--------------------------------------------------------------------------------------
        private int _wrongResultImpCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultImpCount
        {
            get
            {
                return _wrongResultImpCount;
            }
        }
        //--------------------------------------------------------------------------------------
        private int _wrongResultImpRDCount = 0;
        //--------------------------------------------------------------------------------------
        public int WrongResultImpRDCount
        {
            get
            {
                return _wrongResultImpRDCount;
            }
        }
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesGreedyImpCompare(int pCardinality, int pLength, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pCardinality, pLength, pMinimumValue, pForwardAdditive)
        {
            _fBreakElement = 0;
            _fCardinality = pCardinality;

            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);
            representativesGreedy = new RepresentativesGreedySimple();
            representativesGreedyImp = new RepresentativesGreedyImprove();
            representativesGreedyImpRD = new RepresentativesGreedyImproveRD();

            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _statisticAccumulator.Delete(branchAndBound.AlgorithmName, pLength, pCardinality, 1);
            _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyStatisticAccumulator.Delete(representativesGreedy.AlgorithmName, pLength, pCardinality, 1);
            _greedyImpStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyImpStatisticAccumulator.Delete(representativesGreedyImp.AlgorithmName, pLength, pCardinality, 1);
            _greedyImpRDStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality);
            _greedyImpRDStatisticAccumulator.Delete(representativesGreedyImpRD.AlgorithmName, pLength, pCardinality, 1);

            representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
            representativesGreedyImp.StatisticAccumulator = _greedyImpStatisticAccumulator;
            representativesGreedyImpRD.StatisticAccumulator = _greedyImpRDStatisticAccumulator;
            branchAndBound.StatisticAccumulator = _statisticAccumulator;

        }
        //--------------------------------------------------------------------------------------
        protected override void ActAction(int[][] listOfSet)
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
        protected override void AssertAction()
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
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
            _greedyImpStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif : EnumerateRepresentativesTestBase
    {
        private RepresentativesStatisticAccumulator _statisticAccumulator;
        private RepresentativesStatisticAccumulator _greedyStatisticAccumulator;

        private RepresentativesGreedy representativesGreedy;
        private RepresentativesBranchAndBoundByValue branchAndBound;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangleForRepresentativesGreedyCompareOnlyDif(int pCardinality, int pLength,int bufferSize, int pMinimumValue = 1, int pForwardAdditive = 1)
            : base(pCardinality, pLength, pMinimumValue, pForwardAdditive)
        {
            representativesGreedy = new RepresentativesGreedyImproveRD();
            branchAndBound = new RepresentativesBranchAndBoundByValue(_fCardinality);

            _statisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality,1, bufferSize);
            _statisticAccumulator.Delete(branchAndBound.AlgorithmName, pLength, pCardinality,1);
            _greedyStatisticAccumulator = new RepresentativesStatisticAccumulator(new RepresentativesSaver(), pLength, pCardinality, 1, bufferSize);
            _greedyStatisticAccumulator.Delete(representativesGreedy.AlgorithmName, pLength, pCardinality, 1);

            representativesGreedy.StatisticAccumulator = _greedyStatisticAccumulator;
            branchAndBound.StatisticAccumulator = _statisticAccumulator;
        }
        //--------------------------------------------------------------------------------------
        protected override void ActAction(int[][] listOfSet)
        {
            branchAndBound.Execute(listOfSet);
            representativesGreedy.Execute(listOfSet);
            branchAndBound.OptimalSets = branchAndBound.OptimalSets.OrderBy(s => s).ToList();
            representativesGreedy.Solution = representativesGreedy.Solution.OrderBy(s => s).ToList();

        }
        //--------------------------------------------------------------------------------------
        protected override void AssertAction()
        {
            if (branchAndBound.CurrentMinimum == representativesGreedy.Solution.Count)
            {
                String solutionAsString = representativesGreedy.SolutionAsString;
                Assert.IsTrue(branchAndBound.OptimalSets.Any(o => o == solutionAsString));
                _statisticAccumulator.RemoveLastStatistic();
                _greedyStatisticAccumulator.RemoveLastStatistic();
            }
            else
            {
                _wrongResultCount++;
            }

        }
        //--------------------------------------------------------------------------------------
        protected override void PostAction()
        {
            _statisticAccumulator.SaveRemain();
            _greedyStatisticAccumulator.SaveRemain();
        }
        //--------------------------------------------------------------------------------------
    }

}