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
            //Create a performance monitor to get out CPU utilization
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");           
            //Create an instance of Threshold Manager, that alerts on 90%
            ThresholdManager cpuThreshold = new ThresholdManager(90);
            //Register an event to fire when CPU is over 90% utilized
            cpuThreshold.onOverThresholdToggle += new EventHandler(onCpuOverThresholdToggle);
            //Register an event to fire when CPU returns to under 90%
            cpuThreshold.onUnderThresholdToggle += new EventHandler(onCpuUnderThresholdToggle);
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
        private void onCpuOverThresholdToggle(object sender, EventArgs e)
        {
            //Warn User
            Console.WriteLine("Warning: CPU Utilization over threshold!");
        }

        /// <summary>
        /// Event Handler for under CPU Threshold Toggle Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onCpuUnderThresholdToggle(object sender, EventArgs e)
        {
            //Warn User
            Console.WriteLine("Info: CPU Utilization returned to accpetable value");
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
