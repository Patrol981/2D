using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Net;
using System.Net.Sockets;

namespace _2D {
  public class Renderer {

    public RenderWindow _app;

    Color windowColor;

    public uint newHeight;
    public int displace;


    #region  Player
    Player player;

    #endregion

    #region NPC
    List<Entity> npcs = new List<Entity>();
    Entity secondPlayer;
    #endregion

    #region Terrain
    public Terrain terrain;
    #endregion

    #region Diagnostics

    Framerate framerate;

    #endregion
    #region Networking
    private UdpClient udpClient;
    private IPEndPoint IP;
    #endregion

    public Renderer(RenderWindow _app, NetworkClient.NetworkData networkData, string nickname) {
      this._app = _app;
      this.udpClient = networkData.udpClient;
      this.IP = networkData.IP;
      windowColor = new Color(135,206,235);
      framerate = new Framerate(_app);

      // terrain = new Terrain(_app.Size);

      EntityData playerEntityData;
      playerEntityData._app = _app;
      playerEntityData.renderer = this;
      playerEntityData.x = 700f;
      playerEntityData.y = 400f;
      playerEntityData.spritePath = "./resources/hero.png";
      playerEntityData.rotation = 180f;
      playerEntityData.networkData = networkData;
      playerEntityData.nickname = nickname;
      player = new Player(playerEntityData);

      EntityData secondPlayerEntityData;
      secondPlayerEntityData._app = _app;
      secondPlayerEntityData.renderer = this;
      secondPlayerEntityData.x = 400f;
      secondPlayerEntityData.y = 400f;
      secondPlayerEntityData.spritePath = "./resources/hero.png";
      secondPlayerEntityData.rotation = 180f;
      secondPlayerEntityData.networkData = networkData;
      secondPlayerEntityData.nickname = nickname;
      secondPlayer = new Entity(secondPlayerEntityData);
      secondPlayer.SetScale(new Vector2f(4f,4f));

      // treat other players as npcs
      int playersCount;
      //for(int i=0; i<playersCount; i++) {
        //npcs.Add()
      //}

      // npcs.Add(new Entity(_app, this, 800f, 400f, "./resources/hero.png", 180f));

    }

    public async void UpdateEntitiesNetwork() {
      NetworkClient.PositionData positionData;
      positionData.msg = player.nickname;
      positionData.x = player.sprite.Position.X;
      positionData.y = player.sprite.Position.Y;

      Byte[] sendDatagram = NetworkClient.GetBytes(positionData);
      await this.udpClient.SendAsync(sendDatagram, sendDatagram.Length);

      Byte[] recieve = this.udpClient.Receive(ref this.IP);
      NetworkClient.SecondPlayer secondPlayer = NetworkClient.FromBytesToSecondPlayer(recieve);

      Console.WriteLine($"{secondPlayer.nickname} {secondPlayer.x} {secondPlayer.y}");

      this.secondPlayer.x = secondPlayer.x;
      this.secondPlayer.y = secondPlayer.y;
      this.secondPlayer.UpdateLogic();
    }

    public void Update() {
      _app.Clear(windowColor);
      framerate.Display();
      player.UpdateLogic();

      UpdateEntitiesNetwork();

      for(int i=0; i<npcs.Count; i++) {
        // npcs[i].UpdateLogic();
      }
      // _app.Draw(terrain.convexShape);
    }

    private async void HandleData() {
      Byte[] sendStartMsg = Encoding.ASCII.GetBytes("Hello server!");
      await udpClient.SendAsync(sendStartMsg, sendStartMsg.Length);
      Byte[] receiveBytes = udpClient.Receive(ref IP);
      string returnData = Encoding.ASCII.GetString(receiveBytes);
      Console.WriteLine($"Server says {returnData}");
    }
  }
}