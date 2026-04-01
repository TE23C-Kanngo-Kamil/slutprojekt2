using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

public class Game
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private GameState _state = GameState.Menu;
    private Player _player = new Player(new Vector2(ScreenWidth / 2f, ScreenHeight / 2f));
    private bool _quit = false;

    private Package _package = new Package(new Vector2(150, 150));
    private DeliveryZone _deliveryZone = new DeliveryZone(new Vector2(620, 440));
    private bool _packageDelivered = false;

    private List<Enemy> _enemies = new List<Enemy>();
    private string _gameOverMessage = "Game Over!";

    public void Run()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "DUNGEON QUEST");
        // Hindrar att ESC stänger hela spelet även utanför spelmenyn
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(60);

        ResetGame();

        while (!Raylib.WindowShouldClose() && !_quit)
        {
            float dt = Raylib.GetFrameTime();

            Update(dt);
            Draw();
        }

        Raylib.CloseWindow();
    }

    private void Update(float dt)
    {
        // Backa keybind
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            if (_state == GameState.Menu)
            {
                _quit = true;
            }
            else
            {
                _state = GameState.Menu;
            }
        }

        switch (_state)
        {
            case GameState.Menu:
                UpdateMenu();
                break;

            case GameState.Instructions:
                break;

            case GameState.Playing:
                UpdatePlaying(dt);
                break;

            case GameState.GameOver:
                UpdateGameOver();
                break;
        }
    }

    private void UpdateMenu()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            ResetGame();
            _state = GameState.Playing;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.I))
        {
            _state = GameState.Instructions;
        }
    }

    private void UpdatePlaying(float dt)
    {
        _player.Update(dt);

        foreach (Enemy enemy in _enemies)
        {
            enemy.Update(dt);

            bool touchingPlayer = Vector2.Distance(_player.Position, enemy.Position) <= _player.Radius + enemy.Radius;
            if (touchingPlayer)
            {
                _gameOverMessage = "You were caught by an enemy!";
                _state = GameState.GameOver;
                return;
            }
        }

        if (_package.IsActive)
        {
            bool touchingPackage = Vector2.Distance(_player.Position, _package.Position) <= _player.Radius + _package.Radius;

            if (touchingPackage)
            {
                _package.Collect();
                _player.PickUpPackage();
            }
        }

        if (_player.HasPackage && !_packageDelivered)
        {
            bool insideDeliveryZone = Raylib.CheckCollisionCircleRec(
                _player.Position,
                _player.Radius,
                _deliveryZone.GetRectangle()
            );

            if (insideDeliveryZone)
            {
                _player.DeliverPackage();
                _packageDelivered = true;
                _gameOverMessage = "Delivery Complete!";
                _state = GameState.GameOver;
            }
        }
    }

    private void UpdateGameOver()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            ResetGame();
            _state = GameState.Playing;
        }
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        switch (_state)
        {
            case GameState.Menu:
                DrawMenu();
                break;

            case GameState.Instructions:
                DrawInstructions();
                break;

            case GameState.Playing:
                DrawPlaying();
                break;

            case GameState.GameOver:
                DrawGameOver();
                break;
        }

        Raylib.EndDrawing();
    }

    private void DrawMenu()
    {
        DrawCentered("DUNGEON QUEST", 80, 30);
        DrawCentered("[ENTER] - Start", 180, 20);
        DrawCentered("[I] - Instructions", 220, 20);
        DrawCentered("[ESC] - Quit", 260, 20);
    }

    private void DrawInstructions()
    {
        DrawCentered("INSTRUCTIONS", 80, 30);
        DrawCentered("Move: WASD", 150, 20);
        DrawCentered("Avoid enemies", 190, 20);
        DrawCentered("Pick up the package and deliver it to EXIT", 230, 20);
        DrawCentered("[ESC] - Back to menu", 280, 20);
    }

    private void DrawPlaying()
    {
        // Världen
        Raylib.DrawRectangleLines(20, 20, ScreenWidth - 40, ScreenHeight - 40, Color.DarkGray);

        // Objekt
        _package.Draw();
        _deliveryZone.Draw();

        foreach (Enemy enemy in _enemies)
        {
            enemy.Draw();
        }

        _player.Draw();

        Raylib.DrawText("Playing", 30, 28, 18, Color.White);

        if (!_player.HasPackage && !_packageDelivered)
        {
            Raylib.DrawText("Objective: Pick up the package", 30, ScreenHeight - 50, 18, Color.White);
        }
        else if (_player.HasPackage && !_packageDelivered)
        {
            Raylib.DrawText("Objective: Deliver the package to EXIT", 30, ScreenHeight - 50, 18, Color.White);
        }
    }

    private void DrawGameOver()
    {
        DrawCentered(_gameOverMessage, 180, 32);
        DrawCentered("[ENTER] - Play again", 280, 20);
        DrawCentered("[ESC] - Back to menu", 320, 20);
    }
    private void ResetGame()
    {
        _player = new Player(new Vector2(ScreenWidth / 2f, ScreenHeight / 2f));
        _package = new Package(new Vector2(150, 150));
        _deliveryZone = new DeliveryZone(new Vector2(620, 440));
        _packageDelivered = false;

        _enemies = new List<Enemy>()
        {
            new ChaserEnemy(new Vector2(100, 500), _player),
            new ChaserEnemy(new Vector2(700, 100), _player)
        };

        _gameOverMessage = "Game Over!";    }

    private void DrawCentered(string text, int y, int fontSize)
    {
        int textWidth = Raylib.MeasureText(text, fontSize);
        int x = (ScreenWidth - textWidth) / 2;
        Raylib.DrawText(text, x, y, fontSize, Color.White);
    }
}