using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_ScriptManager : MonoBehaviour
{
    public MonoBehaviour[] scriptListOne;

    //Permet d'activer ou désactiver les scripts contenue dans le tableau
    public void ChangeScriptStatus(bool state)
    {
        for (int i = 0; i < scriptListOne.Length; i++)
        {
            scriptListOne[i].enabled = state;
        }
    }
}
