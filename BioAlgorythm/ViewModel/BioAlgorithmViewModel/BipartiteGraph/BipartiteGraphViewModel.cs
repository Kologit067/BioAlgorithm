using BioAlgorithmViewModel.Common;
using BioAlgorithmModel.BipartiteGraphModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BioAlgorithmViewModel.Representatives.Utility;
using BioAlgorithmViewModel.Representatives.Messages;
using BioAlgorithmModel.RepresentativesModel;

namespace BioAlgorithmViewModel.BipartiteGraphModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class BipartiteGraphViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraphViewModel : ViewModelBase
    {
        private List<BipartiteGraphVertexViewModel> leftSet;
        private List<BipartiteGraphVertexViewModel> rightSet;
        private List<BipartiteGraphEdgeViewModel> edges;
        private BipartiteGraph bipartiteGraph;
        //----------------------------------------------------------------------------------------------------------------------
        private ObservableCollection<BipartiteGraphElementViewModel> bipartiteGraphElements;
        public ObservableCollection<BipartiteGraphElementViewModel> BipartiteGraphElements
        {
            get
            {
                return bipartiteGraphElements;
            }
            set
            {
                bipartiteGraphElements = value;
                OnPropertyChanged(nameof(BipartiteGraphElements));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double leftPosition;
        public double LeftPosition
        {
            get
            {
                return leftPosition;
            }
            set
            {
                leftPosition = value;
                OnPropertyChanged(nameof(LeftPosition));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double rightPosition;
        public double RightPosition
        {
            get
            {
                return rightPosition;
            }
            set
            {
                rightPosition = value;
                OnPropertyChanged(nameof(RightPosition));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double vertexRadius;
        public double VertexRadius
        {
            get
            {
                return vertexRadius;
            }
            set
            {
                vertexRadius = value;
                OnPropertyChanged(nameof(VertexRadius));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double verticalPosition;
        public double VerticalPosition
        {
            get
            {
                return verticalPosition;
            }
            set
            {
                verticalPosition = value;
                OnPropertyChanged(nameof(VerticalPosition));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double verticalStep;
        public double VerticalStep
        {
            get
            {
                return verticalStep;
            }
            set
            {
                verticalStep = value;
                OnPropertyChanged(nameof(VerticalStep));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string graph;
        public string Graph
        {
            get
            {
                return graph;
            }
            set
            {
                graph = value;
                OnPropertyChanged(nameof(Graph));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public BipartiteGraphViewModel()
        {
            LeftPosition = 150;
            RightPosition= 400;
            VerticalPosition = 40;
            VerticalStep = 40;
            VertexRadius = 10;
            Messenger.Default.Register<InputDataToGraphMessage>(this, OnInputDataToGraphMessageReceived, typeof(InputDataToGraphMessage));
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand createGraphCommand;
        public ICommand CreateGraphCommand
        {
            get
            {
                if (createGraphCommand == null)
                {
                    createGraphCommand = new DelegateCommand(CreateGraphAction, CanCreateGraphAction);
                }
                return createGraphCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void CreateGraphAction()
        {
            bipartiteGraph = new BipartiteGraph(Graph);
            leftSet = new List<BipartiteGraphVertexViewModel>();
            for (int i = 0; i < bipartiteGraph.LeftSet.Count; i++)
            {
                leftSet.Add(new BipartiteGraphLeftVertexViewModel(bipartiteGraph.LeftSet[i], i, LeftPosition, VerticalPosition + i * VerticalStep, VertexRadius));
            }
            rightSet = new List<BipartiteGraphVertexViewModel>();
            for (int i = 0; i < bipartiteGraph.RightSet.Count; i++)
            {
                rightSet.Add(new BipartiteGraphRightVertexViewModel(bipartiteGraph.RightSet[i], i, RightPosition, VerticalPosition + i * VerticalStep, VertexRadius));
            }
            edges = new List<BipartiteGraphEdgeViewModel>();
            for (int i = 0; i < leftSet.Count; i++)
            {
                for (int j = 0; j < leftSet[i].AdjacentVertices.Count; j++)
                {
                    edges.Add(new BipartiteGraphEdgeViewModel(
                        leftPointX: leftSet[i].CenterX + VertexRadius,
                        leftPointY: leftSet[i].CenterY,
                        rightPointX: rightSet[leftSet[i].AdjacentVertices[j]].CenterX - VertexRadius,
                        rightPointY: rightSet[leftSet[i].AdjacentVertices[j]].CenterY
                        ));
                }
            }
            BipartiteGraphElements = new ObservableCollection<BipartiteGraphElementViewModel>();
            foreach (var element in rightSet.Union(leftSet))
                BipartiteGraphElements.Add(element);
            foreach (var element in edges)
                BipartiteGraphElements.Add(element);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanCreateGraphAction()
        {
            return true;
        }
        private void OnInputDataToGraphMessageReceived(InputDataToGraphMessage inputDataToGraphMessage)
        {
            Graph = inputDataToGraphMessage.InputData;
            CreateGraphAction();
        }      
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
