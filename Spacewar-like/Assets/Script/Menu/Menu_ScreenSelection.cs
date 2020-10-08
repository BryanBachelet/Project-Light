using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Menu_ScreenSelection : MonoBehaviour
{
    public int maxPlayerNumber;
    public int index;

    public GameObject[] player =  new GameObject[4];

    public Color[] colors = new Color[3];

    public int readyCheck;

    public int indexSceneGame;

    public static Menu_ScreenSelection screenSelection ;

    private void Awake()
    {
        screenSelection = this;
    }


    void Update()
    {
        if (Gamepad.current.xButton.wasPressedThisFrame)
        {
            if (CheckGamepad(Gamepad.current,Static_Variable.gamepad))
            {
                if (index < maxPlayerNumber)
                {
                    Static_Variable.gamepad[index] = Gamepad.current;
                    ActivePlayer(index, 0, player[0]);
                    ActivePlayer(index, 1, player[1]);
                    index++;

                }
            }
            if(readyCheck == index && index>1)
            {
                SceneManager.LoadScene(indexSceneGame);
            }
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


    public void ActivePlayer(int index, int playernumber,GameObject player)
    {
        if (index == playernumber)
        {
            player.SetActive(true);
            player.GetComponent<Menu_ActiveSelection>().enabled = true;
        }
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
