using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using static DirectionMapping;
using static TextureMapping;

public partial class MainGame : Node
{
    private const int SNAKE_SOURCEID = 0;
    private const int APPLE_SOURCEID = 1;
    private List<Vector2> snakeBody = new List<Vector2>()
    {
        new Vector2(5, 10),
        new Vector2(4, 10),
        new Vector2(3, 10)
    };

    private Vector2 _currentDirection = GetDirectionVector(Direction.Right);
    
    private Vector2 berry;
    private bool berryEaten = false;

    private int score = 0;
    private readonly Random _random = new Random();

    private readonly double _timerWaitTime = 0.4;



    public override void _Ready()
    {
        GetNode<Timer>("SnakeTick").WaitTime = _timerWaitTime;
        GenerateNewBerry();
        DrawBerry();
        DrawSnake();
    }

    public void OnSnakeTickTimeout()
    {
        MoveSnake();
        CheckCollision();
        DrawBerry();
        DrawSnake();
    }

    public void GenerateNewBerry() {
        int x = _random.Next(0, 20);
        int y = _random.Next(0, 20);
        berry =  new Vector2(x,y);
    }

    public void DrawBerry() {
        SetCellWithCoords(berry, APPLE_SOURCEID, GetTextureVector(TextureMapping.Texture.APPLE));
    }

    public Direction GetRelativeDirection(Vector2 firstBlock, Vector2 secondBlock)
    {
        Vector2 blockRelation =  firstBlock - secondBlock;

        switch (blockRelation)
        {
            case var rel when rel == GetDirectionVector(Direction.Right):
                return Direction.Right;
            case var rel when rel == GetDirectionVector(Direction.Left):
                return Direction.Left;
            case var rel when rel == GetDirectionVector(Direction.Down):
                return Direction.Down;
            default:
                return Direction.Up;
        }
    }
    private void DrawSnake()
    {
        for (int blockIndex = 0; blockIndex < snakeBody.Count; blockIndex++)
        {
            Vector2 bodyPart = snakeBody[blockIndex];

            if (blockIndex == 0) {
                DrawSnakeHead(bodyPart);
            }
            else if (blockIndex == snakeBody.Count - 1) {
                DrawSnakeTail(bodyPart);
            }
            else {
                DrawSnakeBody(bodyPart, blockIndex);
            }
        }
    }

    private void DrawSnakeHead(Vector2 bodyPart)
    {
        Direction headDirection = GetRelativeDirection(snakeBody[0], snakeBody[1]);
        switch (headDirection)
        {
            case Direction.Right:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.HEAD_RIGHT));
                break;
            case Direction.Left:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.HEAD_LEFT));
                break;
            case Direction.Up:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.HEAD_UP));
                break;
            case Direction.Down:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.HEAD_DOWN));
                break;
        }
    }

    private void DrawSnakeTail(Vector2 bodyPart)
    {
        Direction tailDirection = GetRelativeDirection(snakeBody[snakeBody.Count - 2], snakeBody[snakeBody.Count - 1]);

        switch (tailDirection)
        {
            case Direction.Right:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.TAIL_RIGHT));
                break;
            case Direction.Left:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.TAIL_LEFT));
                break;
            case Direction.Up:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.TAIL_UP));
                break;
            case Direction.Down:
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.TAIL_DOWN));
                break;
        }
    }

    private void DrawSnakeBody(Vector2 bodyPart, int blockIndex)
    {
        Vector2 previousBlock = snakeBody[blockIndex + 1] - bodyPart;
        Vector2 nextBlock = snakeBody[blockIndex - 1] - bodyPart;

        if ((int)previousBlock.X == (int)nextBlock.X) {
            SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.BODY_VERTICAL)); // Vertical body segment
        }
        else if ((int)previousBlock.Y == (int)nextBlock.Y) {
            SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.BODY_HORIZONTAL)); // Horizontal body segment
        }
        else {
            // Diagonal body segment
            if (((int)previousBlock.X == -1 && (int)nextBlock.Y == -1) || ((int)nextBlock.X == -1 && (int)previousBlock.Y == -1)) {
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.BODY_RIGHT_DOWN)); // Right-down
            }
            else if (((int)previousBlock.X == -1 && (int)nextBlock.Y == 1) || ((int)nextBlock.X == -1 && (int)previousBlock.Y == 1)) {
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.BODY_RIGHT_UP)); // Right-up
            }
            else if (((int)previousBlock.X == 1 && (int)nextBlock.Y == -1) || ((int)nextBlock.X == 1 && (int)previousBlock.Y == -1)) {
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.BODY_LEFT_DOWN)); // Left-down
            }
            else if (((int)previousBlock.X == 1 && (int)nextBlock.Y == 1) || ((int)nextBlock.X == 1 && (int)previousBlock.Y == 1)) {
                SetCellWithCoords(bodyPart, SNAKE_SOURCEID, GetTextureVector(TextureMapping.Texture.BODY_LEFT_UP)); // Left-up
            }
        }
    }
    public void DeleteTiles(int id) {
        SnakeApple snakeApple = GetNode<SnakeApple>("SnakeApple");
        Array<Vector2I> cellsArray = snakeApple.GetUsedCellsById(id);
    
        foreach (Vector2I cell in cellsArray)
        {
            snakeApple.SetCell(layer: 0, coords: cell, sourceId: -1);
        }
    }

    private void SetCellWithCoords(Vector2 coords, int sourceID, Vector2I atlasCoords)
    {
        GetNode<SnakeApple>("SnakeApple").SetCell(layer: 0, coords: new Vector2I((int)coords.X, (int)coords.Y), sourceId: sourceID, atlasCoords: atlasCoords);
    }


    public override void _Input(InputEvent @event)
	{
		if(@event.IsAction("ui_down") && _currentDirection != GetDirectionVector(Direction.Up)) {
            _currentDirection = GetDirectionVector(Direction.Down);
			return;
		}
		if(@event.IsAction("ui_up") && _currentDirection != GetDirectionVector(Direction.Down)) {
            _currentDirection = GetDirectionVector(Direction.Up);
			return;
		}
		if(@event.IsAction("ui_right") && _currentDirection != GetDirectionVector(Direction.Left)) {
            _currentDirection = GetDirectionVector(Direction.Right);
			return;
		}
		if(@event.IsAction("ui_left") && _currentDirection != GetDirectionVector(Direction.Right)) {
            _currentDirection = GetDirectionVector(Direction.Left);
			return;
		}
	}
    public void MoveSnake() {
        DeleteTiles(SNAKE_SOURCEID);

        List<Vector2> bodyCopy = new List<Vector2>(snakeBody);
        Vector2 newHead = bodyCopy[0] + _currentDirection;
        bodyCopy.Insert(0, newHead);

        if (!berryEaten) {
            bodyCopy.RemoveAt(bodyCopy.Count - 1);
        }
        berryEaten = false;
        snakeBody = bodyCopy;
    }

    public void CheckCollision(){
        Vector2 head = snakeBody[0];

        // Border collision
        if ((int)head.X > 19 || (int)head.X < 0 || (int)head.Y > 19 || (int)head.Y < 0 ){
            Reset();
        }
        // Berry collision
        if (berry == snakeBody[0]) {
            berryEaten = true;
            score++;
            GetTree().CallGroup("ScoreGroup", "UpdateScore", score);
            GenerateNewBerry();
        }

        // Head-body collision
        foreach (Vector2 block in snakeBody.GetRange(1, snakeBody.Count - 1))
        {
            if (block == head) {
                Reset();
                break;
            }
        }
    }

    public void Reset() {
        snakeBody = new List<Vector2>()
        {
            new Vector2(5, 10),
            new Vector2(4, 10),
            new Vector2(3, 10)
        };

        _currentDirection = new Vector2(1,0);
        score = 0;
        GetTree().CallGroup("ScoreGroup", "UpdateScore", score);

    }
}
