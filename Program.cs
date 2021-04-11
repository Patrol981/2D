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
			VideoMode desktopResolution = VideoMode.DesktopMode;
      RenderWindow app = new RenderWindow(desktopResolution, "2D Game");
			app.Closed += new EventHandler(OnClose);

			Renderer renderer = new Renderer(app);

			while(app.IsOpen) {
				app.DispatchEvents();
				renderer.Update();
				app.Display();
			}
    }
  }
}
