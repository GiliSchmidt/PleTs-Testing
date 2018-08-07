using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using Coc.Modeling.FiniteStateMachine;
//using Coc.Modeling.FiniteStateMachine.Hsi;

namespace HSI
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    if (args.Count() == 0)
        //    {
        //        Console.WriteLine("Usage: \n\thsi.exe <filename>");
        //    }
        //    else
        //    {
        //        if (!File.Exists(args[0]))
        //        {
        //            Console.WriteLine("File not found.\n\n");
        //            Console.WriteLine("Usage: \n\thsi.exe <filename>");
        //        }
        //        else
        //        {
        //            try
        //            {
        //                FiniteStateMachine fsm = Program.ImportFromFile(args[0]);
        //                *
        //                HsiMethod method = new HsiMethod(fsm);

        //                String[][] testCases = method.GenerateTestCases();

        //                for (int i = 0; i < testCases.Length; i++)
        //                {
        //                    String[] kk = testCases[i];
        //                    for (int j = 0; j < kk.Length; j++)
        //                    {
        //                        Console.WriteLine(kk[j]);
        //                    }
        //                    Console.WriteLine("==========");
        //                }
        //                */
        //                Console.WriteLine();
        //                Console.WriteLine("FSM:");
        //                Console.WriteLine(fsm.ToString());
        //                Console.WriteLine("Minimizing FSM");
        //                fsm.MinimizeMe();
        //                Console.WriteLine("Minimized FSM:");
        //                Console.WriteLine(fsm.ToString());

        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine("Illegible file.\n" + e.Message);
        //            }
        //        }
        //    }
        //    Console.ReadKey();
        //}

        //private static FiniteStateMachine ImportFromFile(String filename)
        //{

        //    FiniteStateMachine fsm = new FiniteStateMachine();

        //    String[] lines = File.ReadAllLines(filename);

        //    foreach (String l in lines)
        //    {
        //        String[] data = l.Split(' ');

        //        fsm.AddTransition(data[0], data[3], data[1], data[2], true);

        //        if (fsm.InitialState == null)
        //            fsm.InitialState = fsm.GetStateByName(data[0]);
        //    }

        //    return fsm;

        //}
    }
}
