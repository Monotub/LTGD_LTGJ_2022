using UnityEngine;


[SelectionBase]
public class Ladybug : Insect
{
    public int bonusEssence {get; private set;}

    private new void Start()
    {
        base.Start();
        bonusEssence = 25;
    }

}
