using System.Numerics;
using Raylib_cs;

public class Game
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private GameState _state = GameState.Menu;
    private Player _player = new Player(new Vector2(ScreenWidth / 2f, ScreenHeight / 2f));
    private bool _quit = false;

    public void Run()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Dungeon Quest");
        // Hindrar att ESC stänger hela spelet även utanför spelmenyn
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(60);

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
            if (_state == GameState.Playing || _state == GameState.Instructions)
            {
                _state = GameState.Menu;
            }
            else if (_state == GameState.Menu)
            {
                _quit = true;
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
                _player.Update(dt, ScreenWidth, ScreenHeight);
                break;
        }
    }

    private void UpdateMenu()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            // Återställ keybind
            _player = new Player(new Vector2(ScreenWidth / 2f, ScreenHeight / 2f));
            _state = GameState.Playing;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.I))
        {
            _state = GameState.Instructions;
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
        DrawCentered("Move: WASD", 170, 20);
        DrawCentered("Goal: Pick up a package and deliver it", 210, 20);
        DrawCentered("[ESC] - Back to menu", 260, 20);
    }

    private void DrawPlaying()
    {
        // Världen
        Raylib.DrawRectangleLines(20, 20, ScreenWidth - 40, ScreenHeight - 40, Color.DarkGray);

        // Spelaren
        _player.Draw();

        // UI
        Raylib.DrawText("Playing", 30, 28, 18, Color.White);
        Raylib.DrawText("WASD to move | ESC for menu", 30, ScreenHeight - 50, 18, Color.White);
    }

    private void DrawCentered(string text, int y, int fontSize)
    {
        int textWidth = Raylib.MeasureText(text, fontSize);
        int x = (ScreenWidth - textWidth) / 2;
        Raylib.DrawText(text, x, y, fontSize, Color.White);
    }
}