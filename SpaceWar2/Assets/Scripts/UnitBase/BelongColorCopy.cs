using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongColorCopy : MonoBehaviour
{
    [SerializeField] BelongColorSetter belongColorSetter;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        belongColorSetter.BelongColorCopySet(meshRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
