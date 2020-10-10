using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFiledLife : MonoBehaviour
{
    public static float lifeDuration = 2;
    float tempsEcouleLife = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleLife += Time.deltaTime;
        if(tempsEcouleLife > lifeDuration)
        {
            Destroy(gameObject);
        }
    }
}
