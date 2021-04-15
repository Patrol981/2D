using SFML.Graphics;
using SFML;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace _2D {
  public class Framerate {
    Clock clock;
    float fps;
    Font font;
    Time prevTime;
    Time currentTime;

    private readonly RenderWindow _app;

    public Text fpsRate;

    public Framerate(RenderWindow _app) {
      clock = new Clock();
      prevTime = clock.ElapsedTime;
      fps = 0;
      font = new Font("./resources/fonts/PressStart2P-Regular.ttf");
      fpsRate = new Text();
      fpsRate.Font = font;
      fpsRate.DisplayedString = "FPS:";
      fpsRate.CharacterSize = 24;
      fpsRate.Position = new Vector2f(10,10);
      fpsRate.FillColor = Color.White;
      fpsRate.Style = Text.Styles.Regular;
      this._app = _app;
    }

    public void Tick() {
      currentTime = clock.ElapsedTime;
      fps = 1.0f / (currentTime.AsSeconds() - prevTime.AsSeconds());
      this.fpsRate.DisplayedString = $"FPS:{Math.Round(fps)}";
      prevTime = currentTime;
    }

    public void Display() {
      Tick();
      _app.Draw(fpsRate);
    }

  }
}