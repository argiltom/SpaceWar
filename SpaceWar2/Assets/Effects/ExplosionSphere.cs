using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSphere : MonoBehaviour,IExplosion
{
    [SerializeField] Vector2 sizeRange;
    [SerializeField] float time;
    Color explosionColor;
    Color emissionColor;
    float countTime;
    float offset;
    float diff;
    float rate=0;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(sizeRange.x, sizeRange.x, sizeRange.x);
        diff = sizeRange.y - sizeRange.x;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.EnableKeyword("_EMISSION");//参考 https://nopitech.com/2019/01/29/post-962/
        meshRenderer.material.SetColor("_EmissionColor", emissionColor);
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    IEnumerator Explosion()
    {
        
        while (countTime <= time)
        {
            countTime += 1f/60f;
            rate = countTime / time;
            offset = diff * rate;
            float temp = sizeRange.x + offset;

            gameObject.transform.localScale = new Vector3(temp, temp, temp);
            Color tempColor = explosionColor;
            meshRenderer.material.color = new Color(tempColor.r, tempColor.g, tempColor.b,1-rate);
            
            yield return null;
        }
        yield return null;
        Destroy(gameObject);
        
    }
    //imprementsInterface-----------------------------
    public Color ExplosionColor
    {
        set
        {
            explosionColor = value;
        }
    }
    public Color EmissionColor
    {
        set
        {
            emissionColor = value;
        }
    }
    public float ExplosionTime
    {
        set
        {
            time=value;
        }
    }
    public Vector2 ExplosionRange
    {
        set
        {
            sizeRange = value;
        }
    }
}
