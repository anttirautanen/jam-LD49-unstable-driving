using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("BackgroundAudio").Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
