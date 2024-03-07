using Godot;
using System;
using System.Collections.Generic;

public partial class main : Node2D
{

        [Export]
        public PackedScene SnakeScene { get; set; }

        private const bool GameStarted = false;
        private const int Cells = 20;
        private const int CellSize = 50;
        
        private const int InitialSnakeLength = 5;
        private const int MillisecondsPerMove = 100;

        private readonly Random _random = new Random();
        private readonly ConsoleColor _headColor = ConsoleColor.Red;
        private readonly ConsoleColor _bodyColor = ConsoleColor.Green;
        private readonly ConsoleColor _berryColor = ConsoleColor.Cyan;
        private readonly List<int> _xPositions = new List<int>();
        private readonly List<int> _yPositions = new List<int>();

        private int _score = InitialSnakeLength;
        private bool _gameOver;
        private int _berryX;
        private int _berryY;

    public override void _Ready()
    {
        GD.Print("Script is ready!");
    }

    public override void _Process(double delta)
    {
        GD.Print("Script is ready!");
    }

}
