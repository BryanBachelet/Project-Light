using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOSpawn : MonoBehaviour
{
    public GameObject particule;

    public GameObject active;
    public float timing;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = timing;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > timing)
        {
            if (active != null)
            {
                Destroy(active);
            }
                active = Instantiate(particule, transform.position, transform.rotation);
                counter = 0;
        }
        else
        {
            if (active != null)
            {
                active.transform.position += active.transform.forward * 10  * Time.deltaTime;
            }
            counter += Time.deltaTime;
        }


    }
}
