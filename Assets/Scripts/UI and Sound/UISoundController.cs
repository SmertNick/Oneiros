using UnityEngine;

[CreateAssetMenu(menuName = "Oneiros/UI Sound Controller")]
public class UISoundController : ScriptableObject
{
    [SerializeField] private UISoundSet[] sets;
    public UISoundSet[] Sets => sets;

}
