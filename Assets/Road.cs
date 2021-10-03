using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public List<Sprite> roadSprites;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = GetSprite();
    }

    private Sprite GetSprite()
    {
        var random = Random.value;
        if (random > 0.90)
        {
            var roadSpriteIndex = Random.Range(1, roadSprites.Count);
            return roadSprites[roadSpriteIndex];
        }

        return roadSprites[0];
    }
}
