using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeableBullet : Bullet
{
    public override void Explode()
    {
        Debug.LogError("explode");
    }
}
