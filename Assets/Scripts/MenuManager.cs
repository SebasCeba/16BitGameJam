using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject credits;
    [SerializeField] private Button backButton;
    [SerializeField] private Button startButton;


    void Start()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }
}
