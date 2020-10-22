using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapScript : MonoBehaviour
{
    GameObject player;
    bool onTop = false;

    void Start()
    {
        player = GameObject.Find("Player");
        Debug.Log(player.GetComponent<Movement>().burrowed);
    }

    void Update()
    {
        if (player.GetComponent<Movement>().burrowed && onTop)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Heres a trap");
        if (other.tag == "Player" && player.GetComponent<Movement>().burrowed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (other.tag == "Player")
        {
            onTop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onTop = false;
        }
    }

}
