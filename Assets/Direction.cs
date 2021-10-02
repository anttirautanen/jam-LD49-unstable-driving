using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public static class DirectionUtils
{
    public static Dictionary<Direction, Vector2Int> DirectionToVector { get; } = new Dictionary<Direction, Vector2Int>
    {
        { Direction.Up, Vector2Int.up },
        { Direction.Left, Vector2Int.left },
        { Direction.Down, Vector2Int.down },
        { Direction.Right, Vector2Int.right }
    };

    public static bool IsOpposite(Direction direction1, Direction direction2)
    {
        switch (direction1)
        {
            case Direction.Up when direction2 == Direction.Down:
            case Direction.Left when direction2 == Direction.Right:
            case Direction.Down when direction2 == Direction.Up:
            case Direction.Right when direction2 == Direction.Left:
                return true;
            default:
                return false;
        }
    }

    public static Dictionary<Direction, Vector3Int> DirectionToAngle { get; } = new Dictionary<Direction, Vector3Int>
    {
        { Direction.Up, new Vector3Int(0, 0, 0) },
        { Direction.Left, new Vector3Int(0, 0, 90) },
        { Direction.Down, new Vector3Int(0, 0, 180) },
        { Direction.Right, new Vector3Int(0, 0, 270) }
    };
}
