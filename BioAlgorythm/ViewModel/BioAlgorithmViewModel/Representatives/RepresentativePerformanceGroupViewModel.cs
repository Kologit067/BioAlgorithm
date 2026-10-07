using BioAlgorithmViewModel.Common;
using BioAlgorithmViewModel.Mappings;
using BioAlgorithmViewModel.Representatives.Messages;
using BioAlgorithmViewModel.Representatives.Utility;
using BioAlgorythmModel.RepresentativesModel;
using Representatives.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RepresentativeServices;

namespace BioAlgorithmViewModel.Representatives
{
    //----------------------------------------------------------------------------------------------------------------------
    // class RepresentativePerformanceGroupViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class RepresentativePerformanceGroupViewModel : ViewModelBase
    {
        private RepresentativesRepository representativesRepository;
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<RepresentativeAlgorithmGroupDimension> representativeAlgorithmGroupByDimensions;
        public ObservableCollection<RepresentativeAlgorithmGroupDimension> RepresentativeAlgorithmGroupByDimensions
        {
            get
            {
                return representativeAlgorithmGroupByDimensions;
            }
            set
            {
                representativeAlgorithmGroupByDimensions = value;
                OnPropertyChanged(nameof(RepresentativeAlgorithmGroupByDimensions));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativeAlgorithmGroupDimension selectedAlgorithmGroup;
        public RepresentativeAlgorithmGroupDimension SelectedAlgorithmGroup
        {
            get
            {
                return selectedAlgorithmGroup;
            }
            set
            {
                selectedAlgorithmGroup = value;
                OnPropertyChanged(nameof(SelectedAlgorithmGroup));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string algorithmGroupListSort;
        public string AlgorithmGroupListSort
        {
            get
            {
                return algorithmGroupListSort;
            }
            set
            {
                algorithmGroupListSort = value;
                OnPropertyChanged(nameof(AlgorithmGroupListSort));
            }
        }

        //----------------------------------------------------------------------------------------------------------------------
        private List<string> algorithmGroupSortItems;
        public List<string> AlgorithmGroupSortItems
        {
            get
            {
                return algorithmGroupSortItems;
            }
            set
            {
                algorithmGroupSortItems = value;
                OnPropertyChanged(nameof(AlgorithmGroupSortItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public RepresentativePerformanceGroupViewModel(RepresentativesRepository representativesRepository)
        {
            this.representativesRepository = representativesRepository;
            RepresentativeAlgorithmGroupByDimensions = new ObservableCollection<RepresentativeAlgorithmGroupDimension>();
            AlgorithmGroupSortItems = new List<string>()
            {
                "Algorithm, Dimension, NumberOfSet",
                "Algorithm, NumberOfSet, NumberOfSet",
                "Dimension, NumberOfSet, Algorithm",
                "NumberOfSet, Dimension, Algorithm"
            };
            AlgorithmGroupListSort = AlgorithmGroupSortItems[0];
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand refreshRepresentativeAlgorithmGroupListCommand;
        public ICommand RefreshRepresentativeAlgorithmGroupListCommand
        {
            get
            {
                if (refreshRepresentativeAlgorithmGroupListCommand == null)
                {
                    refreshRepresentativeAlgorithmGroupListCommand = new DelegateCommand(RefreshRepresentativeAlgorithmGroupListAction, CanRefreshRepresentativeAlgorithmGroupListAction);
                }
                return refreshRepresentativeAlgorithmGroupListCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private async void RefreshRepresentativeAlgorithmGroupListAction()
        {
            RepresentativeAlgorithmGroupByDimensions.Clear();
            List<RepresentativeAlgorithmGroupDimension> algorithms = await representativesRepository.GetRepresentativeAlgorithmGroupDimensions(AlgorithmGroupListSort);
            foreach (RepresentativeAlgorithmGroupDimension a in algorithms)
                RepresentativeAlgorithmGroupByDimensions.Add(a);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanRefreshRepresentativeAlgorithmGroupListAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand toFilterCommand;
        public ICommand ToFilterCommand
        {
            get
            {
                if (toFilterCommand == null)
                {
                    toFilterCommand = new DelegateCommand(ToFilterAction, CanToFilterAction);
                }
                return toFilterCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void ToFilterAction()
        {
            Messenger.Default.Send<RepresentativeTabChangeMessage>(new RepresentativeTabChangeMessage()
            {
                RepresentativeTabName = "RepresentativePerformance"
            }, typeof(RepresentativeTabChangeMessage));
            Messenger.Default.Send<AlgorithmGroupToFilterMessage>(new AlgorithmGroupToFilterMessage() {
                Algorithm = SelectedAlgorithmGroup.Algorithm,
                Dimension = SelectedAlgorithmGroup.Dimension,
                NumberOfSet = SelectedAlgorithmGroup.NumberOfSet,
                Step = SelectedAlgorithmGroup.Step
            }, typeof( AlgorithmGroupToFilterMessage ));
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanToFilterAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand toFilterInGroupCommand;
        public ICommand ToFilterInGroupCommand
        {
            get
            {
                if (toFilterInGroupCommand == null)
                {
                    toFilterInGroupCommand = new DelegateCommand(ToFilterInGroupAction, CanToFilterInGroupAction);
                }
                return toFilterInGroupCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void ToFilterInGroupAction()
        {
            Messenger.Default.Send<RepresentativeTabChangeMessage>(new RepresentativeTabChangeMessage()
            {
                RepresentativeTabName = "RepresentativePerformanceAsGroup"
            }, typeof(RepresentativeTabChangeMessage));
            Messenger.Default.Send<AlgorithmGroupToFilterByGroupMessage>(new AlgorithmGroupToFilterByGroupMessage()
            {
                Algorithm = SelectedAlgorithmGroup.Algorithm,
                Dimension = SelectedAlgorithmGroup.Dimension,
                NumberOfSet = SelectedAlgorithmGroup.NumberOfSet,
                Step = SelectedAlgorithmGroup.Step
            }, typeof(AlgorithmGroupToFilterByGroupMessage));
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanToFilterInGroupAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand openInWindowCommand;
        public ICommand OpenInWindowCommand
        {
            get
            {
                if (openInWindowCommand == null)
                {
                    openInWindowCommand = new DelegateCommand(OpenInWindowAction, CanOpenInWindowAction);
                }
                return openInWindowCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void OpenInWindowAction()
        {
            Messenger.Default.Send<AlgorithmGroupOpenWindowMessage>(new AlgorithmGroupOpenWindowMessage()
            {
                Algorithm = SelectedAlgorithmGroup.Algorithm,
                Dimension = SelectedAlgorithmGroup.Dimension,
                NumberOfSet = SelectedAlgorithmGroup.NumberOfSet,
                Step = SelectedAlgorithmGroup.Step
            }, typeof(AlgorithmGroupOpenWindowMessage));
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanOpenInWindowAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand deleteGroupCommand;
        public ICommand DeleteGroupCommand
        {
            get
            {
                if (deleteGroupCommand == null)
                {
                    deleteGroupCommand = new DelegateCommand(DeleteGroupAction, CanDeleteGroupAction);
                }
                return deleteGroupCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void DeleteGroupAction()
        {
            representativesRepository.DeleteRepresentativeAlgorithmGroup(SelectedAlgorithmGroup);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanDeleteGroupAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand testIsomorphismCommand;
        public ICommand TestIsomorphismCommand
        {
            get
            {
                if (testIsomorphismCommand == null)
                {
                    testIsomorphismCommand = new DelegateCommand(TestIsomorphismAction, CanTestIsomorphismAction);
                }
                return testIsomorphismCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void TestIsomorphismAction()
        {
            RepresentativeService representativeService = new RepresentativeService(representativesRepository);
            representativeService.TestIsomorphism(SelectedAlgorithmGroup.Algorithm,
                SelectedAlgorithmGroup.Dimension,
                SelectedAlgorithmGroup.NumberOfSet,
                SelectedAlgorithmGroup.Step);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanTestIsomorphismAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
