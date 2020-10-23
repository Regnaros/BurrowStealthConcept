using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelScript : MonoBehaviour
{
    public Movement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.burrowed)
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
        }
        else
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
