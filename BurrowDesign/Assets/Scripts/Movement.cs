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
    public bool burrowed;
    public Vector3 burrow;
    public GameObject capsule;

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

    public void OnBurrow()
    {
        burrowed = !burrowed;
        if (burrowed)
        {
            Vector3 newPosition = capsule.transform.position;
            newPosition.y = -0.17f;
            capsule.transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = capsule.transform.position;
            newPosition.y = 1.03f;
            capsule.transform.position = newPosition;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
}
