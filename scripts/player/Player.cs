using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;

namespace _2D {
  public class Player: Entity {
    
    public float speed;
    public Player(RenderWindow _app, float x, float y, string spritePath, float rotation): base(_app, x, y, spritePath, rotation) {
      speed = .1f;
    }
    public void Move() {
      if(Keyboard.IsKeyPressed(Keyboard.Key.D)) {
        this.x += this.speed;
        this.FlipSprite(new Vector2f(1f,1f));
      }
      
      if(Keyboard.IsKeyPressed(Keyboard.Key.A)) {
        this.x -= this.speed;
        this.FlipSprite(new Vector2f(-1f,1f));
      }

      if(Keyboard.IsKeyPressed(Keyboard.Key.Space) && this.force <= 0) {
        SetForce(3);
      }

      this.sprite.Position = new Vector2f(this.x, this.y);
    }
    public void UpdateLogic() {
      Move();
      UpdateGraphics();
    }
  }
}