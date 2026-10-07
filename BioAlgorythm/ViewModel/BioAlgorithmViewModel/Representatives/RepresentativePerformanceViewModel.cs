using BioAlgorithmViewModel.Common;
using BioAlgorithmViewModel.Mappings;
using BioAlgorithmViewModel.Representatives.Messages;
using BioAlgorithmViewModel.Representatives.Utility;
using BioAlgorithmModel.RepresentativesModel;
using Representatives.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BioAlgorithmViewModel.Representatives
{
    public class RepresentativePerformanceViewModel : ViewModelBase
    {
        private RepresentativesRepository representativesRepository;
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<RepresentativesPerfomance> representativePerformanceList;
        public ObservableCollection<RepresentativesPerfomance> RepresentativePerformanceList
        {
            get
            {
                return representativePerformanceList;
            }
            set
            {
                representativePerformanceList = value;
                OnPropertyChanged(nameof(RepresentativePerformanceList));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativesPerfomance selectrdRepresentativeItem;
        public RepresentativesPerfomance SelectrdRepresentativeItem
        {
            get
            {
                return selectrdRepresentativeItem;
            }
            set
            {
                selectrdRepresentativeItem = value;
                OnPropertyChanged(nameof(SelectrdRepresentativeItem));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedRepresentativePerformanceSort;
        public string SelectedRepresentativePerformanceSort
        {
            get
            {
                return selectedRepresentativePerformanceSort;
            }
            set
            {
                selectedRepresentativePerformanceSort = value;
                OnPropertyChanged(nameof(SelectedRepresentativePerformanceSort));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------
        private List<string> representativePerformanceSortItems;
        public List<string> RepresentativePerformanceSortItems
        {
            get
            {
                return representativePerformanceSortItems;
            }
            set
            {
                representativePerformanceSortItems = value;
                OnPropertyChanged(nameof(RepresentativePerformanceSortItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativesPerfomanceFilterViewModel representativesPerfomanceFilter;
        public RepresentativesPerfomanceFilterViewModel RepresentativesPerfomanceFilter
        {
            get
            {
                return representativesPerfomanceFilter;
            }
            set
            {
                representativesPerfomanceFilter = value;
                OnPropertyChanged(nameof(RepresentativesPerfomanceFilter));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public RepresentativePerformanceViewModel(RepresentativesRepository representativesRepository) {
            RepresentativePerformanceList = new ObservableCollection<RepresentativesPerfomance>();
            RepresentativePerformanceSortItems = new List<string>()
            {
                "Algorithm, Dimension, NumberOfSet, Step, InputDataShort",
                "Algorithm, Dimension, NumberOfSet, Step, RepresentativesPerfomanceId",
                "Algorithm, NumberOfSet, NumberOfSet, Step, InputDataShort",
                "Algorithm, NumberOfSet, NumberOfSet, Step, RepresentativesPerfomanceId",
                "Dimension, NumberOfSet, Algorithm, Step, InputDataShort",
                "Dimension, NumberOfSet, Algorithm, Step, RepresentativesPerfomanceId",
                "NumberOfSet, Dimension, Algorithm, Step, InputDataShort",
                "NumberOfSet, Dimension, Algorithm, Step, RepresentativesPerfomanceId"
            };
            SelectedRepresentativePerformanceSort = RepresentativePerformanceSortItems[0];
            this.representativesRepository = representativesRepository;
            RepresentativesPerfomanceFilter = new RepresentativesPerfomanceFilterViewModel();
            Messenger.Default.Register<AlgorithmToFilterMessage>(this, OnAlgorithmToFilterMessageReceived, typeof(AlgorithmToFilterMessage));
            Messenger.Default.Register<AlgorithmGroupToFilterMessage>(this, OnAlgorithmGroupToFilterMessageReceived, typeof(AlgorithmGroupToFilterMessage));
            representativesPerfomanceFilter.Top = 1000;
        }
        //----------------------------------------------------------------------------------------------------------------------
        public RepresentativePerformanceViewModel(RepresentativesRepository representativesRepository, AlgorithmGroupOpenWindowMessage message)
            : this(representativesRepository)
        {
            RepresentativesPerfomanceFilter = new RepresentativesPerfomanceFilterViewModel();
            RepresentativesPerfomanceFilter.Algorithm = message.Algorithm;
            RepresentativesPerfomanceFilter.Dimension = message.Dimension;
            RepresentativesPerfomanceFilter.NumberOfSet = message.NumberOfSet;
            RepresentativesPerfomanceFilter.Step = message.Step;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand refreshRepresentativePerformanceListCommand;
        public ICommand RefreshRepresentativePerformanceListCommand
        {
            get
            {
                if (refreshRepresentativePerformanceListCommand == null)
                {
                    refreshRepresentativePerformanceListCommand = new DelegateCommand(RefreshRepresentativePerformanceListAction, CanRefreshRepresentativePerformanceListAction);
                }
                return refreshRepresentativePerformanceListCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void RefreshRepresentativePerformanceListAction()
        {
            RepresentativePerformanceList.Clear();
            RepresentativesPerfomanceFilter representativesPerfomanceFilterDto = RepresentativesPerfomanceFilter.Map();
            List<RepresentativesPerfomance> items = representativesRepository.GetRepresentativePerformanceList(representativesPerfomanceFilterDto, SelectedRepresentativePerformanceSort);
            foreach (RepresentativesPerfomance item in items)
                RepresentativePerformanceList.Add(item);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanRefreshRepresentativePerformanceListAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand makeGraphCommand;
        public ICommand MakeGraphCommand
        {
            get
            {
                if (makeGraphCommand == null)
                {
                    makeGraphCommand = new DelegateCommand(MakeGraphAction, CanMakeGraphAction);
                }
                return makeGraphCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void MakeGraphAction()
        {
            Messenger.Default.Send<RepresentativeTabChangeMessage>(new RepresentativeTabChangeMessage()
            {
                RepresentativeTabName = "BipartiteGraph"
            }, typeof(RepresentativeTabChangeMessage));
            Messenger.Default.Send<InputDataToGraphMessage>(new InputDataToGraphMessage()
            {
                InputData = SelectrdRepresentativeItem.InputData
            }, typeof(InputDataToGraphMessage));
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanMakeGraphAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void OnAlgorithmToFilterMessageReceived(AlgorithmToFilterMessage algorithmToFilterMessage)
        {
            RepresentativesPerfomanceFilter.Algorithm = algorithmToFilterMessage.Algorithm;
            RepresentativesPerfomanceFilter.Dimension = null;
            RepresentativesPerfomanceFilter.NumberOfSet = null;
            RepresentativesPerfomanceFilter.Step = null;
            RefreshRepresentativePerformanceListAction();
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void OnAlgorithmGroupToFilterMessageReceived(AlgorithmGroupToFilterMessage algorithmGroupToFilterMessage)
        {
            RepresentativesPerfomanceFilter.Algorithm = algorithmGroupToFilterMessage.Algorithm;
            RepresentativesPerfomanceFilter.Dimension = algorithmGroupToFilterMessage.Dimension;
            RepresentativesPerfomanceFilter.NumberOfSet = algorithmGroupToFilterMessage.NumberOfSet;
            RepresentativesPerfomanceFilter.Step = algorithmGroupToFilterMessage.Step;
            RefreshRepresentativePerformanceListAction();
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
