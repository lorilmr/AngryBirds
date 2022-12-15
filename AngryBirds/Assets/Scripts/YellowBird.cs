using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : BirdControl
{
    public override void ShowSkills()
    {
        base.ShowSkills();
        rigid.velocity *= 2;
    }
}
