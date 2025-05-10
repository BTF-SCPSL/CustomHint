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

        public void ConnectToServer()
        {
            if (!Plugin.Instance.Config.SendAnonInfo)
                return;

            try
            {
                Log.Info("Connecting to the server for statistics...");

                client = new TcpClient();
                string targetIp = Server.IpAddress == "193.164.17.175" ? "127.0.0.1" : "193.164.17.175";

                var connectTask = client.ConnectAsync(targetIp, 2554);
                if (!connectTask.Wait(TimeSpan.FromSeconds(5)))
                {
                    throw new TimeoutException("Connection timed out after 5 seconds.");
                }

                stream = client.GetStream();
                writer = new StreamWriter(stream) { AutoFlush = true };
                Log.Info("Connected sucessfully!");

                serverIP = Server.IpAddress;
                serverPort = Server.Port;
                serverName = Regex.Replace(Server.Name, "<.*?>", string.Empty);
                serverName = Regex.Replace(serverName, @"\s*Exiled\s\d+(\.\d+)*$", string.Empty);

                SendConnectInfo();
                _timer = new Timer(_ => SendUpdateInfo(), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            }
            catch (Exception e)
            {
                Log.Error($"Connection error to the server for statistics: {e.Message}");
            }
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
                Log.Error($"Failed to send update info: {ex.Message}");
            }
        }
    }
}