using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0f, 100f)] public float speed = 10f;
    private Direction? direction = null;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var nextDirection = GetDirection();
        if (nextDirection != null)
        {
            direction = (Direction)nextDirection;
        }

        Move(direction);
    }

    private void Move(Direction? direction)
    {
        if (direction == null)
        {
            return;
        }

        rb.MovePosition(GetNextPosition());
    }

    private static Direction? GetDirection()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            return Direction.Up;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            return Direction.Left;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            return Direction.Down;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
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
            Direction.Up => currentPosition + Vector3.up * speed * Time.deltaTime,
            Direction.Left => currentPosition + Vector3.left * speed * Time.deltaTime,
            Direction.Down => currentPosition + Vector3.down * speed * Time.deltaTime,
            Direction.Right => currentPosition + Vector3.right * speed * Time.deltaTime,
            _ => throw new Exception("Unknown direction " + direction)
        };
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            print("COLLIDED WITH A BUILDING - GAME OVER!");
            SceneManager.LoadScene(0);
        }
    }
}
