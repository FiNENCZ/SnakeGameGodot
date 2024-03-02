using Godot;
using System;

public partial class main : Node2D
{

    private Vector2 _speed = new Vector2(400, 0);
    
    private Sprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _Process(double delta)
    {
        _sprite.Position += _speed * (float)delta;
        if(_sprite.Position.X > 1000)
        {
            _sprite.Position = new Vector2(-100, 350);
        }
    }

}
