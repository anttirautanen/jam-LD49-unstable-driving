using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static event Action StartMoving;
    public static event Action OnCollidedWithBuilding;
    public static event Action<int> OnDiamondCollected;

    [Range(0f, 100f)] public float speed = 10f;
    public AudioSource coinAudio;
    public AudioSource crashAudio;
    public AudioSource engineAudio;
    public AudioSource engineStartAudio;
    public Transform cloud;
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
            if (direction == null)
            {
                StartMoving?.Invoke();
                engineAudio.Play();
                engineStartAudio.Play();
            }

            direction = (Direction)nextDirection;
        }
    }


    private void FixedUpdate()
    {
        Move(direction);
    }

    private void Move(Direction? direction)
    {
        if (direction == null || !GameController.IsRunning)
        {
            return;
        }

        rb.MovePosition(GetNextPosition());
        rb.SetRotation(Quaternion.Euler(DirectionUtils.DirectionToAngle[(Direction)direction]));
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
            Direction.Up => currentPosition + Vector3.up * speed * Time.fixedDeltaTime,
            Direction.Left => currentPosition + Vector3.left * speed * Time.fixedDeltaTime,
            Direction.Down => currentPosition + Vector3.down * speed * Time.fixedDeltaTime,
            Direction.Right => currentPosition + Vector3.right * speed * Time.fixedDeltaTime,
            _ => throw new Exception("Unknown direction " + direction)
        };
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Building") && GameController.IsRunning)
        {
            OnCollidedWithBuilding?.Invoke();
            crashAudio.Play();
            engineAudio.Stop();
            engineStartAudio.Stop();
            StartCoroutine(nameof(ProduceSmoke));
            const int baseForce = 15;
            rb.AddForce(new Vector2(Random.value * baseForce + baseForce, Random.value * baseForce + baseForce));
            rb.AddTorque(Random.value * baseForce + baseForce);
        }
    }

    private IEnumerator ProduceSmoke()
    {
        Instantiate(cloud, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(cloud, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(cloud, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Diamond") && GameController.IsRunning)
        {
            OnDiamondCollected?.Invoke(other.GetComponent<Diamond>().points);
            coinAudio.Play();
        }
    }
}
