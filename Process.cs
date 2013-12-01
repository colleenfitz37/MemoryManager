using System;
/// AUTHOR: DAN FERGUSON

namespace MemoryManager
{
    /// <summary>
    /// DEFINES THE PROCESS OBJECT
    /// </summary>
    public class Process
    {
        private string pid;
        private int memSize;
        private int startLocation;

        /// <summary>
        /// CONSTRUCTOR FOR EVERY PROCESS OBJECT.  CONTAINS ALL NECESSARY INFORMATION FOR EACH PROCESS
        /// </summary>
        /// <param name="PID">PROCESS ID</param>
        /// <param name="burstTime">PROCESS BURST TIME</param>
        /// <param name="priority">PROCESS PRIORITY IF APPLICABLE.  IF NOT, PRIORITY IS -1</param>
        /// <param name="arriveTime">PROCESS ARRIVAL TIME</param>
        public Process(string pid, int size)
        {
            this.pid = pid;
            this.memSize = size;
        }

        #region GETTERS AND SETTERS FOR PROCESS DATA MEMBERS
        public string PID { get { return pid; } }
        public int size { get { return memSize; } }
        public int StartLocation { get { return startLocation; } set { startLocation = value; } }
        #endregion
    }
}
