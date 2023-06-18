using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] currentState = new int[8];

            currentState = RandomState();

            int restarts = 0;
            int changes = 0;

            while (HeuristicTest(currentState) != 0)
            {

                int hBetter;
                int[] bestNeighborResult;


                NeighborCheck(currentState, out hBetter, out bestNeighborResult);

                if (hBetter > 0)
                {
                    Console.WriteLine("Current h: "+HeuristicTest(currentState));
                    Console.WriteLine("Current State");
                    WriteState(currentState);
                    Console.WriteLine("Neighbors found with lower h: "+ hBetter);
                    Console.WriteLine("Setting new current state" + "\r\n");
                    changes++;

                    currentState = bestNeighborResult;
                }

                else
                {
                    Console.WriteLine("Current h: "+HeuristicTest(currentState));
                    Console.WriteLine("Current State");
                    WriteState(currentState);
                    Console.WriteLine("Neighbors found with lower h: "+hBetter);
                    Console.WriteLine("RESTART" + "\r\n");
                    restarts++;

                    currentState = RandomState();
                }
            }

            Console.WriteLine("Current State");
            WriteState(currentState);
            Console.WriteLine("Solution found!");
            Console.WriteLine("State changes: "+changes);
            Console.WriteLine("Restarts: "+restarts);
            Console.ReadKey();
        }

        static void NeighborCheck(int[] testState, out int hCount, out int[] bestNeighborState)
        {

            int[] neighborState = new int[8];
            int i;
            int t;
            int hNeighbor;
            int hNeighborBest = HeuristicTest(testState);
            int hCurrent = HeuristicTest(testState);
            int columnTemp = 0;
            int betterHCount = 0;
            bestNeighborState = new int[8];

            Array.Copy(testState, neighborState, 8);



            for (i = 0; i < 8; i++)
            {

                for (t = 0; t < 8; t++)
                {

                    columnTemp = neighborState[i];
                    neighborState[i] = t;

                    hNeighbor = HeuristicTest(neighborState);

                    if (hNeighbor < hNeighborBest)
                    {

                        Array.Copy(neighborState, bestNeighborState, 8);
                        hNeighborBest = hNeighbor;
                        betterHCount++;
                    }

                    neighborState[i] = columnTemp;

                }

            }

            hCount = betterHCount;

        }

        static int HeuristicTest(int[] testState)
        {
            int h = 0;

            //Horizontal Test

            int i;
            int c;

            for (i = 0; i < 8; i++)
            {
                for (c = 0; c < 8; c++)
                {
                    if (testState[i] == testState[c] && i != c)
                    {
                        h++;
                    }
                }

            }

            //Diagonal Test
            i = 0;
            int t = 0;


            for (i = 0; i < 8; i++)
            {

                for (t = 1; t < 8; t++)
                {

                    if (i + t < 8)
                    {
                        //down right
                        if ((testState[i] + t) == (testState[i + t]))
                        {
                            h++;
                        }


                        //up right
                        if ((testState[i] - t) == (testState[i + t]))
                        {
                            h++;
                        }

                    }


                    if (i - t >= 0)
                    {
                        //up left
                        if ((testState[i] - t) == (testState[i - t]))
                        {
                            h++;
                        }


                        //down left
                        if ((testState[i] + t) == (testState[i - t]))
                        {
                            h++;
                        }
                    }


                }


            }

            return h / 2;
        }

        static int[] RandomState()
        {

            Random random = new Random();

            int[] randomState = new int[8];

            for (int i = 0; i < 8; i++)
            {
                randomState[i] = random.Next(8);

            }

            return randomState;
        }

        static void WriteState(int[] testState)
        {
            int m;
            int k;

            for (m = 0; m < 8; m++)
            {

                int[] writeArray = { 0, 0, 0, 0, 0, 0, 0, 0 };

                for (k = 0; k < 8; k++)
                {
                    if (testState[k] == m)
                    {
                        writeArray[k] = 1;
                    }

                }

                Console.WriteLine(String.Join(", ", writeArray));
            }

        }

    }
}
