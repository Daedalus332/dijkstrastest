using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace dijkstrastest
{
    internal class node
    {
        public nodeType type;
        public int[] coords;
        public int time = 1000;
        public grid parentGrid;
        public bool inPath = false;
        checker[] checkerList = { new checker(-1, 0), new checker(-1, 1), new checker(0, 1), new checker(1, 1), new checker(1, 0), new checker(1, -1), new checker(0, -1), new checker(-1, -1) };
        
        public node(bool start, bool end, int[] coords, int value, grid grid)
        {
            parentGrid = grid;
            this.coords = coords;
            if (start)
            {
                type = nodeType.Start;
                time = 0;
            }
            else if (end)
            {
                type = nodeType.End;
            }
            else if (value <= 4)
            {
                type = nodeType.Wall;
            }
            else
            {
                type = nodeType.Blank;
            }
            this.coords = coords;
        }
        public override string ToString()
        {
            if (type == nodeType.Start) return "S";
            else if (type == nodeType.End) return "E";
            else if (type == nodeType.Wall) return "X";
            else if (inPath) return "O";
            else return ".";
        }
        public nodeType GetNodeType() { return type; }

        public void checkers(int time)
        {
            this.time = time;
            foreach (checker checker in checkerList)
            {
                int[] gotCoords = checker.nodeGet(this);
                if (validGet(gotCoords, checker))
                {
                    if (parentGrid.nodeGet(gotCoords).time > time + 1)
                    {
                        parentGrid.nodeGet(gotCoords).checkers(time + 1);
                    }
                }
            }
        }
        public void backtrack()
        {
            if (this.time == 0)
            {
                parentGrid.coordList.Add(this.coords);
                inPath = true;
                return;
            }
            int checkerNum = 0;
            int lowest = 1000;
            int[] gotCoords;
            for (int i = 0; i < checkerList.Length; i++)
            {
                gotCoords = checkerList[i].nodeGet(this);
                if (validGet(gotCoords, checkerList[i]))
                {
                    if (parentGrid.nodeGet(gotCoords).time < lowest)
                    {
                        lowest = parentGrid.nodeGet(gotCoords).time;
                        checkerNum = i;
                    }
                }
            }
            parentGrid.nodeGet(checkerList[checkerNum].nodeGet(this)).backtrack();
            inPath = true;
            parentGrid.coordList.Add(this.coords);
        }

        bool validGet(int[] coords, checker checker)
        {
            if (coords[0] < 0 || coords[0] > parentGrid.height - 1)
            {
                return false;
            }
            else if (coords[1] < 0 || coords[1] > parentGrid.width - 1)
            {
                return false;
            }
            else if (parentGrid.nodeGet(coords).GetNodeType() == nodeType.Wall)
            {
                return false;
            }
            else if (checker.y != 0 && checker.x != 0) 
            {
                checker checkerY = new checker(checker.y, 0);
                checker checkerX = new checker(0, checker.x);
                int[] coordsY = checkerY.nodeGet(this);
                int[] coordsX = checkerX.nodeGet(this);
                if(parentGrid.nodeGet(coordsY).GetNodeType() == nodeType.Wall && parentGrid.nodeGet(coordsX).GetNodeType() == nodeType.Wall)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
