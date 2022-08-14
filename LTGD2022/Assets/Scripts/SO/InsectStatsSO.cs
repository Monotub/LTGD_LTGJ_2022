using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Insect Stats Object", menuName = "Stats/Insect Stats")]
public class InsectStatsSO : ScriptableObject
{
    public Sprite Portrait;
    public GameObject Prefab;
    public string DisplayName;
    public int EssenceCost;
    [Multiline(2)]
    public string AbilityDesc;
    [Multiline(3)]
    public string Description;
}
