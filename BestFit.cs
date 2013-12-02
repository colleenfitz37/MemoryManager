using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryManager
{
    internal class BestFit : Scheduler, Algorithm
    {
        /// <summary>
        /// SEE BASE IMPLEMENTATION
        /// </summary>
        /// <param name="totalMemory">TOTAL MEMORY</param>
        /// <param name="OSMemory">OPERATING SYSTEM MEMORY</param>
        public BestFit(int totalMemory, int OSMemory)
            : base(totalMemory, OSMemory)
        {
        
        }

        /// <summary>
        /// FITS PROCESSES INTO MEMORY ACCORDING TO THE BESTFIT SCHEME
        /// </summary>
        /// <param name="processToAdd">THIS IS THE PROCESS BEING ADDED</param>
        /// <returns>RETURNS A LIST REPRESENTING MEMORY</returns>
        public override List<string> FitProcess(Process processToAdd) 
        {
            
            int gapSize = 0;
            int gapStart = 0;
            int smallestGap = 0;
            string[] temp = memorySpots.ToArray<string>();
            foreach (KeyValuePair<int, Process> p in addedProcesses)
            {
                for (int i = 0; i < p.Value.size; i++)
                {
                    temp[p.Key + i] = "1 " + p.Value.PID;
                }
            }

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Substring(0, 1).Equals("0"))
                {
                    smallestGap = gapSize;
                }
                
                // I'm not sure what youre analyzing with this part... I am trying to mod you worst fit program
                
                if ((temp[i].Substring(0, 1).Equals("1")) || (i >= temp.Length - 1))
                {
                    if (gapSize > smallestGap)
                    {
                        smallestGap = gapSize;
                        gapStart = i - gapSize;
                    }
                    else
                    {
                        gapSize = 0;
                    }
                }
            return base.FitProcess(processToAdd);
        }

        /// <summary>
        /// SEE PARENT CLASS
        /// </summary>
        public override List<string> Compact()
        {
            return base.Compact();
        }

        /// <summary>
        /// SEE PARENT CLASS
        /// </summary>
        public override List<string> Remove(Process processToRemove)
        {
            return base.Remove(processToRemove);
        }

        /// <summary>
        /// SEE PARENT CLASS
        /// </summary>
        public override void Wait(Process p)
        {
            base.Wait(p);
        }
    }
}
