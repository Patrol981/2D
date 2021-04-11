using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace _2D {
  public class Renderer {

    public RenderWindow _app;

    Color windowColor;


    #region  Player
    Player player;

    #endregion

    public Renderer(RenderWindow _app) {
      this._app = _app;

      windowColor = new Color(10,10,10);

      player = new Player(_app, 700f, 400f, "./resources/adlero.JPG", 180f);
    }

    public void Update() {
      _app.Clear(windowColor);
			// playerTexture.Update(_app);
			// _app.Draw(playerSprite);
			// sprite.Rotation += .03f;
      player.UpdateLogic();
    }
  }
}