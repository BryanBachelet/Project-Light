using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Score : MonoBehaviour
{
    public enum StateOfGame {Any ,Start, Game ,Finish, Pause}
    public StateOfGame gameState = StateOfGame.Game;

    public int winPoint = 0;
    public int blueScore = 0;
    public int redScore = 0;

    [Header("Affichage")]
    public Text scoreBlueUi;
    public Text scoreRedUi;

    private Manager_JoinPlayer manager_JoinPlayer;
    private static bool activeReset;

    public GameObject prefabExplosion;
    static GameObject explosionParticles;
    static GameObject lastExplosion;
    static float tempsEcouleExplosion;
    // Start is called before the first frame update
    void Start()
    {
        if(prefabExplosion != null)
        {
            explosionParticles = prefabExplosion;
        }
        manager_JoinPlayer = GetComponent<Manager_JoinPlayer>();
    }

    private void Update()
    {
        if (activeReset)
        {
            SetScore();
        }
        if(lastExplosion != null)
        {
            tempsEcouleExplosion += Time.deltaTime;
            if(tempsEcouleExplosion > 2)
            {
                Destroy(lastExplosion);
                lastExplosion = null;
            }

        }
        
    }

    public static void PlayerDeath(GameObject player)
    {
       // lastExplosion = Instantiate(explosionParticles, player.transform.position, player.transform.rotation);
        player.GetComponent<Player_Team>().currentShip = Player_Team.ShipState.Die;
        
        tempsEcouleExplosion = 0;
        //player.SetActive(false);
        activeReset = true;
    }

    public void SetScore()
    {
        for (int i = 0; i < manager_JoinPlayer.player.Length; i++)
        {
            if (manager_JoinPlayer.player[i].activeSelf)
            {
                if (manager_JoinPlayer.player[i].GetComponent<Player_Team>().team == Player_Team.ColorTeam.Red) redScore++;
                if (manager_JoinPlayer.player[i].GetComponent<Player_Team>().team == Player_Team.ColorTeam.Blue) blueScore++;
            }
        }

        if(redScore == winPoint ||blueScore == winPoint)
        {
            gameState = StateOfGame.Finish;
            blueScore = 0;
            redScore = 0;
        }

        scoreBlueUi.text = blueScore.ToString();
        scoreRedUi.text = redScore.ToString();
        ResetGame();
    }

    public void ResetGame()
    {
        

        for (int i = 0; i < manager_JoinPlayer.player.Length; i++)
        {
            manager_JoinPlayer.player[i].SetActive(true);
            manager_JoinPlayer.player[i].GetComponent<Player_Team>().currentShip = Player_Team.ShipState.Alive;
            manager_JoinPlayer.player[i].GetComponent<Player_Team>().ResetGame();
        }
        manager_JoinPlayer.ResetGame();
        activeReset = false;
    }
}
