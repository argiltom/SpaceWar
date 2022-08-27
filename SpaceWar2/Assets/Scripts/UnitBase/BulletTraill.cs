using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer))]
public class BulletTraill : MonoBehaviour
{
    [SerializeField] Belong belong;
    TrailRenderer trailRenderer;
    [SerializeField] Gradient Players;
    [SerializeField] Gradient Enemy1;
    [SerializeField] Gradient Enemy2;
    [SerializeField] Gradient Enemy3;
    [SerializeField] Gradient Enemy4;
    List<Gradient> belongGradients = new List<Gradient>();
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        belongGradients.Add(Players);
        belongGradients.Add(Enemy1);
        belongGradients.Add(Enemy2);
        belongGradients.Add(Enemy3);
        belongGradients.Add(Enemy4);
        trailRenderer.colorGradient = belongGradients[(int)belong.belongEnum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
