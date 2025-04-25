using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemyPrefab;
    public Transform player;
    public LayerMask layerMask;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float direction  = Random.Range(0,360);
        transform.position = player.position + new Vector3(Mathf.Sin(Mathf.Deg2Rad * direction),5,Mathf.Cos(Mathf.Deg2Rad * direction)) * 5;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 100, layerMask))
        {
            Instantiate(enemyPrefab, hit.point + new Vector3(0,1.75f,0), transform.rotation);
        }
    }
}
