using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Worm
{
    public readonly List<Vector2Int> Cells;

    public Worm(Vector2Int startPosition, Direction startDirection, int length)
    {
        Cells = new List<Vector2Int>
        {
            startPosition,
            startPosition + DirectionUtils.DirectionToVector[startDirection]
        };

        var direction = GetRandomDirection(startDirection);
        for (var i = 0; i < length - 2; ++i)
        {
            Cells.Add(Cells.Last() + DirectionUtils.DirectionToVector[direction]);
            direction = GetRandomDirection(direction);
        }
    }

    private static Direction GetRandomDirection(Direction previousDirection)
    {
        var random = Random.Range(0, 4);
        var nextDirection = random switch
        {
            0 => Direction.Up,
            1 => Direction.Left,
            2 => Direction.Down,
            _ => Direction.Right
        };

        if (DirectionUtils.IsOpposite(previousDirection, nextDirection))
        {
            return previousDirection;
        }

        return nextDirection;
    }
}
