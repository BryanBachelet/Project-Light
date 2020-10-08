using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_ActiveSelection : MonoBehaviour
{
    public Image test ;

    private Vector2 inputAxis;

    private void OnEnable()
    {
        test.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        test.gameObject.SetActive(false);
    }

    public void Update()
    {
        
    }

  
}
