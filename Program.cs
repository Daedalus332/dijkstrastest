using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstrastest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // While loop allows you to rerun without having to restart program.
            string test = "sjdf";
            while (test != "y")
            {
                //random start and end places
                Random rnd = new Random(DateTime.Now.Millisecond);

                int[] startCoords = { rnd.Next(0, 14), rnd.Next(0, 14) };
                int[] endCoords = { rnd.Next(0, 14), rnd.Next(0, 14) };

                //creates 15 by 15 grid
                grid grid = new grid(15, 15, startCoords, endCoords);

                //starts the time designations for each tile
                node startNode = grid.nodeGet(startCoords);
                startNode.checkers(startNode.time);

                //backtracks from end tile to start
                node endNode = grid.nodeGet(endCoords);
                endNode.backtrack();

                //prints map
                grid.getMap();

                test = Console.ReadLine();
            }

        }
    }
}
