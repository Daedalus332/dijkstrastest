using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstrastest
{
    internal class checker
    {
        public int y;
        public int x;
        public checker(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        //will add the specified x and y to the coordinates of the node provided and return the coordinates.
        //i could return a node here which would require moving the validation code here
        //which is possible but a bit of a headache
        public int[] nodeGet(node node)
        {
            int[] value = { node.coords[0] + y, node.coords[1] + x };
            return value;
        }

    }
}
