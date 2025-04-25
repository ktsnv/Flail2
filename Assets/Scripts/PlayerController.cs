using System;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 vel;
    private Vector2 mouse;
    private Vector2 mouseVel;
    private Camera cam;
    public Transform lookAxis;
    //public Rigidbody handle;
    public Animator animator;
    public LayerMask layerMask;
    public GameObject[] weapons;
    public GameObject[] target;
    public TwoBoneIKConstraint[] ikArms;
    public RigBuilder rigBuilder;
    public Transform handle;
    private PhysicsMaterial ps;
    public float health;
    public Image healthImage;
    public Image healthImageBG;
    void Start()
    {
        health = 25;
        cam = FindFirstObjectByType<Camera>();
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<CapsuleCollider>().material;
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < weapons.Length;)
        {
            if (i == PlayerPrefs.GetInt("Weapon"))
            {

                weapons[i].SetActive(true);
                for (int j = 0; j < ikArms.Length;)
                {
                    ikArms[j].data.target = target[i].transform;
                    j++;
                }
                rigBuilder.Build();
            }
            else
            {
                weapons[i].SetActive(false);
                //weapons[i].transform.rotation = transform.rotation;
            }
            i++;
        }
    }

    
    void Update()
    {
        
        healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, health / 25, Time.deltaTime * 5);

        if (Mathf.Abs((health / 25) - healthImage.fillAmount) < .001f)
        {
            
            healthImageBG.fillAmount = Mathf.Lerp(healthImageBG.fillAmount, health / 25, Time.deltaTime * 20);
        }

        if (health < 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else if (health > 25)
        {
            health = 25;
        }
        RaycastHit hit;
        cam.transform.position = transform.position + Vector3.up;
        if (Physics.Raycast(cam.transform.position, -cam.transform.forward, out hit, 5, layerMask))
        {
            cam.transform.position = hit.point;
        }
        else
        {
            cam.transform.localPosition = new Vector3(0,1,-5);
        }

        vel.x = Input.GetAxis("Horizontal") * 5;
        vel.y = Input.GetAxis("Vertical") * 5;

        mouse.x += Input.GetAxis("Mouse X") * 2;
        mouseVel.x = Input.GetAxis("Mouse X") * 2;
        mouse.y += Input.GetAxis("Mouse Y") * 2;
        mouse.y = Mathf.Clamp(mouse.y, -85, 85);

        //transform.rotation = Quaternion.AngleAxis(mouse.x, Vector3.up);
        rb.AddTorque(Vector3.up * mouseVel.x * 2);
        

        rb.linearVelocity = vel.y * transform.forward + vel.x * transform.right + rb.linearVelocity.y * Vector3.up;
        if (vel.magnitude < 1)
        {
            if (GetComponent<ParticleSystem>().isPlaying)
            {
                GetComponent<ParticleSystem>().Stop();
            }
            animator.SetBool("Walking?", false);
        }
        else
        {
            if (!GetComponent<ParticleSystem>().isPlaying)
            {
                GetComponent<ParticleSystem>().Play();
            }
            animator.SetBool("Walking?", true);
        }

        if (Grounded())
        {
            ps.dynamicFriction = 0.6f;
            ps.staticFriction = 0.6f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * 25, ForceMode.Impulse);
            }
        }
        else
        {
            ps.dynamicFriction = 0;
            ps.staticFriction = 0;
        }
    }

    private void LateUpdate()
    {
        //lookAxis.localRotation = Quaternion.AngleAxis(-mouse.y, Vector3.right);
        lookAxis.localRotation = Quaternion.Lerp(lookAxis.localRotation, Quaternion.AngleAxis(-mouse.y, Vector3.right), Time.deltaTime * 30);
        //hand.position = Vector3.Lerp(hand.position, transform.position + transform.forward * 0.75f, Time.deltaTime * 25);
        //hand.rotation = Quaternion.Lerp(hand.rotation, transform.rotation, Time.deltaTime * 25);
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0,-0.9f,0), -transform.up, 0.5f);
    }
}
