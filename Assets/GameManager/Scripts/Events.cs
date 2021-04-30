using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    [System.Serializable] public class EventVolumeChange: UnityEvent<UnityEngine.UI.Slider, UnityEngine.UI.Slider> { }
    [System.Serializable] public class EventFontChange: UnityEvent<UnityEngine.UI.Slider, UnityEngine.UI.Slider> { }
}
