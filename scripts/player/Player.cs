using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;

namespace _2D {
  public class Player: Entity {
    public float speed;
    public bool canAttack = true;

    Clock clock;
    public Player(EntityData entityData): base(entityData) {
      speed = .2f;
      _app.KeyReleased += OnKeyReleased;
      this.SetScale(new Vector2f(4f,4f));
      clock = new Clock();
    }

    public void OnKeyReleased(object sender, KeyEventArgs e) {
      if(e.Code == Keyboard.Key.Space) {
        AddForce(300);
      }

      if(e.Code == Keyboard.Key.Enter && canAttack) {
        canAttack = false;
        PlayAnimation();
      }
    }

    public override void Move() {
      if(Keyboard.IsKeyPressed(Keyboard.Key.D)) {
        this.x += this.speed;
        this.TurnRight();
        // this.FlipSprite(new Vector2f(1f,1f));
      }
      if(Keyboard.IsKeyPressed(Keyboard.Key.A)) {
        this.x -= this.speed;
        this.TurnLeft();
        // this.FlipSprite(new Vector2f(-1f,1f));
      }

      this.sprite.Position = new Vector2f(this.x, this.y);
    }
    public override void UpdateLogic() {
      if(clock.ElapsedTime > Time.FromSeconds(1f)) {
        clock.Restart();
        canAttack = true;
      }
      Move();
      UpdateGraphics();
      // UpdateNetwork();
    }

    public async void UpdateNetwork() {
      // Thread.Sleep(25);

      /*
      NetworkClient.SecondPlayer secondPlayerData;
			secondPlayerData.x = this.sprite.Position.X;
			secondPlayerData.y = this.sprite.Position.Y;
      */

      NetworkClient.PositionData positionData;
      positionData.msg = this.nickname;
      positionData.x = this.sprite.Position.X;
      positionData.y = this.sprite.Position.Y;

      // Console.WriteLine(this.networkData.udpClient.Client.Connected);

      Console.WriteLine(this.x);

      Byte[] sendDatagram = NetworkClient.GetBytes(positionData);
      await this.networkData.udpClient.SendAsync(sendDatagram, sendDatagram.Length);

      Byte[] recieve = this.networkData.udpClient.Receive(ref this.networkData.IP);
      //NetworkClient.PositionData secondPlayer = NetworkClient.FromBytes(recieve);
      NetworkClient.SecondPlayer secondPlayer = NetworkClient.FromBytesToSecondPlayer(recieve);
      // NetworkClient.PositionData recieveData = NetworkClient.FromBytes(recieve);
      // Console.WriteLine(secondPlayer.nickname);
      // Console.WriteLine(secondPlayer.x);
      // Console.WriteLine(secondPlayer.y);
    }
  }
}