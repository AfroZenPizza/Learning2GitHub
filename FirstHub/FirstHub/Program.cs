//Auto-generated Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//User Added Namespaces
using System.Diagnostics;
using System.Threading;

namespace FirstHub
{
    class Program
    {

        /// <summary>
        /// Create a non-static start point for the program
        /// </summary>
        private void Run()
        {
            string line; // Will be used to handle input from the Console
            bool looping = true; //Used to keep the program looping while waiting for user to request exit
            
            //Let the user know what commands are availiable
            WriteCommandList();
            
            //Create Thread that runs the Cpu Monitoring Thread (function)
            Thread cpuMonitor = new Thread(new ThreadStart(CpuMonitorThread));
            
            //Start The Thread -- Note, this is not set to background, and will cause the program to run
            //as long as it remains alive
            cpuMonitor.Start();
            
            //Enter an infinate loop
            while (looping)
            { 
                //Read a line from the console, and store it as a string
                line = Console.ReadLine();
                //Check the user input for a command that is known
                switch (line.ToLower())
                {
                    case "!quit":
                        looping = false; // Changes the looping status so the looping ends
                        break;
                    case "!clear":
                        Console.Clear();
                        break;
                    case "!help":
                        WriteCommandList();
                        break;
                }
            }
            cpuMonitor.Abort(); //Ends the Cpu Monitor Thread so that the program can exit
        }

        /// <summary>
        /// Monitorign Fucntion to be runs a thread.
        /// Should be invoked as a thread start
        /// !DOES NOT SELF TERMINATE!
        /// </summary>
        private void CpuMonitorThread()
        {
            Console.WriteLine("[Info] Cpu Monitoring Started");
            //Create a performance monitor to get out CPU utilization
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            //Create an instance of Threshold Manager, that alerts on 90%
            ThresholdManager cpuThreshold = new ThresholdManager(90);
            //Register an event to fire when CPU is over 90% utilized
            cpuThreshold.onOverThresholdToggle += new EventHandler(OnCpuOverThresholdToggleEvent);
            //Register an event to fire when CPU returns to under 90%
            cpuThreshold.onUnderThresholdToggle += new EventHandler(OnCpuUnderThresholdToggleEvent);
            while (true) // Create an infinate loop
            {
                //Set current Threshold Comparator to the Performance Monitor's Next Value typecasted as an int from Double
                cpuThreshold.current = (int)cpuCounter.NextValue();
                //Sleep this thread to make sure we don't use an unfair amount of the CPU
                Thread.Sleep(32);
            }                
        }

        /// <summary>
        /// Event Handler for over CPU Threshold Toggle Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCpuOverThresholdToggleEvent(object sender, EventArgs e)
        {
            //Warn User
            Console.WriteLine("[Warning] CPU Utilization over threshold!");
        }

        /// <summary>
        /// Event Handler for under CPU Threshold Toggle Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCpuUnderThresholdToggleEvent(object sender, EventArgs e)
        {
            //Warn User
            Console.WriteLine("[Info] CPU Utilization returned to accpetable value");
        }


        //Simple Function to inform the user of availiable commands
        private void WriteCommandList()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("  !quit  : Ends the program");
            Console.WriteLine("  !clear : Clears the console");
            Console.WriteLine("  !help : Gives command list");
        }

        /// <summary>
        /// Program Entry point
        /// </summary>
        /// <param name="args">Command Line Arguements</param>
        static void Main(string[] args)
        {
            //Write a line to the console
            Console.WriteLine("Hello GitHub");
            Console.WriteLine("This is a simple first application:");
            Console.WriteLine("It's also a simple introduction to Events");
            //Create an instance of the program to run
            Program program = new Program();
            //Run the program
            program.Run();
        }
    
    }
}
