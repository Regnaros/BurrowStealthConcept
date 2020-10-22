using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Accessibility;

public class EnemyBehaviour : MonoBehaviour
{
    public float turn_speed = 6.0f;
    public GameObject PositionA;
    public GameObject PositionB;
    public List<GameObject> path = new List<GameObject>();
    public int nextDest = 0;
    public int looparound;
    public bool chase = false;
    public float speed = 6.0f;
    public GameObject player;
    Vector3 intermediatePos;
    public float turnSmoothTime = 1f;
    public float turnSmoothVelocity;
    
    // Start is called before the first frame update


    void Start()
    {
        path.Add(PositionA);
        path.Add(PositionB);
        looparound = path.Count;
    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime;

        if (!player.GetComponent<Movement>().burrowed)
        {
    
            //, layerMask: Physics.DefaultRaycastLayers, queryTriggerInteraction: QueryTriggerInteraction.Ignore
            if (chase)
            {
                Debug.Log(Physics.Linecast(this.transform.position, player.transform.position));
                if (Physics.Linecast(this.transform.position, player.transform.position))
                {
                    rotateTowards(path[nextDest].transform.position);
                    moveIt(step);
                    
                    
                    
                    
                    
                }
                else
                {
                    
                    rotateTowards(player.transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step); 
                    
                }
                
            }
            else
            {
                rotateTowards(path[nextDest].transform.position);
                moveIt(step);
            }
        }



        else
        {
            rotateTowards(path[nextDest].transform.position);
            moveIt(step);

            
        }
        

    }

    private void moveIt(float step)
    {
        if (path[nextDest].transform.position.x == this.transform.position.x &&
                path[nextDest].transform.position.y == this.transform.position.y)
        {
            nextDest += 1;
            if (nextDest == looparound)
            {
                nextDest = 0;
            }

        }
        Vector3 targetDestination;
        // targetDestination.x = path[nextDest].transform.position.x;
        // targetDestination.y = transform.position.y;
        // targetDestination.z = path[nextDest].transform.position.z;
        
        transform.position = Vector3.MoveTowards(transform.position, path[nextDest].transform.position, step);
    }

    private void rotateTowards(Vector3 to)
    {

        Quaternion _lookRotation =
            Quaternion.LookRotation((to - transform.position).normalized);

        //over time
        transform.rotation =
            Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turn_speed);

        //instant
        //transform.rotation = _lookRotation;
    }

}
