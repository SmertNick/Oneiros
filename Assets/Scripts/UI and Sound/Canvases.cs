using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/Canvases")]
public class Canvases : ScriptableObject
{
    [SerializeField] private GameObject mainMenu;
    public GameObject MainMenu => mainMenu;
    
    [SerializeField] private GameObject pauseMenu;
    public GameObject PauseMenu => pauseMenu;

    [SerializeField] private GameObject transitionScreen;
    public GameObject TransitionScreen => transitionScreen;

    [SerializeField] private GameObject[] hUDCanvases;
    public GameObject[] HUDCanvases => hUDCanvases;
}
