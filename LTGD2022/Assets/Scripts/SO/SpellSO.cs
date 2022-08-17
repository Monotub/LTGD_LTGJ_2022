using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Spell Object", menuName = "Spell Object")]
public class SpellSO : ScriptableObject
{
    public int cost;
    public float value;
    public float duration;
    public float radius;
    public string displayName;
    public string description;
}
