using Godot;
using System;

public partial class Score : Node2D
{
    private Vector2 screenSize;

    public override void _Ready()
    {
        screenSize = GetViewport().GetVisibleRect().Size;

        // Nastavení pozice pro $Apple
        Node2D apple = GetNode<Node2D>("Apple");
        if (apple != null)
        {
            apple.Position = new Vector2(screenSize.X - 60, screenSize.Y - 40);
        }

        // Nastavení pozice pro $ScoreText
        Label scoreText = GetNode<ScoreLabel>("ScoreLabel");
        if (scoreText != null)
        {
            scoreText.Position = new Vector2(screenSize.X - 40, screenSize.Y - 50);
        }
    }
    public void UpdateScore(int snakeLength) {

        GetNode<ScoreLabel>("ScoreLabel").Text = snakeLength.ToString();
    }
}
