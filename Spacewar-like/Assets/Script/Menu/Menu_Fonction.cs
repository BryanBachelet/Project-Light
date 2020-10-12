using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu_Fonction : MonoBehaviour
{
    public EventSystem systemEvent;
    public GameObject MainScreen;

    public GameObject previousScreen;
    public GameObject currentScreen;

    public GameObject returnGameSelection;

    public void ActiveButton(GameObject active)
    {
        active.SetActive(true);
        currentScreen = active;

    }

    public void ResetScreenSelection(int i)
    {
        Static_Variable.ResetVariable(i);
    }

    public void DeactiveButton(GameObject Deactive)
    {
        Deactive.SetActive(false);
        previousScreen = Deactive;
    }

    public void CancelScreen(InputAction.CallbackContext ctx)
    {
        if (currentScreen != MainScreen  &&  ctx.performed )
        {
            Debug.Log("Passe");
            previousScreen.SetActive(true);
            currentScreen.SetActive(false);
            GameObject prev = previousScreen;
            GameObject curr = currentScreen;

            currentScreen = previousScreen;
            previousScreen = currentScreen.GetComponent<Menu_ArborescenInfo>().previousMenu;
            systemEvent.SetSelectedGameObject(null);
            systemEvent.SetSelectedGameObject(currentScreen.transform.GetChild(0).gameObject);
        }
    }


    public void SelectItem(GameObject itemSelect)
    {
        systemEvent.SetSelectedGameObject(itemSelect);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Reset(GameObject returnGO)
    {
        returnGO.SetActive(true);
        returnGameSelection.SetActive(false);
        SelectItem(returnGO.transform.GetChild(0).gameObject);
    }


}
