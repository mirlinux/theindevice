using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Management;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace DeviceApp
{
    delegate void Callback(string message);
    delegate Hashtable GetStatus();

    public partial class Form1 : Form
    {

        private PerformanceCounter cpu =
            new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter mem = new PerformanceCounter("Memory", "Available MBytes");

        ManagementClass cls = new ManagementClass("Win32_OperatingSystem");

        double dCpu;
        double memUsage;
        int memPercent;
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

        

        private Hashtable getMachineStatus()
        {
            Hashtable status = new Hashtable();
            status.Add("IP", localhostIp);
            status.Add("MEM_TOTAL", iTotalMem);
            status.Add("MEM_USAGE", memUsage);
            status.Add("MEM_PERCENT", memPercent);
            status.Add("CPU", dCpu);
            return status;
        }

        private void initSocketServer ()
        {
            try
            {

                Callback callback = new Callback(DebugTextBox);
                GetStatus getStatus = new GetStatus(getMachineStatus);


                TcpListener server = new TcpListener(IPAddress.Parse(localhostIp), 9000);
                server.Start();

                int count = 0;
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    ClientSocket cSocket = new ClientSocket(count, client, localhostIp);
                    cSocket.callback = callback;
                    cSocket.status = getStatus; 
                    cSocket.connect();
                }

            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void DebugTextBox(string message)
        {
            tbLog.Invoke((MethodInvoker)delegate { tbLog.AppendText(message + "\r\n"); });
            tbLog.Invoke((MethodInvoker)delegate { tbLog.ScrollToCaret(); });
            tbLog.Invoke((MethodInvoker)delegate { DebugTextColor("SEND", Color.Red); });
            tbLog.Invoke((MethodInvoker)delegate { DebugTextColor("RECV", Color.Blue); });
        }
        private void DebugTextColor(string target, Color color)
        {
            Regex regex = new Regex(target);
            MatchCollection mc = regex.Matches(tbLog.Text);
            int iCursorPosition = tbLog.SelectionStart;

            foreach (Match m in mc)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;

                tbLog.Select(iStartIdx, iStopIdx);
                tbLog.SelectionColor = color;
                tbLog.SelectionStart = iCursorPosition;
                tbLog.SelectionColor = Color.Black;
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
            dCpu = Math.Round(cpu.NextValue(), 2);
            lbCpu.Text = dCpu.ToString() + " %";
            pbCpu.Value = (int)dCpu;

            memUsage = mem.NextValue();
            memPercent = (int)((memUsage / (double)iTotalMem) * 100);

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

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(initSocketServer);
            thread.IsBackground = true;
            thread.Start();
        }
    }

    class ClientSocket
    {
        private int clientId = 0;
        private TcpClient client;
        public Callback callback;
        public GetStatus status;
        private StreamReader receiver;
        private StreamWriter sender;
        private string ip = "";

        public ClientSocket(int clientId, TcpClient client, string ip)
        {
            this.clientId = clientId;
            this.client = client;
            this.receiver = new StreamReader(client.GetStream());
            this.sender = new StreamWriter(client.GetStream());
            this.sender.AutoFlush = true;
            this.ip = ip;
        }

        public void connect()
        {
            Thread thread = new Thread(worker);
            thread.Start();
        }

        public void worker()
        {
            Socket c = client.Client;
            IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
            string client_ip = ip_point.Address.ToString();
            callback("Connected Client [" + client_ip + "]");

            // 최초로 접속시 Client에게 CONNECT 정보를 전달해 준다.
            string sendData = "CONNECT:" + Environment.MachineName + ";" + ip;
            callback("SEND [" + sendData + "]");
            sender.WriteLine(sendData);
            // "CONNECT:LOCALHOSTNAME;192.168.0.8"
            
            while (client.Connected)
            {
                string message = receiver.ReadLine();
                callback("RECV [" + message + "]");
                string[] dataArray = message.Split(":");
                string command = dataArray[0];
                string body = dataArray[1];
                switch(command)
                {
                    case "GET_STATUS":
                        sendStatusData();
                        break;
                    case "STOP_STATUS":
                        isSendStatus = false;
                        break;
                    default:
                        break;
                }

            }
        }
        bool isSendStatus = false;
        private void sendStatusData()
        {
            isSendStatus = true;
            Thread t = new Thread(sendStatus);
            t.Start();
        }
        private void sendStatus()
        {
            while (isSendStatus)
            {
                Hashtable data = status();
                string statusData = "" + data["CPU"] + ";" + data["MEM_TOTAL"] + ";"
                    + data["MEM_USAGE"] + ";" + data["MEM_PERCENT"];
                string sendData = "STATUS:" + statusData;
                sender.WriteLine(sendData);
                callback("SEND [" + sendData + "]");
                Thread.Sleep(1000);
            }
        }
    }
}