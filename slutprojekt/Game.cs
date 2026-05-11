using Raylib_cs;

namespace Slutprojekt;

public class Game
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private GameState _state = GameState.Menu;
    private GameWorld _world = new GameWorld();
    private bool _quit = false;

    public void Run()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "DUNGEON QUEST");
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
        // Stäng spelet om vi är i menyn, annars gå tillbaka till menyn
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
                _world.Update(dt);

                 if (_world.IsGameOver)
                {
                    _state = GameState.GameOver;
                }
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
            _world.Reset();
            _state = GameState.Playing;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.I))
        {
            _state = GameState.Instructions;
        }
    }

    private void UpdateGameOver()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            _world.Reset();
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
                GameUI.DrawMenu(ScreenWidth);
                break;

            case GameState.Instructions:
                GameUI.DrawInstructions(ScreenWidth);
                break;

            case GameState.Playing:
                _world.Draw();
                break;

            case GameState.GameOver:
                GameUI.DrawGameOver(_world.GameOverMessage, ScreenWidth);
                break;
        }

        Raylib.EndDrawing();
    }
}