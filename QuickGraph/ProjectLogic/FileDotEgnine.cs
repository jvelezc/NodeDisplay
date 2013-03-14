using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace ConsoleApplication1
{
    public class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, string dot, string outputFileName)
        {

            string output = outputFileName + ".dot";
            File.WriteAllText(output, dot);
            return output;
        }
    }
}
