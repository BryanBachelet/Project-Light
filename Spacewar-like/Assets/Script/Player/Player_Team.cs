using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Team : MonoBehaviour
{
    public enum ColorTeam { Any, Red, Blue };
    public ColorTeam team = ColorTeam.Any;

    public enum ShipState {Any, Alive, Die};
    public ShipState currentShip = ShipState.Any;

    public int indexPlayer;
    public string playerProfil;

    private Player_Mouvement player_Mouvement;
    private Player_Shoot player_Shoot;

    private void Start()
    {
        player_Mouvement = GetComponent<Player_Mouvement>();
        player_Shoot = GetComponent<Player_Shoot>();
        playerProfil = Static_Variable.profilName[indexPlayer];
    }


    public void ResetGame()
    {
        player_Mouvement.ResetGame();
        player_Shoot.ResetGame();
    }
}
