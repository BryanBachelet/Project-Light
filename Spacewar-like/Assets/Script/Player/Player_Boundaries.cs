using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Boundaries : MonoBehaviour
{
    public Vector3 center;

  
    void Update()
    {
        if (Tool.ObjectOnScreen(transform.position))
        {
            RePosition();
        }
    }

    public void RePosition()
    {
        int sideIndex = Tool.CheckPositionOfOutScreen(transform.position);
        
        if(sideIndex == 1)
        {
            Vector3 pos = transform.position - center;
            transform.position = center + new Vector3(transform.position.x, transform.position.y  ,pos.z *-1+0.5f);
           
        }

        if(sideIndex == 3)
        {
            Vector3 pos = transform.position - center;
            transform.position = center + new Vector3(transform.position.x, transform.position.y, pos.z*-1+ -0.5f);
         
        }

        if (sideIndex == 2)
        {
            Vector3 pos = transform.position - center;
            transform.position = center + new Vector3(pos.x*-1+0.5f, transform.position.y , transform.position.z);
           
        }
        if (sideIndex == 4)
        {
            Vector3 pos = transform.position - center;
            transform.position = center + new Vector3(pos.x * -1 - 0.5f, transform.position.y, transform.position.z);
         
        }
    }

}
