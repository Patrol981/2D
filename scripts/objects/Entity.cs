using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;
using System;

namespace _2D {
  public class Entity {
    public RenderWindow _app;
    public float x;
    public float y;
    public Texture texture;
    public Sprite sprite;
    public float rotation;

    public float force;

    public Entity(RenderWindow _app, float x, float y, string spritePath, float rotation) {
      this._app = _app;

      this.x = x;
      this.y = y;

      this.texture = new Texture(spritePath);
      this.texture.Smooth = false;
      this.texture.Repeated = false;
      this.sprite = new Sprite(this.texture);

      this.rotation = rotation;
      this.sprite.Position = new Vector2f(this.x, this.y);
      this.sprite.Rotation = this.rotation;
    }

    public void UpdatePhysics() {
      if(this.sprite.Position.Y != 700 && this.force == 0) {
        this.y += 0.25f;
      } else if(force != 0) {
        this.y -= 0.5f;
        this.force -= 0.5f;
      }
    }

    public void UpdateGraphics() {
      this.texture.Update(_app);
      _app.Draw(this.sprite);
      UpdatePhysics();
    }

    public void SetRotation(float rotation) {
      this.sprite.Rotation = rotation;
    }

    public void Rotate(float rotation) {
      this.sprite.Rotation += rotation;
    }

    public void FlipSprite(Vector2f scale) {
      this.sprite.Scale = scale;
    }

    public void AddForce(float force) {
      this.force += force;
    }

    public void SetForce(float force) {
      if(this.force > 0) {
        return;
      }
      this.force = force;
    }
  }
}