
using System.Collections.Generic;
using Godot;
public static class DirectionMapping
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private static readonly Dictionary<Direction, Vector2> directionMap = new Dictionary<Direction, Vector2>()
    {
        { Direction.Up, new Vector2(0,-1) },
        { Direction.Down, new Vector2(0,1) },
        { Direction.Left, new Vector2(-1,0) },
        { Direction.Right, new Vector2(1, 0) }
    };

    public static Vector2 GetDirectionVector(Direction direction)
    {
        return directionMap[direction];
    }
}

