using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Direction direction = Direction.Up;

    private void Update()
    {
        var nextDirection = GetDirection();
        if (nextDirection != null)
        {
            direction = (Direction)nextDirection;
        }

        transform.position = GetNextPosition();
    }

    private static Direction? GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return Direction.Up;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            return Direction.Left;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            return Direction.Down;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            return Direction.Right;
        }

        return null;
    }

    private Vector3 GetNextPosition()
    {
        var currentPosition = transform.position;
        return direction switch
        {
            Direction.Up => currentPosition + Vector3.up * Time.deltaTime,
            Direction.Left => currentPosition + Vector3.left * Time.deltaTime,
            Direction.Down => currentPosition + Vector3.down * Time.deltaTime,
            Direction.Right => currentPosition + Vector3.right * Time.deltaTime,
            _ => throw new Exception("Unknown direction " + direction)
        };
    }
}
