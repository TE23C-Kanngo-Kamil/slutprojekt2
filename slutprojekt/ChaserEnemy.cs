using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

public class ChaserEnemy : Enemy
{
    private Player _player;

    public ChaserEnemy(Vector2 position, Player player) : base(position, 120f, 14f)
    {
        _player = player;
    }

    public override void Update(float dt)
    {
        Vector2 direction = _player.Position - Position;

        if (direction != Vector2.Zero)
        {
            direction = Vector2.Normalize(direction);
            Position += direction * Speed * dt;
        }
    }

    public override void Draw()
    {
        Raylib.DrawCircleV(Position, Radius, Color.Red);
        Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, Radius, Color.White);
    }
}