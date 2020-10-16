using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;


public class Menu_PlayerInput : MonoBehaviour
{
    public bool isSelection = false;
    public GameObject prefabPlayer;
    public PlayerInputManager inputManager;
    public Gamepad[] gamepad = new Gamepad[4];
    public GameObject[] player;
    public GameObject MenuPlayer;

    public GameObject[] root;
    public GameObject gameSelection;
    public GameObject selectButton;
    public GameObject Rroot;

    [Header("Son")]
    public Image ima;
    public Sprite one;
    public Sprite two;

    [FMODUnity.EventRef]
    public string Prop = "";
    FMOD.Studio.EventInstance PropEvent;

    public GameObject Selection;
    public MultiplayerEventSystem system;


    public int i;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        inputManager.playerPrefab = prefabPlayer;
        InstantiateFirstPlayer();
        PropEvent = FMODUnity.RuntimeManager.CreateInstance(Prop);
      
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
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                player[i].SetActive(true);
            }
        }
    }

    public void Son()
    {
        Static_Variable.son = !Static_Variable.son;
        if (Static_Variable.son)
        {
            ima.sprite = one;
        }
        else
        {
            ima.sprite = two;
        }

    }

    public void ReturnGameMode()
    {
        SceneManager.LoadScene(0);
        Static_Variable.ResetVariable(2);
        //isSelection = false;
        //MenuPlayer.SetActive(true);
        //gameSelection.SetActive(true);
        //Selection.SetActive(false);
        //system.playerRoot = Rroot;
        //system.SetSelectedGameObject(selectButton);
        //system.firstSelectedGameObject = selectButton;
        //for (int i = 0; i < player.Length; i++)
        //{
        //    if (player[i] != null)
        //    {
        //        player[i].SetActive(false);
        //    }
        //}
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
        system = MenuPlayer.GetComponent<MultiplayerEventSystem>();
    }

    public void InstantiatePlayer()
    {

        player[i] = inputManager.JoinPlayer(i, 0, "Gamepad", gamepad[i]).gameObject;
        Menu_ActiveSelection menu_Active = player[i].GetComponent<Menu_ActiveSelection>();
        menu_Active.SetRoot(root[i]);
        menu_Active.indexPlayer = i;
        menu_Active.playerInput = this;
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

