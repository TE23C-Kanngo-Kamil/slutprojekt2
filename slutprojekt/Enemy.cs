using System.Numerics;
using System.Reflection.Metadata;
using Raylib_cs;

namespace Slutprojekt;

public abstract class Enemy : Entity
{
    protected Enemy(Vector2 position, float speed, float radius) : base(position, speed, radius){}
}