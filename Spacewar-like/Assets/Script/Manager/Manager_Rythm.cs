using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Rythm : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string blackHole = "";
    FMOD.Studio.EventInstance holeEvent;

    public void Start()
    {
        holeEvent = FMODUnity.RuntimeManager.CreateInstance(blackHole);
       
    }

    public void Sound()
    {
        holeEvent.start();
    }
}
