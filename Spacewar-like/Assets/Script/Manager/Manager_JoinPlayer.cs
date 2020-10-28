using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;


public class Manager_JoinPlayer : MonoBehaviour
{
    public PlayerInputManager inputManager;
    private Manager_Score manager;
    public GameObject playerPrefab;
    public GameObject[] player;
    public Gamepad[] gamepad = new Gamepad[2];
    public Keyboard keyboard;

    public int i = 0;
    public GameObject blackHole;
    public Transform blackHolePosition;

    public Transform playerStartPos1;
    public Transform playerStartPos2;
    public bool logMenu = false;

    public StudioListener studio;

    public Color red;
    public Color blue;

    InputDevice device;


    static public GameObject blackHoleInstante;
    // Start is called before the first frame update
    void Awake()
    {
        studio.enabled = Static_Variable.son;
        gamepad = new Gamepad[Static_Variable.gamepad.Length];
        manager = GetComponent<Manager_Score>();
        InstantiateBlackHole();
        device = Keyboard.current;
       
        Debug.Log("Device = " + device.name);

        // Instantiate Player Gampad From Menu
        //if (Static_Variable.player.Length > 1)
        //{
        //    logMenu = true;
        //    for (int j = 0; j < Static_Variable.gamepad.Length; j++)
        //    {
        //        gamepad[i] = Static_Variable.gamepad[i];
        //        InstantiatePlayer("Gamepad");
        //    }
        //}

        // Instatiate Player from Keyboard
        if (Keyboard.current != null && !logMenu)
        {
            InstantiatePlayer();
        }

        //Instantiate Player from Gamepad directly in the scene  
        if (Gamepad.current != null && !logMenu)
        {
         
                if (CheckGamepad(Gamepad.current))
                {
                    gamepad[i] = Gamepad.current;
                    Debug.Log(gamepad[i]);
                    InstantiatePlayer("Gamepad");
                }

            
        }

      
    }

    public void Update()
    {
        ////Instantiate Player from Gamepad directly in the scene  
        //if (Gamepad.current != null && !logMenu)
        //{
        //    if (Gamepad.current.aButton.wasPressedThisFrame)
        //    {
        //        if (CheckGamepad(Gamepad.current))
        //        {
        //            gamepad[i] = Gamepad.current;
        //            Debug.Log(gamepad[i]);
        //            InstantiatePlayer("Gamepad");
        //        }

        //    }
        //}

        //// Instatiate Player from Keyboard
        //if (Keyboard.current != null && !logMenu)
        //{
        //    InstantiatePlayer();
        //}
    }


    public void InstantiatePlayer(string controller)
    {
        Debug.Log("Test");
        player[i] = inputManager.JoinPlayer(i, 0, controller, gamepad[i]).gameObject;

        if (i == 0)
        {
            player[i].transform.position = playerStartPos1.position;
            SetPlayerTeam(player[i], blue, Player_Team.ColorTeam.Blue);
        }
        if (i == 1)
        {
            player[i].transform.position = playerStartPos2.position;
            SetPlayerTeam(player[i], red, Player_Team.ColorTeam.Red);
        }
        i++;
    }

    public void InstantiatePlayer()
    {

        player[i] = inputManager.JoinPlayer(i, 0, "KeyboardMouse", Keyboard.current).gameObject;

        if (i == 0)
        {
            player[i].transform.position = playerStartPos1.position;
            SetPlayerTeam(player[i], blue, Player_Team.ColorTeam.Blue);
        }
        if (i == 1)
        {
            player[i].transform.position = playerStartPos2.position;
            SetPlayerTeam(player[i], red, Player_Team.ColorTeam.Red);
        }
        i++;
    }

    public void SetPlayerTeam(GameObject player, Color teamColor, Player_Team.ColorTeam colorTeam)
    {
        player.GetComponent<Player_Team>().team = colorTeam;
        player.GetComponent<Player_Team>().indexPlayer = i;
        player.GetComponent<MeshRenderer>().material.color = teamColor;
        player.GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", teamColor);

        Player_Mouvement Mouvement = player.GetComponent<Player_Mouvement>();
        MeshRenderer mesh = Mouvement.model.GetComponent<MeshRenderer>();
        mesh.materials[0].color = teamColor;
        mesh.materials[1].color = teamColor * 0.75f;
        player.GetComponent<Player_MenuInput>().manager = manager;
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

    public void InstantiateBlackHole()
    {
        blackHoleInstante = Instantiate(blackHole, blackHolePosition.position, Quaternion.identity);
        BlackHole_Behavior blackhole = blackHoleInstante.GetComponent<BlackHole_Behavior>();
        blackhole.player = player;
        blackhole.manager = manager;


    }

    public void ResetGame()
    {
        player[0].transform.position = playerStartPos1.position;
        player[0].transform.rotation = Quaternion.identity;
        //  player[0].GetComponent<MeshRenderer>().enabled = true;
        player[1].transform.position = playerStartPos2.position;
        player[1].transform.rotation = Quaternion.identity;
        // player[1].GetComponent<MeshRenderer>().enabled = true;
        blackHoleInstante.GetComponentInChildren<ParticleSystemForceField>().gravity = 0.15f;
    }
}
