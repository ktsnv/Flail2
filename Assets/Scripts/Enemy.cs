using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindFirstObjectByType<PlayerController>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.position), 30 * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        rb.AddForce(-transform.forward * 200 * Time.deltaTime);
        //rb.AddForce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().health -= collision.impulse.magnitude; ;
        }
    }
}
