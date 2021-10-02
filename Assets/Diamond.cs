using UnityEngine;

public class Diamond : MonoBehaviour
{
    public SpriteRenderer sr;
    public int points;

    public void Init(int points)
    {
        this.points = points;
        sr.color = Color.magenta;
    }
}
