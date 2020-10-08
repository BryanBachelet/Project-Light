using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    public float speedProjectile;
    public float lifetime;
    public Vector3 direction;

    public Player_Team player_Team;
    public Player_Shoot player;
    private float countLifeTime;


    void Update()
    {
        transform.position += direction.normalized * speedProjectile * Time.deltaTime;

        if (countLifeTime > lifetime || CheckInCamera())
        {
            DestroyProjectile();
        }
        else
        {
            countLifeTime += Time.deltaTime;
        }

    }

    private bool CheckInCamera()
    {
        bool OnScreen = false;

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x > 1 || pos.x < 0)
        {
            OnScreen = true;
        }

        if (pos.y > 1 || pos.y < 0)
        {
            OnScreen = true;
        }

        return OnScreen;
    }

    private void DestroyProjectile()
    {
        player.RemoveShot();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player_Team player_TeamTouch = other.gameObject.GetComponent<Player_Team>();

            if (player_Team.team != player_TeamTouch.team)
            {
                DestroyProjectile();
                Manager_Score.PlayerDeath(other.gameObject);

            }

        }
    }
}
