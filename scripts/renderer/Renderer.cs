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

    #region NPC
    List<Entity> npcs = new List<Entity>();
    #endregion

    #region Terrain
    public Terrain terrain;
    #endregion

    #region Diagnostics

    Framerate framerate;

    #endregion

    public Renderer(RenderWindow _app) {
      this._app = _app;

      windowColor = new Color(135,206,235);

      framerate = new Framerate(_app);

      terrain = new Terrain(_app.Size);

      player = new Player(_app, 700f, 400f, "./resources/hero.png", 180f);

      npcs.Add(new Entity(_app, 800f, 400f, "./resources/hero.png", 180f));

    }

    public void Update() {
      _app.Clear(windowColor);
      framerate.Display();
      player.UpdateLogic();
      for(int i=0; i<npcs.Count; i++) {
        npcs[i].UpdateLogic();
      }
      _app.Draw(terrain.convexShape);
    }
  }
}