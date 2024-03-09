
using System.Collections.Generic;
using Godot;
public static class TextureMapping
{
    public enum Texture
    {
        HEAD_UP,
        HEAD_DOWN,
        HEAD_LEFT,
        HEAD_RIGHT,
        TAIL_UP,
        TAIL_DOWN,
        TAIL_LEFT,
        TAIL_RIGHT,
        BODY_VERTICAL,
        BODY_HORIZONTAL,
        BODY_LEFT_UP,
        BODY_LEFT_DOWN,
        BODY_RIGHT_UP,
        BODY_RIGHT_DOWN,
        APPLE
    }

    private static readonly Dictionary<Texture, Vector2I> textureMap = new Dictionary<Texture, Vector2I>()
    {
        { Texture.HEAD_UP, new Vector2I(2, 1) },
        { Texture.HEAD_DOWN, new Vector2I(3, 0) },
        { Texture.HEAD_LEFT, new Vector2I(3, 1) },
        { Texture.HEAD_RIGHT, new Vector2I(2, 0) },
        { Texture.TAIL_UP, new Vector2I(1, 1) },
        { Texture.TAIL_DOWN, new Vector2I(0, 1) },
        { Texture.TAIL_LEFT, new Vector2I(1, 0) },
        { Texture.TAIL_RIGHT, new Vector2I(0, 0) },
        { Texture.BODY_VERTICAL, new Vector2I(4, 1) },
        { Texture.BODY_HORIZONTAL, new Vector2I(4, 0) },
        { Texture.BODY_LEFT_UP, new Vector2I(5, 0) },
        { Texture.BODY_LEFT_DOWN, new Vector2I(5, 1) },
        { Texture.BODY_RIGHT_UP, new Vector2I(6, 0) },
        { Texture.BODY_RIGHT_DOWN, new Vector2I(6, 1) },
        { Texture.APPLE, new Vector2I(0, 0) },
    };

    public static Vector2I GetTextureVector(Texture direction)
    {
        return textureMap[direction];
    }
}

