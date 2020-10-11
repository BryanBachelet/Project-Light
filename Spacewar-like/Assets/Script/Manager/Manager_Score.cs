using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class Manager_Score : MonoBehaviour
{
    public enum StateOfGame { Any, Start, Game, Finish, Pause }
    public StateOfGame gameState = StateOfGame.Game;

    public int winPoint = 0;
    public int blueScore = 0;
    public int redScore = 0;

    [Header("GamePhase")]
    public float timerOfDeath = 1;
    private float countdownOfDeath;

    [Header("Start Phase")]
    public float startPhasetimer;
    public Text timer;

    [Header("Finish Phase")]
    public GameObject finishGo;
    public GameObject firstButton;
    public GameObject root;
    public MultiplayerEventSystem eventSystem;

    [Header("Pause Phase")]
    public GameObject pauseGo;
    public GameObject buttonPause;
    public GameObject rootPause;
  

    private float countStartPhase;

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
        if (prefabExplosion != null)
        {
            explosionParticles = prefabExplosion;
        }
        manager_JoinPlayer = GetComponent<Manager_JoinPlayer>();

        ChangeState(StateOfGame.Start);

    }

    private void Update()
    {
        if (gameState == StateOfGame.Start)
        {
            timer.text = (startPhasetimer - countStartPhase).ToString("F0");

            if (countStartPhase > startPhasetimer)
            {
                ChangeState(StateOfGame.Game);
            }
            countStartPhase += Time.deltaTime;
        }

        if (gameState == StateOfGame.Finish)
        {

        }
        if (gameState == StateOfGame.Pause)
        {

        }
        if (gameState == StateOfGame.Game)
        {

            if (activeReset)
            {
                if (countdownOfDeath > timerOfDeath)
                {
                    SetScore();
                    countdownOfDeath = 0;
                }
                else
                {
                    countdownOfDeath += Time.deltaTime;
                }
            }
        }
        if (lastExplosion != null)
        {
            tempsEcouleExplosion += Time.deltaTime;
            if (tempsEcouleExplosion > 2)
            {
                Destroy(lastExplosion);
                lastExplosion = null;
            }

        }

    }

    public void ActiveGame()
    {
        DeactivePlayer(true);
        timer.enabled = false;
        finishGo.SetActive(false);
        pauseGo.SetActive(false);
    }

    public void ActiveStart()
    {
        gameState = StateOfGame.Start;
        timer.enabled = true;
        DeactivePlayer(false);
        countStartPhase = 0;
        finishGo.SetActive(false);
    }

    public void ActiveFinish()
    {
        finishGo.SetActive(true);
        eventSystem.firstSelectedGameObject = firstButton;
        eventSystem.SetSelectedGameObject(firstButton);
        eventSystem.playerRoot = root;
        DeactivePlayer(false);
    }

    public void ActivePause()
    {
        finishGo.SetActive(false);
        pauseGo.SetActive(true);
        eventSystem.firstSelectedGameObject = buttonPause;
        eventSystem.SetSelectedGameObject(buttonPause);
        eventSystem.playerRoot = rootPause;
        DeactivePlayer(false);
    }


    public void ChangeState(StateOfGame state)
    {
        gameState = state;

        switch (state)
        {
            case (StateOfGame.Start):
                ActiveStart();

                break;
            case (StateOfGame.Game):
                ActiveGame();

                break;
            case (StateOfGame.Finish):
                ActiveFinish();
                break;
            case (StateOfGame.Pause):

                break;

        }
    }


    public void DeactivePlayer(bool stateBool)
    {
        for (int i = 0; i < manager_JoinPlayer.player.Length; i++)
        {
            manager_JoinPlayer.player[i].GetComponent<Tool_ScriptManager>().Disable(stateBool);
        }
    }


    public static void PlayerDeath(GameObject player)
    {
        // lastExplosion = Instantiate(explosionParticles, player.transform.position, player.transform.rotation);
        player.GetComponent<Player_Team>().currentShip = Player_Team.ShipState.Die;
        player.SetActive(false);

        tempsEcouleExplosion = 0;
        activeReset = true;
    }

    public void SetScore()
    {
        for (int i = 0; i < manager_JoinPlayer.player.Length; i++)
        {
            if (manager_JoinPlayer.player[i].GetComponent<Player_Team>().currentShip != Player_Team.ShipState.Die)
            {
                if (manager_JoinPlayer.player[i].GetComponent<Player_Team>().team == Player_Team.ColorTeam.Red) redScore++;
                if (manager_JoinPlayer.player[i].GetComponent<Player_Team>().team == Player_Team.ColorTeam.Blue) blueScore++;
            }
        }

        if (redScore == winPoint || blueScore == winPoint)
        {
            ChangeState(StateOfGame.Finish);

            blueScore = 0;
            redScore = 0;
        }
        else
        {
            ChangeState(StateOfGame.Start);
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
