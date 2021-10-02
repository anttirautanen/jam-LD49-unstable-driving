using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(0f, 100f)] public float speed = 10f;
    private Direction direction = Direction.Up;
    private Rigidbody2D rb;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
        }

        if (!isMoving)
        {
            return;
        }

        var nextDirection = GetDirection();
        if (nextDirection != null)
        {
            direction = (Direction)nextDirection;
        }

        rb.MovePosition(GetNextPosition());
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
