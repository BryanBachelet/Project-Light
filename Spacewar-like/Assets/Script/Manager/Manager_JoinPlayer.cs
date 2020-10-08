using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Manager_JoinPlayer : MonoBehaviour
{
    public PlayerInputManager inputManager;
    public GameObject playerPrefab;
    public GameObject[] player;
    public Gamepad[] gamepad = new Gamepad[2];
    public int i = 0;
    public GameObject blackHole;
    public Transform blackHolePosition;

    public Transform playerStartPos1;
    public Transform playerStartPos2;

    // Start is called before the first frame update
    void Awake()
    {
        //InstantiateBlackHole();
    }

    public void Update()
    {
        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            if (CheckGamepad(Gamepad.current))
            {
                gamepad[i] = Gamepad.current;
                Debug.Log(gamepad[i]);
                InstantiatePlayer();
            }

        }
    }

    public void InstantiatePlayer()
    {

        player[i] = inputManager.JoinPlayer(i, 0, "Gamepad", gamepad[i]).gameObject;
        if (i == 0)
        {
            player[i].transform.position = playerStartPos1.position;
            SetPlayerTeam(player[i], Color.blue, Player_Team.ColorTeam.Blue);
        }
        if (i == 1)
        {
            player[i].transform.position = playerStartPos2.position;
            SetPlayerTeam(player[i], Color.red, Player_Team.ColorTeam.Red);
        }
        i++;
    }

    public void SetPlayerTeam(GameObject player, Color teamColor, Player_Team.ColorTeam colorTeam)
    {
        player.GetComponent<Player_Team>().team = colorTeam;
        player.GetComponent<MeshRenderer>().material.color = teamColor;
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
        GameObject blackHoleInstante = Instantiate(blackHole, blackHolePosition.position, Quaternion.identity);
        BlackHole_Behavior blackhole = blackHoleInstante.GetComponent<BlackHole_Behavior>();
        blackhole.player = player;
        blackhole.manager = this;

    }

    public void ResetGame()
    {
        player[0].transform.position = playerStartPos1.position;
        player[1].transform.position = playerStartPos2.position;
    }
}
