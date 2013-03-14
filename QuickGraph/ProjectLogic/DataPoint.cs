using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph
{
    public class DataPoint
    {
        //improvements this could be a singleton. 
        public DataPoint()
        {
            
            
        }

        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int TransmissionRange{get; set; }
        public double EnergyConsumption { get; set; }
        public string UniqueIdentifier { get; set; }
        public List<Guid> ConnectedDataPoints { get; set; }

         
    }
}
