using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class BelongColorSetter : MonoBehaviour
{
    [SerializeField] Belong belong;
    [SerializeField] Color Players;
    [SerializeField] Color Enemy1;
    [SerializeField] Color Enemy2;
    [SerializeField] Color Enemy3;
    [SerializeField] Color Enemy4;
    List<Color> belongColors= new List<Color>();
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        belongColors.Add(Players);
        belongColors.Add(Enemy1);
        belongColors.Add(Enemy2);
        belongColors.Add(Enemy3);
        belongColors.Add(Enemy4);
        meshRenderer.material.color = belongColors[(int)belong.belongEnum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BelongColorCopySet(MeshRenderer meshRenderer) {
        meshRenderer.material.color = belongColors[(int)belong.belongEnum];
    }
    
}
