using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TMP_Text timeText;
    private float? startTime;

    private void Start()
    {
        PlayerController.StartMoving += OnStartMoving;
        PlayerController.OnCollided += OnCollision;
    }

    private void OnStartMoving()
    {
        startTime = Time.time;
    }

    private void OnCollision()
    {
        startTime = null;
    }

    private void Update()
    {
        if (startTime != null)
        {
            var elapsedTime = (float)(Time.time - startTime);
            timeText.text = elapsedTime.ToString("0.0");
        }
    }
}
