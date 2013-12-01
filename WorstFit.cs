using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryManager
{
    internal class WorstFit : Scheduler, Algorithm
    {        
        /// <summary>
        /// SEE BASE IMPLEMENTATION
        /// </summary>
        /// <param name="totalMemory">TOTAL MEMORY</param>
        /// <param name="OSMemory">OPERATING SYSTEM MEMORY</param>
        public WorstFit(int totalMemory, int OSMemory)
            : base(totalMemory, OSMemory)
        {

        }

        /// <summary>
        /// FITS PROCESSES INTO MEMORY ACCORDING TO THE WORSTFIT SCHEME
        /// </summary>
        /// <param name="processToAdd">THIS IS THE PROCESS BEING ADDED</param>
        /// <returns>RETURNS A LIST REPRESENTING MEMORY</returns>
        public override List<string> FitProcess(Process processToAdd)
        {
            int gapSize = 0;
            int gapStart = 0;
            int largestGap = 0;
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
                    gapSize++;
                }
                if ((temp[i].Substring(0, 1).Equals("1")) || (i >= temp.Length - 1))
                {
                    if (gapSize > largestGap)
                    {
                        largestGap = gapSize;
                        gapStart = i - gapSize;
                    }
                    else
                    {
                        gapSize = 0;
                    }
                }
            }
            memorySpots = temp.ToList<string>();
            if (waitingQueue.Count == 0)
            {
                if (!allocateMemory(processToAdd, largestGap, gapStart))
                {
                    MessageBox.Show("Process cannot fit in memory.  Added to wait queue.");
                }
                return base.FitProcess(processToAdd);
            }
            else
            {
                foreach (Process p in waitingQueue)
                {
                    if (!allocateMemory(p, largestGap, gapStart))
                    {
                        MessageBox.Show("Waiting Queue could not be purged into memory.  New process added to Wait Queue.");
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                return base.FitProcess(processToAdd);
            }
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
