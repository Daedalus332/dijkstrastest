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
            string test = "sjdf";
            while (test != "y")
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                int[] startCoords = { rnd.Next(0, 14), rnd.Next(0, 14) };
                int[] endCoords = { rnd.Next(0, 14), rnd.Next(0, 14) };

                grid grid = new grid(15, 15, startCoords, endCoords);

                node startNode = grid.nodeGet(startCoords);
                startNode.checkers(startNode.time);

                node endNode = grid.nodeGet(endCoords);
                endNode.backtrack();

                grid.getMap();

                test = Console.ReadLine();
            }

        }
    }
}
