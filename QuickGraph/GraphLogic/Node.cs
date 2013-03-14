using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.GraphLogic
{
    public class Node
    {
        #region Private Member Variables
        // private member variables
        private string key;
        private object data;
        private AdjacencyList neighbors;
        public virtual Node PathParent { get; set; }
        #endregion

        #region Constructors
        private Node() { }		// remove default constructor

        public Node(string key, object data) : this(key, data, null) { }

        public Node(string key, object data, AdjacencyList neighbors)
        {
            this.key = key;
            this.data = data;
            if (neighbors == null)
                this.neighbors = new AdjacencyList();
            else
                this.neighbors = neighbors;
        }
        #endregion

        #region Public Methods
        #region Add Methods
        /// <summary>
        /// Adds an unweighted, directed edge from this node to the passed-in Node n.
        /// </summary>
        protected internal virtual void AddDirected(Node n)
        {
            AddDirected(new EdgeToNeighbor(n));
        }

        /// <summary>
        /// Adds a weighted, directed edge from this node to the passed-in Node n.
        /// </summary>
        /// <param name="cost">The weight of the edge.</param>
        protected internal virtual void AddDirected(Node n, int cost)
        {
            AddDirected(new EdgeToNeighbor(n, cost));
        }

        /// <summary>
        /// Adds an edge based on the data in the passed-in EdgeToNeighbor instance.
        /// </summary>
        protected internal virtual void AddDirected(EdgeToNeighbor e)
        {
            neighbors.Add(e);
        }
        #endregion
        #endregion

        #region Public Properties
        public List<string> NodeConnectionNames { get; set; }
        public double EnergyReading { get; set; }
        public int TransmissionRange { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        /// <summary>
        /// Returns the Node's Key.
        /// </summary>
        public virtual string Key
        {
            get
            {
                return key;
            }
        }

        /// <summary>
        /// Returns the Node's Data.
        /// </summary>
        public virtual object Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        /// <summary>
        /// Returns an AdjacencyList of the Node's neighbors.
        /// </summary>
        public virtual AdjacencyList Neighbors
        {
            get
            {
                return neighbors;
            }
        }
        #endregion
    }
}
