using UnityEngine;

public class Cloud : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.value * 360));
        transform.localScale = Vector3.one * (Random.value + 1);
        Destroy(gameObject, 2);
    }
}
