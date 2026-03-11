using System.Numerics;
using Raylib_cs;

public class DeliveryZone : GameObject
{
    public float Width { get; } = 80f;
    public float Height { get; } = 80f;

    public DeliveryZone(Vector2 position) : base(position){}

    public Rectangle GetRectangle()
    {
        return new Rectangle(Position.X, Position.Y, Width, Height);
    }

    public override void Draw()
    {
        Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, (int)Width, (int)Height, Color.Green);
        Raylib.DrawText("EXIT", (int)Position.X + 14, (int)Position.Y + 32, 20, Color.Green);
    }
}