using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentativesSet
{
    //--------------------------------------------------------------------------------------
    // class RepresentativesBranchAndBound
    //--------------------------------------------------------------------------------------
    public class RepresentativesBranchAndBound : RepresentativesBranchAndBoundByValue
    {
//        protected int[][] listOfSetAsBinary;
        protected List<int>[] listOfElements;
        protected int numberOfElement;
        protected int[] counterOfSet;
        protected int commonCounter;
        //--------------------------------------------------------------------------------------
        public RepresentativesBranchAndBound(int pLength)
            : base(pLength)
        {
        }
        //-----------------------------------------------------------------------------------
        public override void Execute(int[][] pListOfSet)
        {
            listOfSet = pListOfSet;
            listOfSetAsNumber = listOfSet.Select(s => BruteForceRepresentativesBinaryNumbders.ElementNumbersToLongAsBinaryVector(s)).ToArray();
            if (listOfSet.Any(s => s.Any(e => e >= _fSize)))
                throw new ArgumentException("Element of set can not be > Length.");
            numberOfElement = pListOfSet.Max(x => x.Max())+1;
            listOfElements = new List<int>[numberOfElement];
            counterOfSet = new int[pListOfSet.Length];
            for (int i = 0; i < pListOfSet.Length; i++)
            {
                foreach (int e in pListOfSet[i])
                {
                    if (listOfElements[e] == null)
                        listOfElements[e] = new List<int>();
                    listOfElements[e].Add(i);
                }
            }
            _inputData = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSet));
            _inputDataShort = (Newtonsoft.Json.JsonConvert.SerializeObject(listOfSetAsNumber));

            _fCurrentOptimalSet = _fCurrentSet.ToList();
            currentMinimum = _fSize;
            _fOptimalSets = new List<string>();

            //listOfSetAsBinary = new int[listOfSet.Length][];
            //for (int i = 0; i < listOfSetAsBinary.Length; i++)
            //{
            //    listOfSetAsBinary[i] = new int[pLength];
            //    foreach (int p in listOfSet[i])
            //        listOfSetAsBinary[i][(1 << p)] = 1;
            //}
            //Parallel.For(0, listOfSetAsBinary.Length, i =>
            //{
            //    listOfSetAsBinary[i] = new int[pLength];
            //    foreach (int p in listOfSet[i])
            //        listOfSetAsBinary[i][(1 << p)] = 1;

            //});
            Execute();
        }        
        //--------------------------------------------------------------------------------------
        protected override void RemoveAction(int element)
        {
            base.RemoveAction(element);
            if (_fCurrentSet[_fCurrentPosition] == 1)
            {
                if (_fCurrentPosition < listOfElements.Length)
                {
                    foreach (int i in listOfElements[_fCurrentPosition])
                    {
                        counterOfSet[i] -= 1;
                        if (counterOfSet[i] == 0)
                            commonCounter--;
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void AddAction(int element)
        {
            base.AddAction(element);
            if (_fCurrentSet[_fCurrentPosition] == 1)
            {
                if (_fCurrentPosition < listOfElements.Length)
                {

                    foreach (int i in listOfElements[_fCurrentPosition])
                    {
                        counterOfSet[i] += 1;
                        if (counterOfSet[i] == 1)
                            commonCounter++;
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------
        protected override void SupplementInitial()
        {
            StatisticAccumulator.CreateStatistics(listOfSet, _inputDataShort, AlgorithmName);
            _currentCardinality = 0;
            AddAction(_fCurrentSet[0]);
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {

            if (commonCounter == counterOfSet.Length && _currentCardinality <= currentMinimum)
            {
                UpdateOptimalResults(_currentCardinality);
            }
            return false;

        }
        //-----------------------------------------------------------------------------------
        protected override bool IsCompleteByCardinality()
        {
            return _currentCardinality > currentMinimum || commonCounter == counterOfSet.Length;
        }        
        //--------------------------------------------------------------------------------------
    }
}
