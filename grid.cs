using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstrastest
{
    internal class grid
    {
        node[,] arr;
        public int width { get;}
        public int height { get; }
        public List<int[]> coordList = new List<int[]>();
        Random rand = new Random(DateTime.Now.Millisecond);

        public grid(int width, int height, int[] startCoords, int[] endCoords)
        {
            this.width = width;
            this.height = height;

            //creates an array of nodes of the height and width specified
            arr = new node[height,width];

            //for every row of nodes
            for (int i = 0; i < height; i++)
            {
                //for every node in the row
                for (int j = 0; j < width; j++)
                {
                    //get a random value that will be used to determine if it's a wall
                    int value = rand.Next(0, 20);
                    int[] currentCoords = { i, j };

                    //if its the start or end then keep track of that
                    if(i == startCoords[0] &&  j == startCoords[1])
                    {
                        arr[i,j] = new node(true, false, startCoords, value, this);
                    }
                    else if(i == endCoords[0] && j == endCoords[1])
                    {
                        arr[i, j] = new node(false, true, endCoords, value, this);
                    }
                    //else let it be
                    else arr[i, j] = new node(false, false, currentCoords, value, this);
                }
            }
        }
        //returns the node given its coords
        public node nodeGet(int[] coords)
        {
            return arr[coords[0], coords[1]];
        }


        // This function is just for printing the map pretty
        public void getMap()
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j].GetNodeType() == nodeType.Start || arr[i, j].GetNodeType() == nodeType.End)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(arr[i, j].ToString().PadLeft(3));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (arr[i,j].inPath)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(arr[i, j].ToString().PadLeft(3));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (arr[i,j].GetNodeType() == nodeType.Wall)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(arr[i, j].ToString().PadLeft(3));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(arr[i, j].ToString().PadLeft(3));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            foreach (int[] coords in coordList)
            {
                foreach (int coord in coords) Console.Write(coord + " ");
                Console.Write("| ");
            }
        }
    }
}
