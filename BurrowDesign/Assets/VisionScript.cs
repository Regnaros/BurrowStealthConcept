using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<EnemyBehaviour>().chase = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<EnemyBehaviour>().chase = false;
        }
    }
}
