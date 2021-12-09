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
      // convexShape.SetPointCount(size.X);

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

       //float noise = Noise2D(255,255);
       //Console.WriteLine(noise);

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

    float Noise2D(float x, float y) {
      int x0 = (int)x;
      int x1 = x0 + 1;
      int y0 = (int)y;
      int y1 = y0 + 1;

      float sX = x - (float)x0;
      float sY = y - (float)y0;

      float n0, n1, ix0, ix1, value;
      n0 = DotGridGradient(x0, y0, x, y);
      n1 = DotGridGradient(x1, y0, x, y);
      ix0 = Interpolate(n0, n1, sX);

      n0 = DotGridGradient(x0, y1, x, y);
      n1 = DotGridGradient(x1, y1, x, y);
      ix1 = Interpolate(n0, n1, sY);

      value = Interpolate(ix0, ix1, sY);
      return value;
    }

    float DotGridGradient(int ix, int iy, float x, float y) {
      Vector2f gradient = RandomGradient(ix, iy);

      float dx = x - (float)ix;
      float dy = y - (float)iy;

      return (dx*gradient.X + dy * gradient.Y);
    }

    Vector2f RandomGradient(int ix, int iy) {
      float random = 2920f * MathF.Sin(ix * 21942f + iy * 171324f + 8912f) * MathF.Cos(ix * 23157f * iy * 217832f + 9758f);
      return new Vector2f(MathF.Cos(random), MathF.Sin(random));
    }

    float Interpolate(float a0, float a1, float w) {
      return (a1 - a0) * w + a0;
    }

  }

  internal abstract class Randomizer {
    public static uint ReturnRandomHeight(int min, int max) {
      Random random = new Random();
      return (uint) random.Next(min, max);
    }
  }
}
