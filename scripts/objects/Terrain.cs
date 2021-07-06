using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;

namespace _2D {
  public struct VertexPosition {
    public float x;
    public float y;
  }
  public class Terrain {
    public ConvexShape convexShape;
    public List<VertexPosition> positions;
    public Terrain(Vector2u size) {
      this.convexShape = new ConvexShape();
      this.positions = new List<VertexPosition>();

      convexShape.SetPointCount(10);

      VertexPosition vertexPosition;

      // Begin
      convexShape.SetPoint(0, new Vector2f(0f, size.Y - 40));
      vertexPosition.x = convexShape.GetPoint(0).X;
      vertexPosition.y = convexShape.GetPoint(0).Y;
      positions.Add(vertexPosition);

      //Main
      uint pointStart = 1;
      uint points = size.X / (convexShape.GetPointCount() - 3);
      for(uint i=points; i<size.X; i+= points) {
        convexShape.SetPoint(pointStart, new Vector2f(i, Randomizer.ReturnRandomHeight((int)size.Y -100, (int)size.Y - 40)));

        vertexPosition.x = convexShape.GetPoint(pointStart).X;
        vertexPosition.y = convexShape.GetPoint(pointStart).Y;
        positions.Add(vertexPosition);

        pointStart++;
      }

      //End
      convexShape.SetPoint(convexShape.GetPointCount()-2, new Vector2f(size.X, size.Y));
      convexShape.SetPoint(convexShape.GetPointCount()-1, new Vector2f(0f, size.Y));

      vertexPosition.x = convexShape.GetPoint(convexShape.GetPointCount()-2).X;
      vertexPosition.y = convexShape.GetPoint(convexShape.GetPointCount()-2).Y;
      positions.Add(vertexPosition);

      vertexPosition.x = convexShape.GetPoint(convexShape.GetPointCount()-1).X;
      vertexPosition.y = convexShape.GetPoint(convexShape.GetPointCount()-1).Y;
      positions.Add(vertexPosition);
      // convexShape.SetPoint(4, new Vector2f())

      for(int i=0; i<positions.Count; i++) {
        Console.WriteLine($"{positions[i].x} {positions[i].y}");
      }
    }
  }

  internal abstract class Randomizer {
    public static uint ReturnRandomHeight(int min, int max) {
      Random random = new Random();
      return (uint) random.Next(min, max);
    }
  }
}
