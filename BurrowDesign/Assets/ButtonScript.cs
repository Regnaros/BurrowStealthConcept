using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public Vector3 startPos;
    public GameObject button;

    // Update is called once per frame

    void Start()
    {
        startPos  = button.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (door.active)
            {
                door.SetActive(false);
            }
            else
            {
                door.SetActive(true);
            }
            Vector3 newPos = transform.position;
            newPos.y = -0.2f;
            button.transform.position = newPos;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            button.transform.position = startPos;
        }
    }
}
