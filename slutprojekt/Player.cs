using System.Numerics;
using Raylib_cs;

public class Player
{
    public Vector2 Position { get; private set; }
    public float Speed { get; set; } = 240f;
    public float Radius { get; set; } = 16f;
    public bool HasPackage { get; private set; } = false;

    public Player(Vector2 startPosition)
    {
        Position = startPosition;
    }

    public void Update(float dt, int screenWidth, int screenHeight)
    {
        Vector2 move = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.W)) move.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) move.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A)) move.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) move.X += 1;

        if (move != Vector2.Zero)
        {
            move = Vector2.Normalize(move);
            Position += move * Speed * dt;
        }

        // Håll spelaren inom fönstret
        float minX = 20 + Radius;
        float minY = 20 + Radius;
        float maxX = screenWidth - 20 - Radius;
        float maxY = screenHeight - 20 - Radius;

        Position = new Vector2(
            Clamp(Position.X, minX, maxX),
            Clamp(Position.Y, minY, maxY)
        );
    }

    public void PickUpPackage()
    {
        HasPackage = true;
    }

    public void DeliverPackage()
    {
        HasPackage = true;
    }

    public void Draw()
    {
        Raylib.DrawCircleV(Position, Radius, Color.SkyBlue);
        Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, Radius, Color.White);

        if (HasPackage)
        {
            Raylib.DrawCircle((int)Position.X, (int)(Position.Y - 24), 6, Color.Gold);
        }
    }

    private static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}