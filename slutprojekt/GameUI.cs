using Raylib_cs;

namespace Slutprojekt;

public static class GameUI
{
    public static void DrawMenu(int ScreenWidth)
    {
        DrawCentered("DUNGEON QUEST", 80, 30, ScreenWidth);
        DrawCentered("[ENTER] - Start", 180, 20, ScreenWidth);
        DrawCentered("[I] - Instructions", 220, 20, ScreenWidth);
        DrawCentered("[ESC] - Quit", 260, 20, ScreenWidth);
    }

    public static void DrawInstructions(int ScreenWidth)
    {
        DrawCentered("INSTRUCTIONS", 80, 30, ScreenWidth);
        DrawCentered("Move: WASD", 150, 20, ScreenWidth);
        DrawCentered("Avoid enemies", 190, 20, ScreenWidth);
        DrawCentered("Pick up the package and deliver it to EXIT", 230, 20, ScreenWidth);
        DrawCentered("[ESC] - Back to menu", 280, 20, ScreenWidth);
    }

    public static void DrawGameOver(string message, int ScreenWidth)
    {
        DrawCentered(message, 180, 32, ScreenWidth);
        DrawCentered("[ENTER] - Play again", 280, 20, ScreenWidth);
        DrawCentered("[ESC] - Back to menu", 320, 20, ScreenWidth);
    }

    private static void DrawCentered(string text, int y, int fontSize, int ScreenWidth)
    {
        int textWidth = Raylib.MeasureText(text, fontSize);
        int x = (ScreenWidth - textWidth) / 2;
        Raylib.DrawText(text, x, y, fontSize, Color.White);
    }
}