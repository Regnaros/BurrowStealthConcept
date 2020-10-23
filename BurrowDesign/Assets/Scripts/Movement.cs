using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    // Start is called before the first frame update
    private Vector3 movementInput;
    public bool burrowed = false;
    public Vector3 burrow;
    public GameObject capsule;
    public bool digHim;
    GameObject enemy;
    float dist = 10;
    Vector3 directi = new Vector3(0, -1, 0);
    bool canBurrow;
    private Vector3 movementVector; 

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

        if (movementInput.magnitude > 0.1f)
        {
            Vector3 relativeInput = GetMovementRelativeToCamera();
            //rotate player to face same direction as input-direction
            float angleDelta = Vector3.SignedAngle(this.transform.forward, relativeInput, Vector3.up);
            angleDelta *= Mathf.Min(Time.deltaTime * speed, 1f); //values >1 will cause oversteer
            transform.Rotate(0f, angleDelta, 0f);
            //apply relative input with movespeed to the movement vector
            movementVector.x = relativeInput.x * speed;
            movementVector.z = relativeInput.z * speed;
        }
        else //if no input, reset xz-movement; 
            movementVector.x = movementVector.z = 0;
        //use character-controller to move player (takes care of slopes & other edge-cases)
        controller.Move(movementVector * Time.deltaTime);
    
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

    void OnJump()
    {
        RaycastHit hit;
        Vector3 startdirection = transform.position;
        startdirection.y += 4;
        Vector3 direction = transform.position;
        direction.y += 50;
        float reach = 50;
        Debug.Log("Hej");
        if(Physics.Linecast(startdirection, direction, out hit, layerMask: Physics.DefaultRaycastLayers , queryTriggerInteraction: QueryTriggerInteraction.Ignore))
        {
            Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.tag == "Dirt")
            {
                Vector3 rotation = transform.eulerAngles;
                rotation.x += 180;
                transform.rotation = Quaternion.Euler(rotation);
                Vector3 newPosition = transform.position;
                newPosition.y = 12.3f;
                transform.position = newPosition;
            }
        }
    }

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

    private Vector3 GetMovementRelativeToCamera()
    {
        //Player moves in the xz-plane based on camera forward, so simply project the camera forward.
        Vector3 cameraDirectionProjected = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
        //actual movement is movement relative to camera forward, but input is relative to Vector3.forward, so we must first rotate the input vector
        float angleToRotate = Vector3.SignedAngle(Vector3.forward, cameraDirectionProjected, Vector3.up);
        Vector3 rotatedInput = Quaternion.AngleAxis(angleToRotate, Vector3.up) * movementInput;
        return rotatedInput;
    }
    
}

