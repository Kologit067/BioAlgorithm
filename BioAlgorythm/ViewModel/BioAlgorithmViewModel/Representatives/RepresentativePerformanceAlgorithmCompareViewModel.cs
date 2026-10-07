using BioAlgorithmViewModel.Common;
using BioAlgorithmModel.RepresentativesModel;
using Representatives.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BioAlgorithmViewModel.Helpers;
using System.Windows.Input;

namespace BioAlgorithmViewModel.Representatives
{
    //----------------------------------------------------------------------------------------------------------------------
    // class RepresentativePerformanceAlgorithmCompareViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class RepresentativePerformanceAlgorithmCompareViewModel : ViewModelBase
    {
        private RepresentativesRepository representativesRepository;
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<RepresentativeAlgorithWithDimension> algorithmWithDimensions;
        public ObservableCollection<RepresentativeAlgorithWithDimension> AlgorithmWithDimensions
        {
            get
            {
                if ( algorithmWithDimensions == null )
                {
                    List<RepresentativeAlgorithWithDimension> list = representativesRepository.GetRepresentativeAlgorithmWithDimensions();
                    algorithmWithDimensions = list.ToObservable();
                }
                return algorithmWithDimensions;
            }
        }
        ////----------------------------------------------------------------------------------------------------------------------
        //private ObservableCollection<RepresentativeAlgorithmCompare> representativeAlgorithmCompareList;
        //public ObservableCollection<RepresentativeAlgorithmCompare> RepresentativeAlgorithmCompareList
        //{
        //    get
        //    {
        //        return representativeAlgorithmCompareList;
        //    }
        //    set
        //    {
        //        representativeAlgorithmCompareList = value;
        //        OnPropertyChanged(nameof(RepresentativeAlgorithmCompareList));
        //    }
        //}
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<int> numberOfSetItems;
        public ObservableCollection<int> NumberOfSetItems
        {
            get
            {
                return numberOfSetItems;
            }
            set
            {
                numberOfSetItems = value;
                OnPropertyChanged(nameof(NumberOfSetItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private int selectedNumberOfSet;
        public int SelectedNumberOfSet
        {
            get
            {
                return selectedNumberOfSet;
            }
            set
            {
                selectedNumberOfSet = value;
                OnPropertyChanged(nameof(SelectedNumberOfSet));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<int> dimensionItems;
        public ObservableCollection<int> DimensionItems
        {
            get
            {
                return dimensionItems;
            }
            set
            {
                dimensionItems = value;
                OnPropertyChanged(nameof(DimensionItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private int selectedDimension;
        public int SelectedDimension
        {
            get
            {
                return selectedDimension;
            }
            set
            {
                selectedDimension = value;
                OnPropertyChanged(nameof(SelectedDimension));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<long> stepItems;
        public ObservableCollection<long> StepItems
        {
            get
            {
                return stepItems;
            }
            set
            {
                stepItems = value;
                OnPropertyChanged(nameof(StepItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private long selectedStep;
        public long SelectedStep
        {
            get
            {
                return selectedStep;
            }
            set
            {
                selectedStep = value;
                OnPropertyChanged(nameof(SelectedStep));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<string> algorithmItems;
        public ObservableCollection<string> AlgorithmItems
        {
            get
            {
                return algorithmItems;
            }
            set
            {
                algorithmItems = value;
                OnPropertyChanged(nameof(AlgorithmItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedAlgorithm1;
        public string SelectedAlgorithm1
        {
            get
            {
                return selectedAlgorithm1;
            }
            set
            {
                selectedAlgorithm1 = value;
                OnPropertyChanged(nameof(SelectedAlgorithm1));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedAlgorithm2;
        public string SelectedAlgorithm2
        {
            get
            {
                return selectedAlgorithm2;
            }
            set
            {
                selectedAlgorithm2 = value;
                OnPropertyChanged(nameof(SelectedAlgorithm2));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<string> compareOptionItems;
        public ObservableCollection<string> CompareOptionItems
        {
            get
            {
                return compareOptionItems;
            }
            set
            {
                compareOptionItems = value;
                OnPropertyChanged(nameof(CompareOptionItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedBestValueCompare;
        public string SelectedBestValueCompare
        {
            get
            {
                return selectedBestValueCompare;
            }
            set
            {
                selectedBestValueCompare = value;
                OnPropertyChanged(nameof(SelectedBestValueCompare));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedDurationCompare;
        public string SelectedDurationCompare
        {
            get
            {
                return selectedDurationCompare;
            }
            set
            {
                selectedDurationCompare = value;
                OnPropertyChanged(nameof(SelectedDurationCompare));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedcbNumberIterationCompare;
        public string SelectedcbNumberIterationCompare
        {
            get
            {
                return selectedcbNumberIterationCompare;
            }
            set
            {
                selectedcbNumberIterationCompare = value;
                OnPropertyChanged(nameof(SelectedcbNumberIterationCompare));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedElemenationCountCompare;
        public string SelectedElemenationCountCompare
        {
            get
            {
                return selectedElemenationCountCompare;
            }
            set
            {
                selectedElemenationCountCompare = value;
                OnPropertyChanged(nameof(SelectedElemenationCountCompare));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<RepresentativesPerfomanceCompare> representativesPerfomanceCompareList;
        public ObservableCollection<RepresentativesPerfomanceCompare> RepresentativesPerfomanceCompareList
        {
            get
            {
                return representativesPerfomanceCompareList;
            }
            set
            {
                representativesPerfomanceCompareList = value;
                OnPropertyChanged(nameof(RepresentativesPerfomanceCompareList));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativesPerfomanceCompare selectedRepresentativesPerfomanceCompare;
        public RepresentativesPerfomanceCompare SelectedRepresentativesPerfomanceCompare
        {
            get
            {
                return selectedRepresentativesPerfomanceCompare;
            }
            set
            {
                selectedRepresentativesPerfomanceCompare = value;
                OnPropertyChanged(nameof(SelectedRepresentativesPerfomanceCompare));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public RepresentativePerformanceAlgorithmCompareViewModel(RepresentativesRepository representativesRepository)
        {
            CompareOptionItems = new ObservableCollection<string>()
            {
                "N/A",
                "1 > 2",
                "1 < 2",
                "1 <> 2",
                "1 == 2"
            };
            PropertyChanged += RepresentativePerformanceAlgorithmCompareViewModel_PropertyChanged;
            this.representativesRepository = representativesRepository;
            IEnumerable<int> list = AlgorithmWithDimensions.GroupBy(a => a.NumberOfSet).OrderBy(n => n.Key).Select(n => n.Key);
            NumberOfSetItems = list.ToObservable<int>();
            if (NumberOfSetItems.Count > 0)
                SelectedNumberOfSet = NumberOfSetItems[0];
            //            RepresentativeAlgorithmCompareList = new ObservableCollection<RepresentativeAlgorithmCompare>();
            SelectedBestValueCompare = "1 > 2";
            SelectedcbNumberIterationCompare = "N/A";
            SelectedDurationCompare = "N/A";
            SelectedElemenationCountCompare = "N/A";
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void RepresentativePerformanceAlgorithmCompareViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedNumberOfSet":
                    UpdateDimensionItems();
                    UpdateStepItems();
                    UpdateAlgorithmItems();
                    break;
                case "SelectedDimension":
                    UpdateStepItems();
                    UpdateAlgorithmItems();
                    break;
                case "SelectedStep":
                    UpdateAlgorithmItems();
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public void UpdateDimensionItems()
        {
            var list = AlgorithmWithDimensions.Where(a => a.NumberOfSet == selectedNumberOfSet).GroupBy(a => a.Dimension).OrderBy(n => n.Key).Select(n => n.Key);
            DimensionItems = list.ToObservable<int>();
            if (!DimensionItems.Contains(SelectedDimension))
                if (DimensionItems.Count > 0)
                    SelectedDimension = DimensionItems[0];
        }
        //----------------------------------------------------------------------------------------------------------------------
        public void UpdateStepItems()
        {
            var listStep = AlgorithmWithDimensions.Where(a => a.NumberOfSet == selectedNumberOfSet
&& a.Dimension == selectedDimension).GroupBy(a => a.Step).OrderBy(n => n.Key).Select(n => n.Key);
            StepItems = listStep.ToObservable<long>();
            if (!StepItems.Contains(SelectedStep))
                if (StepItems.Count > 0)
                    SelectedStep = StepItems[0];

        }
        //----------------------------------------------------------------------------------------------------------------------
        public void UpdateAlgorithmItems()
        {
            var listAlgorithm = AlgorithmWithDimensions.Where(a => a.NumberOfSet == SelectedNumberOfSet
         && a.Dimension == SelectedDimension && a.Step == SelectedStep).GroupBy(a => a.Algorithm).OrderBy(n => n.Key).Select(n => n.Key);
            AlgorithmItems = listAlgorithm.ToObservable();
            if (!AlgorithmItems.Contains(SelectedAlgorithm1))
                if (AlgorithmItems.Count > 0)
                    SelectedAlgorithm1 = AlgorithmItems[0];
            if (!AlgorithmItems.Contains(SelectedAlgorithm2))
                if (AlgorithmItems.Count > 0)
                    SelectedAlgorithm2 = AlgorithmItems[0];
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand refreshRepresentativeCompareListCommand;
        public ICommand RefreshRepresentativeCompareListCommand
        {
            get
            {
                if (refreshRepresentativeCompareListCommand == null)
                {
                    refreshRepresentativeCompareListCommand = new DelegateCommand(RefreshRepresentativeCompareListAction, CanRefreshRepresentativeCompareListAction);
                }
                return refreshRepresentativeCompareListCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void RefreshRepresentativeCompareListAction()
        {
            RepresentativesPerfomanceCompareFilter representativesPerfomanceCompareFilter = new RepresentativesPerfomanceCompareFilter()
            {
                Algorithm1 = SelectedAlgorithm1,
                Algorithm2 = SelectedAlgorithm2,
                NumberOfSet = SelectedNumberOfSet,
                Dimension = SelectedDimension,
                Step = SelectedStep,
                BestValueCompare = SelectedBestValueCompare,
                DurationCompare = SelectedDurationCompare,
                NumberIterationCompare = SelectedcbNumberIterationCompare,
                ElemenationCountCompare = SelectedElemenationCountCompare
            };
            List<RepresentativesPerfomanceCompare> items = representativesRepository.GetRepresentativePerformanceCompareList(representativesPerfomanceCompareFilter);
            RepresentativesPerfomanceCompareList = items.ToObservable();
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanRefreshRepresentativeCompareListAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
