using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Management;


namespace DeviceApp
{
    public partial class Form1 : Form
    {

        private PerformanceCounter cpu =
            new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter mem = new PerformanceCounter("Memory", "Available MBytes");

        ManagementClass cls = new ManagementClass("Win32_OperatingSystem");

        private int iTotalMem = 0;


        public Form1()
        {
            InitializeComponent();
            GetIP();
            GetMemory();
        }

        private void GetMemory()
        {
            try
            {
                ManagementObjectCollection moc = cls.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    iTotalMem = int.Parse(mo["TotalVisibleMemorySize"].ToString());
                }
                iTotalMem = iTotalMem / 1024;
                
                lbMemTotal.Text = String.Format("( {0:N0} MB )", iTotalMem);
            }
            catch (Exception ex)
            {

            }
        }



        private void GetIP()
        {
            groupBox1.Text = Environment.MachineName;

            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = entry.AddressList;
            foreach (IPAddress ip in addr)
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

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            double dCpu = Math.Round(cpu.NextValue(), 2);
            lbCpu.Text = dCpu.ToString() + " %";
            pbCpu.Value = (int)dCpu;

            double memUsage = mem.NextValue();
            int memPercent = (int)((memUsage / (double)iTotalMem) * 100);

            lbMem.Text = String.Format("{0:N0} MB", mem.NextValue());
            pbMem.Value = memPercent;
        }
    }
}