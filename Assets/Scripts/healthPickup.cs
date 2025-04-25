using UnityEngine;

public class healthPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Pickup Generated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().health += 5;
            Destroy(gameObject);
        }
    }
}
