using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class BackgroundImage : MonoBehaviour
{
    private void Start()
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");

        var t = transform;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var cam = Camera.main;
        var cameraHeight = cam.orthographicSize * 2;
        var cameraSize = new Vector2(cam.aspect * cameraHeight, cameraHeight);
        var spriteSize = spriteRenderer.sprite.bounds.size;
        var scale = t.localScale;

        scale *= cameraSize.x >= cameraSize.y
            ? cameraSize.x / spriteSize.x
            : cameraSize.y / spriteSize.y;
        t.position = Vector2.zero;
        t.localScale = scale;
    }
}
