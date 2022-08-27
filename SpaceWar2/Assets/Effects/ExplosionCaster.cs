using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCaster : MonoBehaviour
{
    [SerializeField] GameObject explosionObject;
    [ColorUsage(false, true)] public Color emissionColor;
    public Color explosionColor;
    public float explosionTime;
    public Vector2 explosionRange;
    // Start is called before the first frame update
    public void ExecuteExplosion()
    {
        ExecuteExplosion(transform.position);
    }
    public void ExecuteExplosion(Vector3 pos)
    {
        GameObject obj = Instantiate(explosionObject, pos, Quaternion.identity);
        IExplosion explosion = obj.GetComponent<IExplosion>();
        explosion.EmissionColor = emissionColor;
        explosion.ExplosionColor = explosionColor;
        explosion.ExplosionRange = explosionRange;
        explosion.ExplosionTime = explosionTime;
    }
}
