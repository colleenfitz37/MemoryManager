using System.Collections.Generic;

namespace MemoryManager
{
    /// <summary>
    /// THIS INTERFACE IS DESIGNED TO OUTLINE THE STRUCTURE OF ANY ALGORITHM CLASS
    /// </summary>
    public interface Algorithm
    {
        /// <summary>
        /// SEE IMPLEMENTATION
        /// </summary>
        /// <param name="activeProcesses">LIST OF PROCESSES FROM GUI</param>
        /// <returns>STRING REPRESENATION OF GANTT</returns>
        List<string> FitProcess(Process processToAdd);

        /// <summary>
        /// DOES THE COMPACTION NECESSARY TO THE PROCESSES
        /// </summary>
        List<string> Compact();

        List<string> Remove(Process processToRemove);
    }
}
