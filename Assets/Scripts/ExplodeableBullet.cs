using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeableBullet : Bullet
{
    protected override void Explode()
    {
        Debug.LogError("explode");
    }
}
