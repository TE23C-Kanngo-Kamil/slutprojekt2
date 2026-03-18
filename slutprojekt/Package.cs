using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

public class Package : GameObject
{
    public float Radius { get; } = 12f;
    public bool IsCollected { get; private set; } = false;

    public Package(Vector2 position) : base(position){}

    public void Collect()
    {
        IsCollected = true;
        IsActive = false;
    }

    public override void Draw()
    {
        if (!IsActive)
        {
            return;
        }

        Raylib.DrawCircleV(Position, Radius, Color.Gold);
        Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, Radius, Color.White);
    }
}