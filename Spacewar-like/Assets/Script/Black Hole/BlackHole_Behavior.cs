using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Behavior : MonoBehaviour
{
    public GameObject[] player;

    public float distance;
    public float maxforce = 10f;
    public float minForce = 3f;
    public Manager_Score manager;

    private bool Ingame = true;

    void Update()
    {
        if (manager.gameState == Manager_Score.StateOfGame.Game)
        {
            // Attraction SpaceShip
            BlackHoleAttraction();
            BlackHoleGameStatus();

        }
        else
        {
            BlackHoleGame();
        }

    }

    private void BlackHoleGameStatus()
    {
            ChangePlayerMouvement(false);
            return;       
    }

    private void BlackHoleGame()
    {
        ChangePlayerMouvement(true);
        return;
    }


    private void ChangePlayerMouvement(bool changeState)
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                player[i].GetComponent<Player_Mouvement>().StopMouvemen(changeState);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public void BlackHoleAttraction()
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                float currentdist = Vector3.Distance(transform.position, player[i].transform.position);
                float forceAppli = ForceTraction(currentdist);

                if (currentdist < distance)
                {
                    Vector3 blackHoleDir = transform.position - player[i].transform.position;
                    Rigidbody rigid = player[i].GetComponent<Rigidbody>();
                    rigid.AddForce(blackHoleDir.normalized * forceAppli, ForceMode.Acceleration);
                }
            }

        }
    }

    public float ForceTraction(float currentdistance)
    {
        float forceGive = currentdistance / distance * (maxforce - minForce);
        return forceGive;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Manager_Score.PlayerDeath(other.gameObject);
        }
    }


}
