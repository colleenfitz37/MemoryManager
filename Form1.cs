using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// PREFERENCE IS GIVEN TO THE WAITING QUEUE PROCESSES OVER ANY INCOMING PROCESSES
namespace MemoryManager
{
    public partial class Form1 : Form
    {
        Dictionary<string, Process> processes = new Dictionary<string, Process>();
        private static MemoryManager mm;
        Process p;
        int totalMemory;
        int osMemory;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTotalMemory.Text = "4000";
            txtOSMemory.Text = "400";
            mm = new MemoryManager(Convert.ToInt32(txtTotalMemory.Text), Convert.ToInt32(txtOSMemory.Text));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbProcesses.SelectedItem.ToString()))
                {
                    if (!string.IsNullOrEmpty(cmbAlgorithm.SelectedItem.ToString()))
                    {
                        if (!string.IsNullOrEmpty(txtProcessSize.Text))
                        {
                            if (!string.IsNullOrEmpty(txtOSMemory.Text))
                            {
                                if (!string.IsNullOrEmpty(txtTotalMemory.Text))
                                {
                                    string pid = cmbProcesses.SelectedItem.ToString();
                                    string algorithm = cmbAlgorithm.SelectedItem.ToString();
                                    int size = Convert.ToInt32(txtProcessSize.Text);
                                    totalMemory = Convert.ToInt32(txtTotalMemory.Text);
                                    osMemory = Convert.ToInt32(txtOSMemory.Text);
                                    mm._TotalMemory = Convert.ToInt32(txtTotalMemory.Text);
                                    mm._OSMemory = Convert.ToInt32(txtOSMemory.Text);
                                    p = new Process(pid, size);
                                    if (!processes.ContainsKey(pid))
                                    {
                                        processes.Add(p.PID.ToString(), p);
                                        lstMemory.Items.Clear();
                                        lstMemory.Items.AddRange(mm.AddProcess(algorithm, p));
                                    }
                                    else
                                    {
                                        MessageBox.Show("Process already added to memory");
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please enter the size of RAM");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter the size of the OS memory requirrments");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter the size of the process");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select the memory allocation algorithm to use");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select the process ID from the list");
                    return;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter numbers and letters where appropriate");
            }
            catch (NullReferenceException ex2)
            {
                MessageBox.Show("Please do not leave input areas blank");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbProcesses.SelectedItem.ToString()))
                {
                    string pid = cmbProcesses.SelectedItem.ToString();
                    if (processes.ContainsKey(pid))
                    {
                        p = processes[pid];
                        lstMemory.Items.Clear();
                        lstMemory.Items.AddRange(mm.RemoveProcess(p));
                        processes.Remove(pid);
                    }
                    else
                    {
                        MessageBox.Show("Process not found");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select the process ID from the list");
                    return;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please enter numbers and letters where appropriate");
            }
            catch (NullReferenceException ex2)
            {
                MessageBox.Show("Please do not leave input areas blank");
            }
        }

        private void btnCompact_Click(object sender, EventArgs e)
        {
            lstMemory.Items.Clear();
            lstMemory.Items.AddRange(mm.CompactMemory());
        }

        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
