using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    // Start is called before the first frame update
    private Vector3 movementInput;
    public Transform transf;
    public bool burrowed = false;
    public Vector3 burrow;
    public GameObject capsule;
    public bool digHim;
    GameObject enemy;
    float dist = 10;
    Vector3 directi = new Vector3(0, -1, 0);
    bool canBurrow;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movementInput = new Vector3(input.x, 0f, input.y); //this should be normalized in the input system itself.
        
    }

    // Update is called once per frame
    void Update()
    {

        if (movementInput.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Mathf.SmoothDamp(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            cam.position = Vector3.Lerp(cam.position, transform.position, Time.deltaTime * 20.0f);
            controller.Move(movementInput * speed * Time.deltaTime);

            
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, directi, out hit, dist, Physics.DefaultRaycastLayers)){
           if (hit.collider.gameObject.tag == "Steel")
           {
               canBurrow = false;
           }
           else
           {
               canBurrow = true;
           }
        }
        else{
            canBurrow = true;
           
        }
    }

    public void OnBurrow()
    {
        if (canBurrow)
        {
            burrowed = !burrowed;
            if (burrowed)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = -1.30f;
                transform.position = newPosition;
            }
            else
            {
                if (digHim)
                {
                    Vector3 newPosition = enemy.transform.position;
                    newPosition.y = -0.17f;
                    enemy.transform.position = newPosition;
                    enemy.GetComponent<EnemyBehaviour>().enabled = false;
                    enemy.GetComponent<CapsuleCollider>().enabled = false;

                    newPosition = transform.position;
                    newPosition.y = 0.20f;
                    transform.position = newPosition;
                    digHim = false;
    

                    StartCoroutine(GettingOut(enemy));
        
                
                }
                else
                {
                    Vector3 newPosition = transform.position;
                    newPosition.y = 0.20f;
                    transform.position = newPosition;
                }   
            }
        }


    }

    // void OnJump()
    // {
    //     RaycastHit hit;
    //     Vector3 direction = transform.position;
    //     direction.y += 50;
    //     float reach = 50;
    //     Debug.Log("Hej");
    //     if(Physics.Linecast(transform.position, direction, out hit, layerMask: Physics.DefaultRaycastLayers , queryTriggerInteraction: QueryTriggerInteraction.Ignore))
    //     {
    //         Debug.Log(hit.collider.gameObject.tag);
    //         // if(hit.collider.gameObject.tag == "Dirt")
    //         // {
    //         //     Vector3 rotation = transform.eulerAngles;
    //         //     rotation.x += 180;
    //         //     transform.rotation = Quaternion.Euler(rotation);
    //         //     Vector3 newPosition = transform.position;
    //         //     newPosition.y = 12.3f;
    //         //     transform.position = newPosition;
    //         // }
    //     }
    // }

    IEnumerator GettingOut(GameObject enemy)
    {
        yield return new WaitForSeconds(5);
        Vector3 newPosition = enemy.transform.position;
        newPosition.y = 1.03f;
        enemy.transform.position = newPosition;
        enemy.GetComponent<EnemyBehaviour>().enabled = true;
        enemy.GetComponent<CapsuleCollider>().enabled = true;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !burrowed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.tag == "Enemy" && burrowed)
        {
            enemy = other.gameObject;
            digHim = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            digHim = false;
        }
    }
    
}
