---

<div align="center">

# 🚀 SKIDC2 - Ultimate C2 Framework 🔥

### **The most comprehensive C2 framework written in C# for... educational purposes 😉

![version](https://img.shields.io/badge/version-1.0.0-red) 
![csharp](https://img.shields.io/badge/C%23-.NET%204.8-blue)
![platform](https://img.shields.io/badge/platform-Windows-lightgrey)
![license](https://img.shields.io/badge/license-EDUCATIONAL ONLY-brightgreen)
![vibe](https://img.shields.io/badge/vibe-100%25%20Skid-orange)

**⚠️ WARNING: FOR EDUCATIONAL PURPOSES ONLY. DO NOT USE ILLEGALLY. ⚠️**

</div>

## 📖 Overview

SKIDC2 is a sophisticated Command and Control (C2) framework designed for penetration testers and security researchers. It features a modular console-based server with extensive capabilities for remote administration, surveillance, and post-exploitation.

```bash
# Just look at this clean interface
SKIDC2> list
[+] Connected Bots:
INDEX | IP:PORT
------|---------
  0   | 192.168.1.69:1337
  1   | 10.0.0.15:1337

SKIDC2> interact 0
[+] Interacting with bot 0 (192.168.1.69:1337)
BOT/0> info
[RESPONSE] User: victim | Machine: DESKTOP-ABC123 | OS: Windows 10 | Admin: True
```

## ✨ Features

### 🎯 **Core Capabilities**
- **Builder System** - Generate customized clients with specific IP/Port
- **Bot Management** - Manage multiple connections simultaneously  
- **Interactive Sessions** - Real-time command execution
- **Stealth Operations** - Low detection rate with various evasion techniques

### 🕵️ **Surveillance Modules**
- 🔑 **Keylogger** - Real-time keystroke capture
- 📸 **Screenshot** - Remote desktop capture
- 🎥 **Webcam** - Camera access (theoretical)
- 🎤 **Microphone** - Audio recording (theoretical)
- 📋 **Clipboard** - Clipboard content monitoring

### ⚡ **System Operations**
- 🔒 **Workstation Lock** - Remote lock capability
- ⚡ **Shutdown/Reboot** - Power management
- 💣 **BSOD** - Trigger system crashes (use responsibly)
- 💬 **Message Box** - Display messages to user

### 📁 **File Operations**
- 📥 **Download** - Exfiltrate files from target
- 📤 **Upload** - Deploy files to target  
- 📂 **File Explorer** - Browse remote filesystem
- 🗑️ **File Manipulation** - Delete, execute, view files

### 🌐 **Network Capabilities**
- 🔍 **Network Scanning** - Discover hosts and services
- 🎯 **Port Scanning** - Identify open ports
- ⚔️ **DDoS** - Distributed denial of service (theoretical)
- 🔄 **ARP Poisoning** - Man-in-the-middle attacks

### 🛡️ **Defense Evasion**
- 🚫 **AV Killer** - Disable antivirus software
- 🔥 **Firewall Disabler** - Bypass network restrictions
- 🛡️ **Defender Control** - Manage Windows Defender
- 🔓 **AMSI Bypass** - Avoid script scanning

## 🚀 Quick Start

### Prerequisites
- Windows OS
- .NET Framework 4.8
- Visual Studio 2019+ (recommended)

### Installation
```bash
# Clone the repository
git clone https://github.com/yourusername/SKIDC2.git
cd SKIDC2

# Open in Visual Studio
start SKIDC2.sln
```

### Basic Usage
1. **Build the client**
   ```bash
   SKIDC2> build
   [+] Enter IP for client: 192.168.1.100
   [+] Enter Port for client: 1337
   [+] Client built successfully: Client.exe
   ```

2. **Start listening**
   ```bash
   SKIDC2> listen
   [+] Enter port to listen on: 1337
   [+] Listening on port 1337...
   ```

3. **Execute on target**  
   Run `Client.exe` on target machine

4. **Manage connections**
   ```bash
   SKIDC2> list
   SKIDC2> interact 0
   BOT/0> shell whoami
   ```

## 🛠️ Command Reference

### Basic Commands
| Command | Description |
|---------|-------------|
| `build` | Create a new client executable |
| `listen` | Start C2 server listener |
| `list` | Show connected bots |
| `interact <#>` | Enter interactive session with bot |
| `help` | Show command help |

### Surveillance Commands  
| Command | Description |
|---------|-------------|
| `screenshot` | Capture remote desktop |
| `keylog` | Retrieve keystroke logs |
| `webcam` | Capture webcam image |
| `processes` | List running processes |

### System Commands
| Command | Description |
|---------|-------------|
| `lock` | Lock workstation |
| `shutdown` | Force shutdown |
| `reboot` | Force reboot |
| `persist` | Install persistence |

## ⚠️ Legal Disclaimer

This project is intended for **EDUCATIONAL PURPOSES ONLY**. The developers are not responsible for any misuse of this software. 

- 🚫 **Do not** use on systems you don't own
- 🚫 **Do not** use for illegal activities  
- 🚫 **Do not** deploy in production environments
- ✅ **Do** use in controlled lab environments
- ✅ **Do** use for learning and research

## 🔧 Development

### Project Structure
```
SKIDC2/
├── Program.cs                 # Main server application
├── ClientTemplate.cs          # Client source template
├── HvncForm.cs               # Hidden VNC implementation
├── Resources/                # Embedded resources
└── SkidC2.csproj            # Project file
```

### Adding New Modules
1. Add command to switch statement in `Program.cs`
2. Implement server-side handler method
3. Add client-side execution in `ClientTemplate.cs`
4. Update help documentation

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 TODO List

- [ ] Implement full HVNC functionality
- [ ] Add encryption for communications
- [ ] Develop web-based interface
- [ ] Add lateral movement modules
- [ ] Implement process injection
- [ ] Add anti-analysis techniques

## 🙋 FAQ

**Q: Is this detectable by antivirus?**  
A: Probably. This is a proof-of-concept without advanced obfuscation.

**Q: Can I use this for red teaming?**  
A: Only in environments where you have explicit permission.

**Q: Why "SKIDC2"?**  
A: Because we acknowledge the skid within all of us. 😎

**Q: How do I uninstall?**  
A: Use the `uninstall` command from an interactive session.

## 📜 License

This project is licensed under the **EDUCATIONAL USE ONLY** license. See [LICENSE](LICENSE) file for details.

## 👨‍💻 Authors

- **Your Name** - *Initial work* - [dex](https://github.com/syntrixseller)

## 🌟 Acknowledgments

- Inspired by various security research projects
- Thanks to the infosec community for continuous learning
- Shoutout to all the skids trying to learn something new

---

<div align="center">

**💀 USE RESPONSIBLY. DON'T BE A DICK. 💀**

</div>

---
