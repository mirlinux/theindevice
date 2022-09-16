using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Management;
using MySql.Data.MySqlClient;

namespace DeviceApp
{
    public partial class Form1 : Form
    {

        private PerformanceCounter cpu =
            new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter mem = new PerformanceCounter("Memory", "Available MBytes");

        ManagementClass cls = new ManagementClass("Win32_OperatingSystem");

        private int iTotalMem = 0;

        private string localhostName = Environment.MachineName;
        private string localhostIp = "";
        private int machineId = -1;

        public Form1()
        {
            InitializeComponent();
            GetIP();
            GetMemory();
            GetDatabaseInfo();
            if (machineId < 0)
            {
                SetMachineInfo();
                GetDatabaseInfo();
            }
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
            groupBox1.Text = localhostName;

            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = entry.AddressList;
            foreach (IPAddress ip in addr)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (ip.ToString().Contains("192.168"))
                    {
                        localhostIp = ip.ToString();
                        lbIpAddress.Text = ip.ToString();
                    }
                }
            }
        }

        private void GetDatabaseInfo ()
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(Config.DB_DATASOURSE);
                conn.Open ();

                string query = String.Format(
                    "SELECT id FROM machine WHERE name = '{0}' AND ip = '{1}'",
                    localhostName, localhostIp);
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    result.Read ();
                    machineId = int.Parse(result["id"].ToString());
                }
                Debug.WriteLine("machine id [{0}]", machineId);


            } catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            } finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void SetMachineInfo()
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(Config.DB_DATASOURSE);
                conn.Open();

                string query = String.Format(
                    "INSERT INTO machine (name, ip) VALUES('{0}','{1}')",
                    localhostName, localhostIp);
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    result.Read();
                    machineId = int.Parse(result["id"].ToString());
                }
                Debug.WriteLine("SetMachineInfo:: machine id [{0}]", machineId);


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
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

            UpdateStateInfoToDB(dCpu, memUsage, memPercent);
        }

        private void UpdateStateInfoToDB(double dCpu, double dMem, int percent)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(Config.DB_DATASOURSE);
                conn.Open();

                string query = String.Format(
                    "INSERT INTO log (time, machine_id, ip, cpu, mem, mem_usage) " +
                    "VALUES(now(), {0},'{1}', {2}, {3}, {4})",
                    machineId, localhostIp, dCpu, dMem, percent);
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader result = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
    }
}