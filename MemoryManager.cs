using System.Collections.Generic;
using System.Text;

/// AUTHOR: DAN FERGUSON

namespace MemoryManager
{
    /// <summary>
    /// MASTER SCHEDULE ORGANIZER.  USED TO DETERMINE WHICH ALGORITHM IS USED.
    /// </summary>
    public class MemoryManager
    {
        private Algorithm selectedAlgorithm;
        private string[] RAM;
        private int totalMemory;
        private int OSMemory;
        private int availableMemory;

        /// <summary>
        /// THIS CONSTRUCTOR DETERMINES WHICH SCHEDULING ALGORITHM IS IN USE AND CALCULATES THE APPROPRIATE GANTT CHART
        /// </summary>
        /// <param name="activeProcesses">LIST OF PROCESSES FROM GUI</param>
        /// <param name="algorithm">TYPE OF ALGORITHM USED</param>
        public MemoryManager(int totalMemory, int OSMemory)
        {
            this.totalMemory = totalMemory;
            this.OSMemory = OSMemory;
            this.availableMemory = totalMemory - OSMemory;
        }

        /// <summary>
        /// ADDS A PROCESS TO MEMORY ACCORDING TO SELECTED ALGORITHM
        /// </summary>
        /// <param name="algorithm">THE MEMORY ALLOCATION ALGORITHM</param>
        /// <param name="p">THE PROCESS TO ADD</param>
        /// <returns>STRING ARRAY REPRESENTING MEMORY</returns>
        public string[] AddProcess(string algorithm, Process p)
        {
            switch (algorithm)
            {
                case "First Fit":
                    {
                        selectedAlgorithm = new FirstFit(this.totalMemory, this.OSMemory);
                        break;
                    }
                case "Best Fit":
                    {
                        selectedAlgorithm = new BestFit(this.totalMemory, this.OSMemory);
                        break;
                    }
                case "Worst Fit":
                    {
                        selectedAlgorithm = new WorstFit(this.totalMemory, this.OSMemory);
                        break;
                    }
                default:
                    {
                        string[] defaultGantt = new string[3];
                        defaultGantt[0] = ("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");   //50 tildas
                        defaultGantt[1] = ("Unkown algorithm");
                        defaultGantt[2] = ("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");   //50 tildas
                        this.RAM = defaultGantt;
                        break;
                    }
            }

            return this.RAM = selectedAlgorithm.FitProcess(p).ToArray();
        }

        
        public string[] RemoveProcess(Process p)
        {
            //selectedAlgorithm = new Scheduler(this.totalMemory, this.OSMemory);
            return this.RAM = selectedAlgorithm.Remove(p).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] CompactMemory()
        {
            //selectedAlgorithm = new Scheduler(this.totalMemory, this.OSMemory);
            return this.RAM = selectedAlgorithm.Compact().ToArray();
        }

        public int _TotalMemory { get { return totalMemory; } set { totalMemory = value; } }
        public int _OSMemory { get { return OSMemory; } set { OSMemory = value; } }
    }
}
