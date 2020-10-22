using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 12.0f;
    Rigidbody rigidbody;
    public GameObject player;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // void start()
    // {
    //     Quaternion newRotation = transform.rotation;
    //     newRotation.x += -90;

    //     transform.rotation = newRotation;
    // }
    void FixedUpdate()
    {
        rigidbody.position += transform.up * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !player.GetComponent<Movement>().burrowed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(this.gameObject);
        }
        if (other.tag == "Respawn")
        {
            Destroy(this.gameObject);
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
