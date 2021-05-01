using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolume, fxVolume;

    private void Start()
    {
        musicVolume.onValueChanged.AddListener(Events.ChangeMusicVolume);
        fxVolume.onValueChanged.AddListener(Events.ChangeFXVolume);
    }
    
    private void OnDestroy()
    {
        musicVolume.onValueChanged.RemoveListener(Events.ChangeMusicVolume);
        fxVolume.onValueChanged.RemoveListener(Events.ChangeFXVolume);
    }
}
