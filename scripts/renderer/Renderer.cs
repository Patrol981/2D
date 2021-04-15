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

    public uint newHeight;
    public int displace;


    #region  Player
    Player player;

    #endregion

    #region Diagnostics

    Framerate framerate;

    #endregion

    public Renderer(RenderWindow _app) {
      this._app = _app;

      windowColor = new Color(135,206,235);

      framerate = new Framerate(_app);

      player = new Player(_app, 700f, 400f, "./resources/hero.png", 180f);
    }

    public void Update() {
      _app.Clear(windowColor);
      
      framerate.Display();
      player.UpdateLogic();
    }
  }
}