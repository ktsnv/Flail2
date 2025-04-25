using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Camera cam;
    public Rigidbody playerRB;
    private float time;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 70 + playerRB.linearVelocity.magnitude, Time.deltaTime * 30);
        cam.transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0,0,Mathf.Sin(time * 10) * playerRB.linearVelocity.magnitude / 5), Time.deltaTime * 30);
        time += Time.deltaTime;
    }
}
