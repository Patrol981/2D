using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;
using System;

namespace _2D {
  public class Player: Entity {
    public float speed;
    public bool canAttack = true;

    Clock clock;
    public Player(RenderWindow _app, float x, float y, string spritePath, float rotation): base(_app, x, y, spritePath, rotation) {
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
    }
  }
}