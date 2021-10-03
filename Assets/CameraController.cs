using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        PlayerController.OnCollidedWithBuilding += OnCollision;
    }

    private void Update()
    {
        var playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
    }

    private static void OnCollision()
    {
        if (Camera.main != null)
        {
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.value * 10 - 5));
        }
    }
}
