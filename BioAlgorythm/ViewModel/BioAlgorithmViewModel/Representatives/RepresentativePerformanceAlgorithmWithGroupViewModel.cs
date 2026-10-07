using BioAlgorithmViewModel.Common;
using BioAlgorithmViewModel.Helpers;
using BioAlgorithmViewModel.Mappings;
using BioAlgorithmViewModel.Representatives.Dto;
using BioAlgorithmViewModel.Representatives.Messages;
using BioAlgorithmViewModel.Representatives.Utility;
using BioAlgorithmModel.RepresentativesModel;
using Representatives.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace BioAlgorithmViewModel.Representatives
{
    //----------------------------------------------------------------------------------------------------------------------
    // class RepresentativePerformanceAlgorithmWithGroupViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class RepresentativePerformanceAlgorithmWithGroupViewModel : ViewModelBase
    {
        private RepresentativesRepository representativesRepository;
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<RepresentativesPerfomanceAsGroup> representativePerformanceAsGroupList;
        public ObservableCollection<RepresentativesPerfomanceAsGroup> RepresentativePerformanceAsGroupList
        {
            get
            {
                return representativePerformanceAsGroupList;
            }
            set
            {
                representativePerformanceAsGroupList = value;
                OnPropertyChanged(nameof(RepresentativePerformanceAsGroupList));
            }
        }
        private RepresentativesPerfomanceAsGroup selectedRepresentativePerformanceAsGroup;
        public RepresentativesPerfomanceAsGroup SelectedRepresentativePerformanceAsGroup
        {
            get
            {
                return selectedRepresentativePerformanceAsGroup;
            }
            set
            {
                selectedRepresentativePerformanceAsGroup = value;
                OnPropertyChanged(nameof(SelectedRepresentativePerformanceAsGroup));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<string> groupColumnItems;
        public ObservableCollection<string> GroupColumnItems
        {
            get
            {
                return groupColumnItems;
            }
            set
            {
                groupColumnItems = value;
                OnPropertyChanged(nameof(GroupColumnItems));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string selectedGroupColumn;
        public string SelectedGroupColumn
        {
            get
            {
                return selectedGroupColumn;
            }
            set
            {
                selectedGroupColumn = value;
                OnPropertyChanged(nameof(SelectedGroupColumn));
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
        public RepresentativePerformanceAlgorithmWithGroupViewModel(RepresentativesRepository representativesRepository)
        {
            GroupColumnItems = new ObservableCollection<string>()
            {
                "InputLenSort",
                "InputDataShort",
                "OptimalRoute",
                "BestValue"
            };
            RepresentativePerformanceSortItems = new List<string>()
            {
                "InputLenSort, OptimalRoute",
                "InputLenSort, BestValue",
                "OptimalRoute, InputLenSort",
                "OptimalRoute, BestValue",
                "BestValue, OptimalRoute",
                "BestValue, InputLenSort",
            };
            PropertyChanged += RepresentativePerformanceAlgorithmWithGroupViewModel_PropertyChanged;
            SelectedGroupColumn = GroupColumnItems.FirstOrDefault();
            this.representativesRepository = representativesRepository;
            SelectedRepresentativePerformanceSort = RepresentativePerformanceSortItems[0];
            RepresentativesPerfomanceFilter = new RepresentativesPerfomanceFilterViewModel();

            Messenger.Default.Register<AlgorithmToFilterByGroupMessage>(this, OnAlgorithmToFilterMessageReceived, typeof(AlgorithmToFilterByGroupMessage));
            Messenger.Default.Register<AlgorithmGroupToFilterByGroupMessage>(this, OnAlgorithmGroupToFilterMessageReceived, typeof(AlgorithmGroupToFilterByGroupMessage));

            representativesPerfomanceFilter.Top = 1000;
            RepresentativePerformanceAsGroupList = new ObservableCollection<RepresentativesPerfomanceAsGroup>();
        }

        private void OnAlgorithmGroupToFilterMessageReceived(AlgorithmGroupToFilterByGroupMessage algorithmGroupToFilterMessage)
        {
            RepresentativesPerfomanceFilter.Algorithm = algorithmGroupToFilterMessage.Algorithm;
            RepresentativesPerfomanceFilter.Dimension = algorithmGroupToFilterMessage.Dimension;
            RepresentativesPerfomanceFilter.NumberOfSet = algorithmGroupToFilterMessage.NumberOfSet;
            RepresentativesPerfomanceFilter.Step = algorithmGroupToFilterMessage.Step;
            //RefreshRepresentativePerformanceListAction();
        }

        private void OnAlgorithmToFilterMessageReceived(AlgorithmToFilterByGroupMessage algorithmToFilterMessage)
        {
            RepresentativesPerfomanceFilter.Algorithm = algorithmToFilterMessage.Algorithm;
            RepresentativesPerfomanceFilter.Dimension = null;
            RepresentativesPerfomanceFilter.NumberOfSet = null;
            RepresentativesPerfomanceFilter.Step = null;
            //RefreshRepresentativePerformanceListAction();
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
            RepresentativePerformanceAsGroupList.Clear();
            RepresentativesPerfomanceFilter representativesPerfomanceFilterDto = RepresentativesPerfomanceFilter.Map();
            string order = SelectedRepresentativePerformanceSort.Replace(SelectedGroupColumn, string.Empty);
            order = $"{SelectedGroupColumn},{order}".Replace(",,", ",").TrimEnd(new char[] { ',', ' ' });
            List<RepresentativesPerfomance> rp = representativesRepository.GetRepresentativePerformanceList(
                representativesPerfomanceFilterDto, order);
            List<RepresentativesPerfomanceDto> items = rp.Map().ToList();
            RepresentativePerformanceAsGroupList = items.GroupBy(GetGroupKey(SelectedGroupColumn).Compile()).Select(g => 
            new RepresentativesPerfomanceAsGroup() { 
                ColumnGroupName = SelectedGroupColumn, 
                ColumnGroupValue = g.Key, 
                RepresentativesPerfomanceList = g.ToList() }).ToObservable();
            
        }
        //----------------------------------------------------------------------------------------------------------------------
        private static Expression<Func<RepresentativesPerfomanceDto, string>> GetGroupKey(string property)
        {
            var parameter = Expression.Parameter(typeof(RepresentativesPerfomanceDto));
            var body = Expression.Property(parameter, property);
            return Expression.Lambda<Func<RepresentativesPerfomanceDto, string>>(body, parameter);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanRefreshRepresentativePerformanceListAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void RepresentativePerformanceAlgorithmWithGroupViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
