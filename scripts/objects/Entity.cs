using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace _2D {
  public struct EntityData {
    public RenderWindow _app;
    public Renderer renderer;
    public float x;
    public float y;
    public string spritePath;
    public float rotation;
    public string nickname;
    public NetworkClient.NetworkData networkData;
  }
  public class Entity {
    public RenderWindow _app;
    public float x;
    public float y;
    public string nickname;
    public Texture texture;
    public Sprite sprite;
    public float rotation;

    public float force;

    bool firstStepIdle = true;
    public bool waitingForAnimationFrame = false;
    public bool state = true;

    Clock clock;

    int turn;
    int idle;
    enum Animations {
      Idle1 = 64,
      Idle2 = 96,
      SpecialAnimation1 = 32,
      SpecialAnimation2 = 0,
    }

    // Terrain ref
    private Terrain myTerrain;
    private Renderer rendererRef;

    // Networking
    protected NetworkClient.NetworkData networkData;

    public Entity(EntityData entityData) {
      this._app = entityData._app;

      this.x = entityData.x;
      this.y = entityData.y;

      this.nickname = entityData.nickname;

      this.texture = new Texture(entityData.spritePath);
      this.texture.Smooth = false;
      this.texture.Repeated = false;
      this.sprite = new Sprite(this.texture);
      this.sprite.TextureRect = SpriteLoader.SetRect(0,32,32,32);

      this.rotation = entityData.rotation;
      this.sprite.Position = new Vector2f(this.x, this.y);
      this.sprite.Rotation = this.rotation;

      this.networkData = entityData.networkData;

      clock = new Clock();

      this.turn = 0;
      this.idle = 0;

      // this.myTerrain = myTerrain;
      rendererRef = entityData.renderer;
    }
    // if ( sprite.getPosition().y + sprite.getLocalBounds().height >= window.getSize().y ) { }
    public virtual void UpdateLogic() {
      Move();
      UpdateGraphics();
    }

    public virtual void Move() {
      this.sprite.Position = new Vector2f(this.x, this.y);
    }
    public void UpdatePhysics() {
      //float closestX = rendererRef.terrain.positions.OrderBy(myX => Math.Abs(sprite.Position.X - myX)).First();
      //float closestY;
      //if(this.sprite.Position.Y + sprite.GetGlobalBounds().Height <= rendererRef.terrain.positions)
      if(this.sprite.Position.Y + sprite.GetLocalBounds().Height <= _app.Size.Y  && this.force == 0) {
        this.y += 0.25f;
      } else if(force != 0) {
        this.y -= 0.5f;
        this.force -= 0.5f;
      }
    }

    public void UpdateGraphics() {
      if(clock.ElapsedTime > Time.FromSeconds(.5f) && !waitingForAnimationFrame) {
        clock.Restart();
        if(this.firstStepIdle) {
          this.firstStepIdle = false;
          this.idle = Convert.ToInt32(Animations.Idle1);

          //this.sprite.TextureRect = SpriteLoader.SetRect(this.turn,Convert.ToInt32(Animations.Idle1),32,32);
        } else {
          this.firstStepIdle = true;
          this.idle = Convert.ToInt32(Animations.Idle2);
          //this.sprite.TextureRect = SpriteLoader.SetRect(this.turn,Convert.ToInt32(Animations.Idle2),32,32);
        }
      } else if(waitingForAnimationFrame && clock.ElapsedTime > Time.FromSeconds(.15f)) {
        waitingForAnimationFrame = false;
        this.idle = Convert.ToInt32(Animations.SpecialAnimation2);
        clock.Restart();
      }
      this.sprite.TextureRect = SpriteLoader.SetRect(this.turn,this.idle,32,32);
      this.texture.Update(_app);
      _app.Draw(this.sprite);
      UpdatePhysics();
    }

    public void TurnRight() {
      this.turn = 32;
    }

    public void TurnLeft() {
      this.turn = 0;
    }

    public void PlayAnimation() {
      clock.Restart();
      this.idle = Convert.ToInt32(Animations.SpecialAnimation1);
      this.waitingForAnimationFrame = true;
    }

    public void SetRotation(float rotation) {
      this.sprite.Rotation = rotation;
    }

    public void Rotate(float rotation) {
      this.sprite.Rotation += rotation;
    }

    public void SetScale(Vector2f scale) {
      this.sprite.Scale = scale;
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