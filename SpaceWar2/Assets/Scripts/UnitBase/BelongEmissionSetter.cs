using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongEmissionSetter : MonoBehaviour
{
    [SerializeField] Belong belong;
    [ColorUsage(false, true)] public Color Players;
    [ColorUsage(false, true)] public Color Enemy1;
    [ColorUsage(false, true)] public Color Enemy2;
    [ColorUsage(false, true)] public Color Enemy3;
    [ColorUsage(false, true)] public Color Enemy4;
    List<Color> belongColors = new List<Color>();
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
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.EnableKeyword("_EMISSION");//参考 https://nopitech.com/2019/01/29/post-962/
        meshRenderer.material.SetColor("_EmissionColor", belongColors[(int)belong.belongEnum]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BelongColorCopySet(MeshRenderer meshRenderer)
    {
        meshRenderer.material.EnableKeyword("_EMISSION");//参考 https://nopitech.com/2019/01/29/post-962/
        meshRenderer.material.SetColor("_EmissionColor", belongColors[(int)belong.belongEnum]);
    }
}
