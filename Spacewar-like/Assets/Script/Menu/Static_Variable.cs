using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Static_Variable : MonoBehaviour
{
    public static GameObject[] player = new GameObject[2];
    public static Gamepad[] gamepad =  new Gamepad[2];
    public static string[] profilName = new string[2];
    public static Color[] profilColor = new Color[2];

    public static int winPoint;

    public  static bool son = true;

    public static void ResetVariable(int playerNumber)
    {
        player = new GameObject[playerNumber];
        gamepad = new Gamepad[playerNumber];
        profilName = new string[playerNumber];
        profilColor = new Color[playerNumber];
    }
}
