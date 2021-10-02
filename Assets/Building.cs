using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Sprite> sprites;

    private void Start()
    {
        var sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
