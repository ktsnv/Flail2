using UnityEngine;
using TMPro;

public class Fracture : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform[] children;
    public float health;
    public TextMeshPro healthText;
    private UIManager uimanager;
    public GameObject pickup;
    void Start()
    {
        uimanager = FindFirstObjectByType<UIManager>();
        children = GetComponentsInChildren<Transform>();
        foreach (Transform obj in children)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
        Collider parentCollider = GetComponent<Collider>();
        parentCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = Mathf.Round(health*10)/10 + "";
        if (health < 0)
        {
            if (Random.value >= 0.5f)
            {
                Debug.Log("Pickup Try Generate");
                Instantiate(pickup, transform.position, transform.rotation);
            }
            uimanager.UpdateScoreBy(1);
            Explode();
        }
    }

    void Explode()
    {
        transform.DetachChildren();
        
        Destroy(healthText.gameObject);
        foreach (Transform obj in children)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            Collider collider = obj.GetComponent<Collider>();
            if (rb != null)
            {
                //obj.gameObjec.SetActive(true);
                collider.enabled = true;
                rb.isKinematic = false;
                rb.AddExplosionForce(5, transform.position, 5, 2);                
            }

            if (obj.GetComponent<Cleaner>() != null)
            {
                obj.GetComponent<Cleaner>().Init();
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Weapon"))
        {
            health -= collision.impulse.magnitude;
        }
    }
}
