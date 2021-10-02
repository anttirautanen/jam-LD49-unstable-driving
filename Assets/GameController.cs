using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMP_Text timeText;
    public RectTransform startViewPrefab;
    public RectTransform endViewPrefab;
    public static bool IsRunning = false;
    private float? startTime;
    private RectTransform startView;
    private int extraPoints;

    private void Start()
    {
        PlayerController.StartMoving += OnStartMoving;
        PlayerController.OnCollidedWithBuilding += OnCollision;
        PlayerController.OnDiamondCollected += OnDiamondHit;
        EndView.OnRestart += OnRestart;

        extraPoints = 0;
        startView = Instantiate(startViewPrefab, FindObjectOfType<Canvas>().transform);
    }

    private void OnStartMoving()
    {
        if (startView)
        {
            Destroy(startView.gameObject);
        }

        startTime = Time.time;
        IsRunning = true;
    }

    private void OnCollision()
    {
        if (IsRunning)
        {
            IsRunning = false;
            timeText.text = "";

            var endViewGo = Instantiate(endViewPrefab, FindObjectOfType<Canvas>().transform);
            endViewGo.GetComponent<EndView>().Init(GetScore());

            startTime = null;
        }
    }

    private void OnDiamondHit(int points)
    {
        extraPoints += points;
    }

    private static void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (IsRunning)
        {
            timeText.text = GetScore().ToString();
        }
    }

    private int GetScore()
    {
        if (startTime == null)
        {
            return 0;
        }

        var elapsedTime = (float)(Time.time - startTime);
        return Mathf.FloorToInt(elapsedTime * 10) + extraPoints;
    }
}
