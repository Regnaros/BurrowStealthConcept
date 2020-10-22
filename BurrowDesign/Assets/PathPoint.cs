using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshRenderer meshy;
    void Start()
    {
        meshy = GetComponent<MeshRenderer>();
        meshy.enabled = false;
    }

}
