using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace _2D {
	class Program {
		static void OnClose(object sender, EventArgs e) {
			RenderWindow window = (RenderWindow)sender;
			window.Close();
		}
    static void Main(string[] args) {
      RenderWindow app = new RenderWindow(new VideoMode(800,600), "SFML TEST");
			app.Closed += new EventHandler(OnClose);
			
			Color windowColor = new Color(10,10,10);

			Texture texture = new Texture("./resources/adlero.JPG");
			texture.Smooth = false;
			texture.Repeated = false;
			
			Sprite sprite = new Sprite(texture);

			sprite.Position = new Vector2f(400f,200f);

			while(app.IsOpen) {
				app.DispatchEvents();
				app.Clear(windowColor);
				texture.Update(app);
				app.Draw(sprite);
				sprite.Rotation += .03f;
				app.Display();
			}
    }
  }
}
