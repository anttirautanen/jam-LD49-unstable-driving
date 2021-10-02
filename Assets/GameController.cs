using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class GameController : MonoBehaviour
{
    public TMP_Text timeText;
    public RectTransform startViewPrefab;
    public RectTransform endViewPrefab;
    private float? startTime;
    private RectTransform startView;
    public static bool IsRunning = false;

    private void Start()
    {
        PlayerController.StartMoving += OnStartMoving;
        PlayerController.OnCollided += OnCollision;
        EndView.OnRestart += OnRestart;

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
        Debug.Assert(startTime != null, nameof(startTime) + " != null");

        var score = (float)(Time.time - startTime);

        IsRunning = false;
        timeText.text = "";

        var endViewGo = Instantiate(endViewPrefab, FindObjectOfType<Canvas>().transform);
        endViewGo.GetComponent<EndView>().Init(score);

        startTime = null;
    }

    private static void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (IsRunning)
        {
            Debug.Assert(startTime != null, nameof(startTime) + " != null");

            var elapsedTime = (float)(Time.time - startTime);
            timeText.text = elapsedTime.ToString("0.0");
        }
    }
}
