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
        //8 checkers, for cardinal directions.
        checker[] checkerList = { new checker(-1, 0), new checker(-1, 1), new checker(0, 1), new checker(1, 1), new checker(1, 0), new checker(1, -1), new checker(0, -1), new checker(-1, -1) };
        
        public node(bool start, bool end, int[] coords, int value, grid grid)
        {
            parentGrid = grid;
            this.coords = coords;
            //if it's the start, designate it as such and set the time to get here from the start to be 0
            //otherwise just give it the right type
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

        //override the tostring for nicer print display
        public override string ToString()
        {
            if (type == nodeType.Start) return "S";
            else if (type == nodeType.End) return "E";
            else if (type == nodeType.Wall) return "X";
            else if (inPath) return this.time.ToString();
            else return ".";
        }

        //Prolly shoulda just made the type be gettable and settable but to change it now would be tricky.
        public nodeType GetNodeType() { return type; }

        
        public void checkers(int time)
        {
            //sets the time to get here to what it is provided with
            this.time = time;
            //for every checker in the list
            foreach (checker checker in checkerList)
            {
                //get the coordinates that checker creates and check if they are valid
                int[] gotCoords = checker.nodeGet(this);
                if (validGet(gotCoords, checker))
                {
                    //if they are make sure the node doesn't already have a better time
                    if (parentGrid.nodeGet(gotCoords).time > time + 1)
                    {
                        //if no better time then recursively call the function on the next node
                        parentGrid.nodeGet(gotCoords).checkers(time + 1);
                    }
                }
            }
        }

        //very similar to checkers, except it works down in time instead of calculating times
        public void backtrack()
        {
            //if we have reached the end, terminate
            if (this.time == 0)
            {
                parentGrid.coordList.Add(this.coords);
                inPath = true;
                return;
            }
            //keeps track of which checker had the lowest score
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
            //node that is returned by the lowest score checker gets backtrack recursively called
            parentGrid.nodeGet(checkerList[checkerNum].nodeGet(this)).backtrack();
            inPath = true;

            //AFTER the recursion is "rebounding" then we add to the coordinate list
            //this means it's in the right order, pretty cool right?!
            parentGrid.coordList.Add(this.coords);
        }

        //this checks to see if a given set of coordinates are valid
        bool validGet(int[] coords, checker checker)
        {
            //if the y is valid (not too high or low)
            if (coords[0] < 0 || coords[0] > parentGrid.height - 1)
            {
                return false;
            }
            //if the x in valid (not too high or low)
            else if (coords[1] < 0 || coords[1] > parentGrid.width - 1)
            {
                return false;
            }
            //if the node itself is a wall
            else if (parentGrid.nodeGet(coords).GetNodeType() == nodeType.Wall)
            {
                return false;
            }
            //if it is a diagonal then if the direction we are trying to go is actually blocked by walls
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
            //if all that passes, then we are good to go
            return true;
        }
    }
}
