using System;
using TMPro;
using UnityEngine;

public class EndView : MonoBehaviour
{
    public static event Action OnRestart;
    public TMP_Text scoreText;

    public void Init(float score)
    {
        scoreText.text = $"Score: {score * 10:0}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnRestart?.Invoke();
            Destroy(gameObject);
        }
    }
}
