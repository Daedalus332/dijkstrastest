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
        public int[] nodeGet(node node)
        {
            int[] value = { node.coords[0] + y, node.coords[1] + x };
            return value;
        }

    }
}
