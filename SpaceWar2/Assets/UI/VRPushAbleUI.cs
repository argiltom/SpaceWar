using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRPushAbleUI : MonoBehaviour
{
    float seekedCount;
    bool isSeeked
    {
        get => seekedCount > 0;
    }
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Color nomalColor;
    [SerializeField] Color seekedColor;
    [SerializeField] UnityEvent executeFanc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(isSeeked)
        {
            meshRenderer.material.color = seekedColor;
            seekedCount -= 2*Time.deltaTime;
        }
        else
        {
            meshRenderer.material.color = nomalColor;
        }
    }
    public void Seeking()
    {
        seekedCount = 1;
    }
    public void Pushed()
    {
        executeFanc.Invoke();
    }
}
