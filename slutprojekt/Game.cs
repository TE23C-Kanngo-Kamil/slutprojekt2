using System.Numerics;
using Raylib_cs;

public class Game
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private GameState _state = GameState.Menu;
    private Player _player = new Player(new Vector2(ScreenWidth / 2f, ScreenHeight / 2f));

    public void Run()
    {
        
    }

}