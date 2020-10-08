using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Tool : MonoBehaviour
{
   public static bool ObjectOnScreen(Vector3 position)
    {

        bool OnScreen = false;

        Vector3 pos = Camera.main.WorldToViewportPoint(position);
        if (pos.x > 1 || pos.x < 0)
        {
            OnScreen = true;
        }

        if (pos.y > 1 || pos.y < 0)
        {
            OnScreen = true;
        }

        return OnScreen;
    }

    public static int CheckPositionOfOutScreen(Vector3 position)
    {

        int OnScreen = 0;

        Vector3 pos = Camera.main.WorldToViewportPoint(position);
        if (pos.x > 1)
        {
            OnScreen = 2;
        }
        if (pos.x < 0)
        {
            OnScreen = 4;
        }

        if (pos.y < 0)
        {
            OnScreen = 3;
        }

        if (pos.y > 1)
        { 
            OnScreen = 1;
        }

        return OnScreen;
    }
}
