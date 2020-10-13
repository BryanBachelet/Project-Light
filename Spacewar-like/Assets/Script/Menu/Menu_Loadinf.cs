using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Loadinf : MonoBehaviour
{
    public float timer;
    private float timerCountdown;

    public int indexLoading;

    void Update()
    {
        if (this.enabled)
        {
            if (timerCountdown > timer)
            {
                SceneManager.LoadScene(indexLoading);
            }
            else
            {

                timerCountdown += Time.deltaTime;
            }
        }
    }
}
