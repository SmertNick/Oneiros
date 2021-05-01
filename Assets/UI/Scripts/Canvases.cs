using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/Canvases")]
public class Canvases : ScriptableObject
{
    [SerializeField] private GameObject mainMenu;
    public GameObject MainMenu => mainMenu;

    [SerializeField] private GameObject[] hUDCanvases;
    public GameObject[] HUDCanvases => hUDCanvases;
}
