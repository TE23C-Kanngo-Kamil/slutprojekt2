using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

public abstract class Entity : GameObject
{
    public float Speed { get; protected set; }
    public float Radius { get; protected set; }

    protected Entity(Vector2 position, float speed, float radius) : base(position)
    {
        Speed = speed;
        Radius = radius;
    }

    public abstract void Update(float dt);
}