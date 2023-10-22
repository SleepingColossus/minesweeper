using Godot;
using System;

public static class Vector2Extensions
{
    public static Tuple<int, int> ToIntVector(this Vector2 vector)
    {
        return new Tuple<int, int>((int)vector.x, (int)vector.y);
    }
}
