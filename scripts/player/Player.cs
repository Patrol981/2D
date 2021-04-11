using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;

namespace _2D {
  public class Player {
    public RenderWindow _app;

    public float x;
    public float y;
    public Texture texture;
    public Sprite sprite;
    public float speed;
    public float rotation;
    public Player(RenderWindow _app, float x, float y, string spritePath, float rotation) {
      this._app = _app;

      this.x = x;
      this.y = y;
      this.texture = new Texture(spritePath);
      this.texture.Smooth = false;
      this.texture.Repeated = false;
      this.sprite = new Sprite(this.texture);
      speed = .1f;
      this.rotation = rotation;
      this.sprite.Position = new Vector2f(this.x, this.y);
      this.sprite.Rotation = this.rotation;
    }
    public void Move() {
      if(Keyboard.IsKeyPressed(Keyboard.Key.D)) {
        this.x += this.speed;
      }
      
      if(Keyboard.IsKeyPressed(Keyboard.Key.A)) {
        this.x -= this.speed;
      }
      this.sprite.Position = new Vector2f(this.x, this.y);
    }

    public void UpdateGraphics() {
      this.texture.Update(_app);
      _app.Draw(this.sprite);
    }
    public void UpdateLogic() {
      Move();
      UpdateGraphics();
    }
  }
}