using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace DeviceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            groupBox1.Text = Environment.MachineName;

            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = entry.AddressList;
            foreach(IPAddress ip in addr)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (ip.ToString().Contains("192.168"))
                    {
                        lbIpAddress.Text = ip.ToString();
                    }
                }
            }


        }

        private PerformanceCounter cpu =
            new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter mem = new PerformanceCounter("Memory", "Available MBytes");
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbCpu.Text = Math.Round(cpu.NextValue(), 2).ToString() + " %";
            lbMem.Text = mem.NextValue().ToString() + " MB";
        }
    }
}