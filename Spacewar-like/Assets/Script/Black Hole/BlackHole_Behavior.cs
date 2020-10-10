using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Behavior : MonoBehaviour
{
    public GameObject[] player;
  
    public float distance;
    public float force = 10f ;

   
    void Update()
    {
        BlackHoleAttraction();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    public void BlackHoleAttraction()
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (Vector3.Distance(transform.position, player[i].transform.position)< distance)
            {
                Vector3 blackHoleDir = transform.position - player[i].transform.position;
                Rigidbody rigid = player[i].GetComponent<Rigidbody>();
                rigid.AddForce(blackHoleDir.normalized * force, ForceMode.Acceleration);
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Manager_Score.PlayerDeath(other.gameObject);
        }
    }


}
