using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public LineRenderer line;
    public Transform origin;
    void Update()
    {
        line.SetPosition(0, origin.transform.position);
        if (Physics.Raycast(origin.transform.position, origin.up, out RaycastHit info, 100))
        {
            line.SetPosition(1, info.point);
            //Debug.DrawRay(transform.position, origin.up* 100, Color.green);
        }
        else
        {
            line.SetPosition(1, origin.up*100);
        }

    }
}
