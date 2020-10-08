using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Menu_ActiveSelection : MonoBehaviour
{
    public Image test;
    public Text text;

    public Menu_ScreenSelection screenSelection;
    public Vector2 inputAxis;
    public int index;
    public bool resetInput;
    public bool ready;

    private float axisReset;
    private int frame;

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

    public void Ready(InputAction.CallbackContext ctx)
    {
        if (this.enabled && screenSelection!= null)
        {
         
            if (!ready)
            {
                screenSelection.SendReady();
                ready = true;
                text.gameObject.SetActive(false);
            }
        }
    }


    public void Unready(InputAction.CallbackContext ctx)
    {
        if (ready)
        {
            screenSelection.SendUnready();
            ready = false;
            text.gameObject.SetActive(true);
        }
    }


    public void ChangeShip(InputAction.CallbackContext ctx)
    {
        if (!ready)
        {
            float axis = ctx.ReadValue<float>();
            axisReset = axis;

            if (resetInput)
            {
                if (axis > 0.2f)
                {

                    index = IndexLoop(index, screenSelection.colors.Length, true);
                    test.color = screenSelection.colors[index];
                }
                if (axis < -0.2f)
                {

                    index = IndexLoop(index, screenSelection.colors.Length, false);
                   test.color = screenSelection.colors[index];

                }
                resetInput = false;
            }
            if (axisReset > -0.20f && axisReset < 0.20f)
            {
                resetInput = true;
            }
        }

    }

    public int IndexLoop(int index, int lengthArray, bool positif)
    {
        if (positif)
        {
            if (index == lengthArray - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
        else
        {
            if (index == 0)
            {
                index = lengthArray - 1;
            }
            else
            {
                index--;
            }

        }
        return index;
    }


}
