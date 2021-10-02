using UnityEngine;

public class Diamond : MonoBehaviour
{
    public SpriteRenderer sr;
    public int points;

    public void Init(int points)
    {
        this.points = points;
        if (points <= 100)
        {
            sr.color = new Color(0.04f, 0.55f, 0.04f);
            transform.localScale = Vector3.one * 0.8f;
        }
        else if (points <= 200)
        {
            sr.color = new Color(0.64f, 0.58f, 0.06f);
            transform.localScale = Vector3.one;
        }
        else if (points <= 300)
        {
            sr.color = new Color(0.55f, 0.04f, 0.32f);
            transform.localScale = Vector3.one * 1.5f;
        }
        else
        {
            sr.color = new Color(0.73f, 0.81f, 0.77f);
            transform.localScale = Vector3.one * 2;
        }
    }
}
