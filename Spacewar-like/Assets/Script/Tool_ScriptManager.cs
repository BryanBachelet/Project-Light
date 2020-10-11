using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_ScriptManager : MonoBehaviour
{
    public MonoBehaviour[] scriptListOne;
    public float test;

    public void Disable(bool state)
    {
        for (int i = 0; i < scriptListOne.Length; i++)
        {
            scriptListOne[i].enabled = state;
        }
    }
}
