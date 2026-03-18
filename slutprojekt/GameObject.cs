using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

public abstract class GameObject
{
    public Vector2 Position { get; protected set; }
    public bool IsActive { get; protected set; } = true;

    protected GameObject(Vector2 position)
    {
        Position = position;
    }

    public abstract void Draw();
}