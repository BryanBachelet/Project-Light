using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_MenuInput : MonoBehaviour
{
    public Manager_Score manager;

   public void InputMenu(InputAction.CallbackContext ctx)
    {
        manager.ChangeState(Manager_Score.StateOfGame.Pause);
    }
}
