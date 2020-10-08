using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shoot : MonoBehaviour
{
    [Header("Parameter")]
    public GameObject projectileShoot;
    public Vector3 instantiatePos;
    public float speedProjectile;
    public int projectileNumberAlive;
    public int maxProjectileNumber;
    public float lifetimeOfProjectile;

    private List<GameObject> projectileAlive =  new List<GameObject>();
    private bool activeTir;
   

    
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (projectileNumberAlive < maxProjectileNumber && ctx.performed )
        {
            GameObject bullet = Instantiate(projectileShoot, transform.position + instantiatePos, Quaternion.identity);
            Projectile_Behavior currentProjectile = bullet.GetComponent<Projectile_Behavior>();
            currentProjectile.lifetime = lifetimeOfProjectile;
            currentProjectile.speedProjectile = speedProjectile;
            currentProjectile.player = this;
            currentProjectile.player_Team = GetComponent<Player_Team>();
            currentProjectile.direction = transform.forward;
            projectileAlive.Add(bullet);
            activeTir = true;
            AddShot();
            return;
        }
        

    }

    public void AddShot()
    {
        projectileNumberAlive++;
    }

    public void RemoveShot()
    {
        projectileNumberAlive--;
    }

    public void ResetGame()
    {
        projectileNumberAlive = 0;
        for (int i = 0; i < projectileAlive.Count; i++)
        {
            Destroy(projectileAlive[i]); 
          
        }
        projectileAlive = new List<GameObject>();
    }
    
    
}
