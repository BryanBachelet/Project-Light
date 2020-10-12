using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Events;

public class Menu_ActiveSelection : MonoBehaviour
{
    public GameObject root;
    public MultiplayerEventSystem eventSystem;

    public int indexPlayer;

    public Image imagePlayer;
    public Text profilname;
    public Text press;

    public Menu_PlayerInput playerInput;
    public PlayerInput player;
    public Menu_ScreenSelection screenSelection;
    public Vector2 inputAxis;
    public int index;
    public bool resetInput;
    public bool ready;

    private float axisReset;
    private int frame;

   

    public void Update()
    {
      //  indexPlayer = screenSelection.CheckIndex();
       
    }

    public void Ready(InputAction.CallbackContext ctx)
    {
        if (this.enabled && screenSelection != null)
        {

            if (!ready)
            {
                screenSelection.SendReady();
                ready = true;
                press.gameObject.SetActive(false);
                Static_Variable.profilName[indexPlayer] = screenSelection.nameProfil[index];
                Static_Variable.profilColor[indexPlayer] = screenSelection.colors[index];
            }
        }
    }

    public void ReturnMenu(InputAction.CallbackContext ctx)
    {
        if (!ready)
        {
            playerInput.ReturnGameMode();
        }
    }

    public void Unready(InputAction.CallbackContext ctx)
    {
        if (ready)
        {
            screenSelection.SendUnready();
            ready = false;
            press.gameObject.SetActive(true);
        }
    }


    public void SetRoot(GameObject root)
    {
        this.root = root;
        eventSystem.playerRoot = root;
        Menu_SelectionInformation menu_SelectionInformation = this.root.GetComponent<Menu_SelectionInformation>();
        imagePlayer = menu_SelectionInformation.playerImage;
        press = menu_SelectionInformation.pressText;
        profilname = menu_SelectionInformation.profilName;
        screenSelection = menu_SelectionInformation.screenSelection;
        imagePlayer.gameObject.SetActive(true);
        profilname.gameObject.SetActive(true);
        profilname.text = screenSelection.nameProfil[index];
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

                    index = IndexLoop(index, screenSelection.nameProfil.Length, true);
                    profilname.text = screenSelection.nameProfil[index];
                }
                if (axis < -0.2f)
                {

                    index = IndexLoop(index, screenSelection.nameProfil.Length, false);
                    profilname.text = screenSelection.nameProfil[index];

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
