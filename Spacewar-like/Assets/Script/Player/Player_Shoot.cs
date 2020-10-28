using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class Player_Shoot : MonoBehaviour
{
    [Header("Parameter")]
    public GameObject projectileShoot;
    public Vector3 instantiatePos;
    public float speedProjectile;
    public int projectileNumberAlive;
    //   public int maxProjectileNumber;
    public float lifetimeOfProjectile;

    private List<GameObject> projectileAlive = new List<GameObject>();
    [SerializeField]
    private bool activeTir;

    [FMODUnity.EventRef]
    public string ShootEvent = "";

    FMOD.Studio.EventInstance shootEvent;

    public float timeBtwShoot;
    public float timeEcouleShoot;
    public bool addBullet;

    public void Start()
    {
        shootEvent = FMODUnity.RuntimeManager.CreateInstance(ShootEvent);
    }

    public void Update()
    {
        if (activeTir)
        {

            if (timeEcouleShoot >= timeBtwShoot)
            {
                activeTir = false;
           
            }
            else
            {
                timeEcouleShoot += Time.deltaTime;
            }

        }
    }
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (this.enabled == true && ctx.performed)
        {
            if (activeTir)
            {
                return;
            }
            GameObject bullet = Instantiate(projectileShoot, transform.position + instantiatePos, transform.rotation);
            Projectile_Behavior currentProjectile = bullet.GetComponent<Projectile_Behavior>();
            currentProjectile.lifetime = lifetimeOfProjectile;
            currentProjectile.speedProjectile = speedProjectile;
            currentProjectile.player = this;
            currentProjectile.player_Team = GetComponent<Player_Team>();
            currentProjectile.direction = transform.forward;
            projectileAlive.Add(bullet);
            Debug.Log(activeTir);
            AddShot();
            shootEvent.start();
            activeTir = true;
            timeEcouleShoot = 0;

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
