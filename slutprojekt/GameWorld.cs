using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

public class GameWorld
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    public Player Player { get; private set; }
    public Package Package { get; private set; }
    public DeliveryZone DeliveryZone { get; private set; }
    public List<Enemy> Enemies { get; private set; }

    public bool PackageDelivered { get; private set; }
    public string GameOverMessage { get; private set; }
    public bool IsGameOver { get; private set; }

    public GameWorld()
    {
        Reset();
    }

    public void Reset()
    {
        Player = new Player(new Vector2(ScreenWidth / 2f, ScreenHeight / 2f));
        Package = new Package(new Vector2(150, 150));
        DeliveryZone = new DeliveryZone(new Vector2(620, 440));

        Enemies = new List<Enemy>()
        {
            new ChaserEnemy(new Vector2(100, 500), Player),
            new ChaserEnemy(new Vector2(700, 100), Player)
        };

        PackageDelivered = false;
        IsGameOver = false;
        GameOverMessage = "Game Over!";
    }

    public void Update(float dt)
    {
        Player.Update(dt);

        foreach (Enemy enemy in Enemies)
        {
            enemy.Update(dt);

            bool touchingPlayer =
            Vector2.Distance(Player.Position, enemy.Position)
            <= Player.Radius + enemy.Radius;

            if (touchingPlayer)
            {
                GameOverMessage = "You were caught by an enemy!";
                IsGameOver = true;
                return;
            }
        }

        if (Package.IsActive)
        {
            bool touchingPackage = 
            Vector2.Distance(Player.Position, Package.Position)
            <= Player.Radius + Package.Radius;

            if (touchingPackage)
            {
                Package.Collect();
                Player.PickUpPackage();
            }
        }

        if (Player.HasPackage && !PackageDelivered)
        {
            bool insideDeliveryZone = Raylib.CheckCollisionCircleRec(
                Player.Position,
                Player.Radius,
                DeliveryZone.GetRectangle()
            );

            if (insideDeliveryZone)
            {
                Player.DeliverPackage();
                PackageDelivered = true;
                GameOverMessage = "Delivery complete!";
                IsGameOver = true;
            }
        }
    }

    public void Draw()
    {
        Raylib.DrawRectangleLines(20, 20, ScreenWidth - 40, ScreenHeight - 40, Color.DarkGray);

        Package.Draw();
        DeliveryZone.Draw();

        foreach (Enemy enemy in Enemies)
        {
            enemy.Draw();
        }

        Player.Draw();

        Raylib.DrawText("Playing", 30, 28, 18, Color.White);

        if (!Player.HasPackage && !PackageDelivered)
        {
            Raylib.DrawText("Objective: Pick up the package", 30, ScreenHeight - 50, 18, Color.White);
        }
        else if (Player.HasPackage && !PackageDelivered)
        {
            Raylib.DrawText("Objective: Deliver the package to EXIT", 30, ScreenHeight - 50, 18, Color.White);
        }
    }
}