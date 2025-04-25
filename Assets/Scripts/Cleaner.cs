using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Cleaner : MonoBehaviour
{
    public Vector3 originalScale;
    public bool deleting;
    public float scale;
    public void Start()
    {
        deleting = false;
    }
    public void Init()
    {
        scale = 1.0f;
        originalScale = transform.localScale;
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (deleting)
        {
            scale = Mathf.Lerp(scale, 0.0f, Time.deltaTime * 30);
            transform.localScale = scale * originalScale;
            
            if (scale < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        deleting = true;
    }
}
