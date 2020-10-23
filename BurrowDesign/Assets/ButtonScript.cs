using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(door);
            Vector3 newPos = transform.position;
            newPos.y = -0.4f;
            transform.position = newPos;
        }
    }
}
