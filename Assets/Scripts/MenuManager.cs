using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //public static MenuManager Instance { get; private set; }
    AudioManager audioManager;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject credits;
    [SerializeField] private Button backButton;
    [SerializeField] private Button startButton;


    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>(); 
    }
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
        audioManager.PlaySfx(audioManager.selected);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
        audioManager.PlaySfx(audioManager.selected);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }
    public void startScene()
    {
        GameManager.Instance.Startgame();
    }
}
