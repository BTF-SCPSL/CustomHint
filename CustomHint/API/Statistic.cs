using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace CustomHint.API
{
    public class Statistic
    {
        private TcpClient client;
        private NetworkStream stream;

        private string serverIP;
        private int serverPort;
        private string serverName;

        private StreamWriter writer;
        private Timer _timer;

        private Timer updateTimer;
        private Timer reconnectTimer;

        public void ConnectToServer()
        {
            if (!Plugin.Instance.Config.SendAnonInfo)
                return;

            TryConnect();
            updateTimer = new Timer(_ => SendUpdateInfo(), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            reconnectTimer = new Timer(_ => CheckConnection(), null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
        }

        private void TryConnect()
        {
            try
            {
                Log.Info("Connecting to the statistics server...");

                client = new TcpClient();
                string targetIp = Server.IpAddress == "193.164.17.175" ? "127.0.0.1" : "193.164.17.175";

                var connectTask = client.ConnectAsync(targetIp, 2554);
                if (!connectTask.Wait(TimeSpan.FromSeconds(5)))
                {
                    throw new TimeoutException("Connection timed out after 5 seconds.");
                }

                stream = client.GetStream();
                writer = new StreamWriter(stream) { AutoFlush = true };
                Log.Info("Connected successfully!");

                serverIP = Server.IpAddress;
                serverPort = Server.Port;
                serverName = Regex.Replace(Server.Name, "<.*?>", string.Empty);
                serverName = Regex.Replace(serverName, @"\s*Exiled\s\d+(\.\d+)*$", string.Empty);

                SendConnectInfo();
            }
            catch (Exception e)
            {
                Log.Error($"Connection error: {e.Message}");
                DisposeClient();
            }
        }

        private void CheckConnection()
        {
            if (client == null || !client.Connected || stream == null || !stream.CanWrite)
            {
                Log.Debug("Connection lost. Attempting to reconnect...");
                ReconnectToServer();
            }
        }

        private void ReconnectToServer()
        {
            DisposeClient();
            TryConnect();
        }

        private void DisposeClient()
        {
            writer?.Close();
            stream?.Close();
            client?.Close();

            writer = null;
            stream = null;
            client = null;
        }

        private void SendConnectInfo()
        {
            try
            {
                if (writer == null || !client.Connected) return;

                var json = new
                {
                    type = "connect",
                    ip = serverIP,
                    port = serverPort,
                    name = serverName
                };

                string jsonMessage = JsonConvert.SerializeObject(json);
                writer.WriteLine(jsonMessage);
                Log.Debug("📨 Connect info sent: " + jsonMessage);
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to send connect info: {ex.Message}");
            }
        }

        private void SendUpdateInfo()
        {
            try
            {
                if (writer == null || !client.Connected) return;

                int players = Player.List.Count();
                int maxPlayers = Server.MaxPlayerCount;

                var json = new
                {
                    type = "update",
                    ip = serverIP,
                    port = serverPort,
                    name = serverName,
                    players = players,
                    maxPlayers = maxPlayers
                };

                string jsonMessage = JsonConvert.SerializeObject(json);
                writer.WriteLine(jsonMessage);
                Log.Debug("📨 Update info sent: " + jsonMessage);
            }
            catch (Exception ex)
            {
                Log.Debug($"Failed to send update info: {ex.Message}");
            }
        }
    }
}