using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplosion 
{
    Color ExplosionColor
    {
        set;
    }
    Color EmissionColor
    {
        set;
    }
    float ExplosionTime
    {
        set;
    }
    Vector2 ExplosionRange
    {
        set;
    }
}
