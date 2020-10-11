using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu_PlayerInput : MonoBehaviour
{
    public bool isSelection = false;
    public GameObject prefabPlayer;
    public PlayerInputManager inputManager;
    public Gamepad[] gamepad = new Gamepad[4];
    public GameObject[] player;
    public GameObject MenuPlayer;

    public GameObject[] root;

    public int i;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        inputManager.playerPrefab = prefabPlayer;
        InstantiateFirstPlayer();
    }

    // Update is called once per frame
    void Update()
    {

        if (isSelection)
        {
            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                if (CheckGamepad(Gamepad.current) && CheckGamepad(Gamepad.current, Static_Variable.gamepad))
                {
                    Static_Variable.gamepad[i] = Gamepad.current;
                    gamepad[i] = Gamepad.current;

                    InstantiatePlayer();
                }

            }
        }
    }

    public void ChangeSelection()
    {
        isSelection = true;
        MenuPlayer.SetActive(false);
    }

    public bool CheckGamepad(Gamepad current)
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

    public void InstantiateFirstPlayer()
    {
        MenuPlayer = inputManager.JoinPlayer(i, 0, "Gamepad", Gamepad.current).gameObject;
    }

    public void InstantiatePlayer()
    {

        player[i] = inputManager.JoinPlayer(i, 0, "Gamepad", gamepad[i]).gameObject;
        Menu_ActiveSelection menu_Active = player[i].GetComponent<Menu_ActiveSelection>();
        menu_Active.SetRoot(root[i]);
        menu_Active.indexPlayer = i;
        i++;
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

