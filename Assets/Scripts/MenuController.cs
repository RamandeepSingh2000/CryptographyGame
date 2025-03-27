using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _achievementsMenu;
    [SerializeField] private GameObject _overlay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartGame()
    {
        _overlay.SetActive(true);
        _mainMenu.SetActive(false);
        
        MenuEvents.OnGameStart?.Invoke();
    }

    public void OpenMenu()
    {
        _overlay.SetActive(false);
        _mainMenu.SetActive(true);
        _achievementsMenu.SetActive(false);
        MenuEvents.OnGoToMainMenu?.Invoke();
    }

    public void OpenAchievements()
    {
        _mainMenu.SetActive(false);
        _achievementsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
