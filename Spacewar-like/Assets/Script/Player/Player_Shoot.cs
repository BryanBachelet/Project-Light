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
    [SerializeField]
    private bool activeTir;

    public float timeBtwShoot;
    public float timeEcouleShoot;
    public bool addBullet;
    public void Update()
    {
        if (activeTir)
        {

            if(timeEcouleShoot >= timeBtwShoot)
            {
                AddShot();
                if(addBullet)
                {
                    addBullet = false;
                    activeTir = false;

                }

                if(activeTir)
                {
                    addBullet = true;
                }

                timeEcouleShoot = 0;

            }
            else
            {
                timeEcouleShoot += Time.deltaTime;
            }

        }
    }
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (projectileNumberAlive < maxProjectileNumber && ctx.performed)
        {
            if (!activeTir)
            {
                if(!addBullet)
                {
                    activeTir = true;
                    return;
                }
                timeEcouleShoot = timeBtwShoot;
                return;
            }
            else
            {
                if(addBullet)
                {
                    return;
                }
            }
            GameObject bullet = Instantiate(projectileShoot, transform.position + instantiatePos, Quaternion.identity);
            Projectile_Behavior currentProjectile = bullet.GetComponent<Projectile_Behavior>();
            currentProjectile.lifetime = lifetimeOfProjectile;
            currentProjectile.speedProjectile = speedProjectile;
            currentProjectile.player = this;
            currentProjectile.player_Team = GetComponent<Player_Team>();
            currentProjectile.direction = transform.forward;
            projectileAlive.Add(bullet);
            Debug.Log(activeTir);


            activeTir = true;

            Debug.Log(activeTir);

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
