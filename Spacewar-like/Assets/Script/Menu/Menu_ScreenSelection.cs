using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Menu_ScreenSelection : MonoBehaviour
{
    public int maxPlayerNumber;
    public int index;

    public GameObject[] player = new GameObject[4];

    public Color[] colors = new Color[3];

    public int readyCheck;

    public int indexSceneGame;

    public string[] nameProfil = new string[53];

    public static Menu_ScreenSelection screenSelection;
    public GameObject screanControl;
    public GameObject Selection;
    public GameObject PressALaunch;

    private void Awake()
    {
        screenSelection = this;
    }


    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.xButton.wasPressedThisFrame)
            {

                if (index < maxPlayerNumber)
                {


                    index++;

                }

            }
            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                if (readyCheck == index && index > 1)
                {
                    screanControl.SetActive(true);
                    Selection.SetActive(false);
                }
            }
        }
        if (readyCheck == index && index > 1)
        {
            PressALaunch.SetActive(true);
        }
    }

    public void SendReady()
    {
        readyCheck++;

    }

    public void SendUnready()
    {
        readyCheck--;
    }

    public int CheckIndex(Gamepad gamepad)
    {
        int i = 0;
        for (int j = 0; j < Static_Variable.gamepad.Length; j++)
        {
            if(gamepad== Static_Variable.gamepad[j])
            {
                i = j;
            }
        }

        return i;
    }




    public bool CheckGamepad(Gamepad current, Gamepad[] gamepad)
    {
        bool check = true;
        for (int i = 0; i < gamepad.Length; i++)
        {
            if (current == gamepad[i])
            {
                check = false;
            }
        }


        return check;

    }
}
