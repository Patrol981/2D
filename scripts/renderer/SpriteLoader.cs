using SFML.Graphics;

namespace _2D {
  public static class SpriteLoader {
    public static IntRect SetRect(int left, int top, int width, int height) {
      return new IntRect(left, top, width, height);
    }
  }
}