using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Winpoint : MonoBehaviour
{
    public int winPoint;
    public Text pointDisplay;

    // Start is called before the first frame update
    void Start()
    {
        winPoint = 3;
        Static_Variable.winPoint = winPoint;
    }

    // Update is called once per frame
    void Update()
    {
        pointDisplay.text = winPoint.ToString("F0");
    }

    public void AddPoint()
    {
        winPoint++;
        winPoint = Mathf.Clamp(winPoint, 1, 7);
        Static_Variable.winPoint = winPoint;
    }

    public void RemovePoint()
    {
        winPoint--;
        winPoint = Mathf.Clamp(winPoint, 1, 7);
        Static_Variable.winPoint = winPoint;
    }

    public void ResetPoint()
    {
        winPoint = 0;
        Static_Variable.winPoint = winPoint;
    }
}
