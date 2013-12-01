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
