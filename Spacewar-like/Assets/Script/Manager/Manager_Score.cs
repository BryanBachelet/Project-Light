using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using FMODUnity;

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
    public Text Winner;
    public MultiplayerEventSystem eventSystem;

    [Header("Pause Phase")]
    public GameObject pauseGo;
    public GameObject buttonPause;
    public GameObject rootPause;



    private float countStartPhase;

    [Header("Affichage")]
    public Text scoreBlueUi;
    public Text scoreRedUi;
    public Text PlayerNameOne;
    public Text PlayerNameTwo;

    public Manager_Rythm managerSon;

    private Manager_JoinPlayer manager_JoinPlayer;
    private static bool activeReset;

    private bool test;

    public GameObject prefabExplosion;

    [FMODUnity.EventRef]
    public string blackHole = "";
    FMOD.Studio.EventInstance holeEvent;


    [FMODUnity.EventRef]
    public string Prop = "";
    FMOD.Studio.EventInstance PropEvent;



    static GameObject explosionParticles;
    static GameObject lastExplosion;
    static float tempsEcouleExplosion;
    // Start is called before the first frame update
    void Start()
    {
        PropEvent = FMODUnity.RuntimeManager.CreateInstance(Prop);
        PropEvent.start();
        if (prefabExplosion != null)
        {
            explosionParticles = prefabExplosion;
        }
        manager_JoinPlayer = GetComponent<Manager_JoinPlayer>();
        if (Static_Variable.winPoint > 0 && Static_Variable.winPoint < 8)
        {
            winPoint = Static_Variable.winPoint;
            PlayerNameOne.text = Static_Variable.profilName[0];
            PlayerNameTwo.text = Static_Variable.profilName[1];
        }
        else
        {
            winPoint = 3;
        }
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
                    if (!test)
                    {
                        managerSon.Sound();
                        test = true;
                    }
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

    public void StopProp()
    {
        PropEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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


    public void ResetPause()
    {
        ChangeState(StateOfGame.Game);
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
                ActivePause();
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
        Destroy(lastExplosion);
        Manager_JoinPlayer.blackHoleInstante.GetComponentInChildren<ParticleSystemForceField>().gravity = -5f;

        //lastExplosion = Instantiate(explosionParticles, player.transform.position, player.transform.rotation);
        player.GetComponent<Player_Team>().currentShip = Player_Team.ShipState.Die;
        player.transform.GetChild(0).gameObject.SetActive(false);
        //player.SetActive(false);

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
            if (redScore == winPoint)
            {
                Winner.text = PlayerNameTwo.text + " Win !";
            }
            if (blueScore == winPoint)
            {
                Winner.text = PlayerNameOne.text + " Win !";
            }

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
        test = false;
    }

    public void ResetGame()
    {

        for (int i = 0; i < manager_JoinPlayer.player.Length; i++)
        {
            manager_JoinPlayer.player[i].SetActive(true);
            manager_JoinPlayer.player[i].GetComponent<Player_Team>().currentShip = Player_Team.ShipState.Alive;
            manager_JoinPlayer.player[i].GetComponent<Player_Team>().ResetGame();
            manager_JoinPlayer.player[i].transform.GetChild(0).gameObject.SetActive(true);
        }
        manager_JoinPlayer.ResetGame();

        activeReset = false;
    }
}
