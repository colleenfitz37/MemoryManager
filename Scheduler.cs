using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// AUTHOR: DAN FERGUSON

namespace MemoryManager
{
    /// <summary>
    /// BASE CLASS FOR ALL CPU SCHEDULE ALGORITHMS
    /// </summary>
    public class Scheduler : Algorithm
    {
        private static int availableMemory;
        private int OSMemory;
        private int totalMemory;
        protected static List<string> memorySpots;
        protected static Dictionary<int, Process> addedProcesses = new Dictionary<int, Process>();
        protected static List<Process> waitingQueue = new List<Process>();

        /// <summary>
        /// THE CONSTRUCTOR FOR THE SCHEDULAR OBJECT
        /// </summary>
        /// <param name="totalMemory">THE TOTAL AVAILABLE MEMORY</param>
        /// <param name="OSMemory">THE MEMORY TAKEN UP BY THE OS</param>
        public Scheduler(int totalMemory, int OSMemory)
        {
            this.OSMemory = OSMemory;
            this.totalMemory = totalMemory;
            availableMemory = totalMemory - OSMemory;
            memorySpots = new List<string>();
            string[] temp = new string[availableMemory + 1];
            for(int i = 0; i < temp.Length; i++)
            {
                temp[i] = "0";
            }
            for (int i = 0; i < OSMemory; i++)
            {
                temp[i] = "1 OS MEMORY";
            }
            memorySpots = temp.ToList<string>();
        }

        /// <summary>
        /// WHERE THE ACTUAL ALGORITHM GOES. MUST BE OVERWRITTEN
        /// </summary>
        /// <param name="activeProcesses">LIST OF ALL PROCESSES THAT ARE ACTIVE</param>
        /// <returns>RETURNS THE LIST REPRESENTATION OF MEMORY</returns>
        public virtual List<string> FitProcess(Process processToAdd)
        {
            return memorySpots;
        }

        /// <summary>
        /// THE ALGORITHM FOR COMPACTING MEMORY
        /// </summary>
        /// <returns>THE LIST REPRESENTATION OF MEMORY</returns>
        public virtual List<string> Compact()
        {
            int offset = 0;

            for(int i = 0; i < memorySpots.Count; i++)
            {
                if (memorySpots[i].Equals("0"))
                {
                    try
                    {
                        while (i <= memorySpots.Count && memorySpots[i].Equals("0"))
                        {
                            memorySpots.Remove(memorySpots[i]);
                            offset++;
                        }
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            string[] temp = new string[offset];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = "0";
            }
            memorySpots.AddRange(temp);
            return memorySpots;
        }

        /// <summary>
        /// ADDS A PROCESS TO THE WAITING LIST
        /// </summary>
        /// <param name="processToAdd">THE PROCESS THAT MUST WAIT</param>
        /// <returns></returns>
        public virtual void Wait(Process processToAdd)
        {
            waitingQueue.Add(processToAdd);
        }

        /// <summary>
        /// REMOVES A PROCESS FROM MEMORY
        /// </summary>
        /// <param name="processToRemove">THE PROCESS THAT NEEDS TO BE REMOVED</param>
        /// <returns>THE NEW REPRESENTATION OF MEMORY</returns>
        public virtual List<string> Remove(Process processToRemove)
        {
            int startLocation = processToRemove.StartLocation;
            int size = processToRemove.size;
            string[] temp = memorySpots.ToArray<string>();

            for (int i = startLocation; i < startLocation + size; i++)
            {
                temp[i] = "0";
            }
            addedProcesses.Remove(processToRemove.StartLocation);
            memorySpots = temp.ToList<string>();
            return memorySpots;
        }

        /// <summary>
        /// ALLOCATES MEMORY FOR THE PROCESS TO BE ADDED
        /// </summary>
        /// <param name="processToAdd">THE PROCESS THAT IS BEING ADDED</param>
        /// <param name="largestGap">THE GAP OF MEMORY IN WHICH THE PROCESS WILL FIT</param>
        /// <param name="gapStart">THE FIRST MEMORY LOCATION THE PROCESS WILL FIT IN</param>
        /// <returns>RETURNS TRUE IF THE PROCESS IS ADDED TO MEMORY, FALSE IF ADDED TO WAIT QUEUE</returns>
        protected bool allocateMemory(Process processToAdd, int largestGap, int gapStart)
        {
            if (processToAdd.size <= largestGap)
            {
                processToAdd.StartLocation = gapStart;
                addedProcesses.Add(gapStart, processToAdd);
                for (int i = gapStart; i < (gapStart + processToAdd.size); i++)
                {
                    memorySpots[i] = "1 " + processToAdd.PID;
                }
            }
            else
            {
                Wait(processToAdd);
                return false;
            }
            return true;
        }

        /*
        /// <summary>
        /// SORTS PROCESSES BASED ON ARRIVAL
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortArrival(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            int ptr = 0;
            for (int i = 0; i < processArr.Length; i++)
            {
                Process earliest = processArr[i];
                ptr = i;
                for (int j = 0; j < processArr.Length; j++)
                {
                    if ((processArr[j].ArriveTime < earliest.ArriveTime) && (processArr[j].ArriveTime != 999))
                    {
                        earliest = processArr[j];
                        ptr = j;
                    }
                    else
                    {
                        ptr = i;
                        continue;
                    }
                }
                processArr[ptr] = dummy;
                temp.Add(earliest);
            }
            activeProcesses = temp;
        }

        /// <summary>
        /// SORTS PROCESSES BASED ON BURST TIME
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortSize(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            int ptr = 0;
            for (int i = 0; i < processArr.Length; i++)
            {
                Process shortest = processArr[i];
                ptr = i;
                for (int j = 0; j < processArr.Length; j++)
                {
                    if ((processArr[j].BurstTime < shortest.BurstTime) && (processArr[j].BurstTime != 999))
                    {
                        shortest = processArr[j];
                        ptr = j;
                    }
                    else continue;
                }
                processArr[ptr] = dummy;
                temp.Add(shortest);
            }
            activeProcesses = temp;
        }

        /// <summary>
        /// SORTS PROCESSES BASED ON PRIORITY
        /// LOW NUMBER -> HIGH PRIORITY
        /// </summary>
        /// <param name="activeProcesses">LIST OF ACTIVE PROCESSES</param>
        protected void sortPriority(ref List<Process> activeProcesses)
        {
            List<Process> temp = new List<Process>();
            Process[] processArr = activeProcesses.ToArray();
            Process dummy = new Process(999, 999, 999, 999);
            int ptr = 0;
            for (int i = 0; i < processArr.Length; i++)
            {
                Process mostImportant = processArr[i];
                ptr = i;
                for (int j = 0; j < processArr.Length; j++)
                {
                    if ((processArr[j].Priority < mostImportant.Priority) && (processArr[j].Priority != 999))
                    {
                        mostImportant = processArr[j];
                        ptr = j;
                    }
                    else continue;
                }
                processArr[ptr] = dummy;
                temp.Add(mostImportant);
            }

            processArr = temp.ToArray();
            for (ptr = 0; ptr < processArr.Length; ptr++)
            {
                processArr[ptr].Priority = ptr + 1;
            }
            activeProcesses = processArr.ToList<Process>();
        }
         */
    }
}
