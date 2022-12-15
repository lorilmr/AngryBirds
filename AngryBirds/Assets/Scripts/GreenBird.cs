using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : BirdControl
{
    public override void ShowSkills()
    {
        base.ShowSkills();
        Vector3 speed = rigid.velocity;
        speed.x *= -1;
        rigid.velocity = speed;
    }
}
