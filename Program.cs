using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Net;
using System.Net.Sockets;

namespace _2D {
	class Program {
		static void OnClose(object sender, EventArgs e) {
			RenderWindow window = (RenderWindow)sender;
			window.Close();
		}
    static void Main(string[] args) {
			Console.Write("Wpisz nick: ");
			String nickname = Console.ReadLine();

			Console.Write("Podaj adres IP: ");
			String IPAddr = Console.ReadLine();

			/*
			UdpClient udpClient = new UdpClient();
			IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.5"), 4342);
			udpClient.Connect(ipEndPoint.Address, ipEndPoint.Port);
			udpClient.Client.ReceiveTimeout = 500;
			// Byte[] sendMsg = Encoding.ASCII.GetBytes("siema server");
			// udpClient.Send(sendMsg, sendMsg.Length);

			NetworkClient.PositionData positionData;
			positionData.x = 0f;
			positionData.y = 0f;
			positionData.msg = nickname;
      Byte[] sendDatagram2 = NetworkClient.GetBytes(positionData);

			while(true) {
				Thread.Sleep(1000);
				Console.WriteLine("Sending data");
				udpClient.SendAsync(sendDatagram2, sendDatagram2.Length);

				// NetworkClient.ServerPlayerData returnData = NetworkClient.ServerDataFromBytes(recieve);

				Byte[] recieve = udpClient.Receive(ref ipEndPoint);
				NetworkClient.SecondPlayer recievedData = NetworkClient.FromBytesToSecondPlayer(recieve);
				Console.WriteLine(recievedData.nickname);
				// int returnData = BitConverter.ToInt32(recieve);
				// Console.WriteLine(returnData);
				//Console.WriteLine(returnData.playerName);
				//Console.WriteLine(returnData.x);
				//Console.WriteLine(returnData.y);
			}
			*/

			VideoMode desktopResolution = VideoMode.DesktopMode;
			VideoMode debugRes = new VideoMode(860,530);
			// RenderWindow app = new RenderWindow(desktopResolution, "2D Game", Styles.Fullscreen);
			RenderWindow app = new RenderWindow(debugRes, "2D Network Test");
			app.Closed += new EventHandler(OnClose);

			UdpClient udpClient = new UdpClient();
			IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(IPAddr), 4342);
			udpClient.Connect(ipEndPoint.Address, ipEndPoint.Port);
			NetworkClient.NetworkData networkData;
			networkData.IP = ipEndPoint;
			networkData.udpClient = udpClient;

			NetworkClient.PositionData positionData;
			positionData.x = 0f;
			positionData.y = 0f;
			positionData.msg = nickname;
      Byte[] sendDatagram2 = NetworkClient.GetBytes(positionData);
			try {
				// udpClient.SendAsync(sendDatagram2, sendDatagram2.Length);
			} catch(Exception ex) {
				Console.WriteLine(ex.ToString());
			}

			Renderer renderer = new Renderer(app, networkData, nickname);

			while(app.IsOpen) {
				app.DispatchEvents();
				renderer.Update();
				app.Display();
			}

    }
  }
}
