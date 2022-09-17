using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraRig : MonoBehaviour
{
    [SerializeField] GameObject CenterEyeAnchor;
    [SerializeField] GameObject leftOVAControllerPrefab;
    [SerializeField] GameObject rightOVAControllerPrefab;
    OVRInput.Controller rightController;
    OVRInput.Controller leftController;

    /// <summary>
    /// レバガチャ左
    /// </summary>
    [System.NonSerialized] public Vector2 controllerRotationOfLeft;
    /// <summary>
    /// レバガチャ右
    /// </summary>
    [System.NonSerialized] public Vector2 controllerRotationOfRight;

    [System.NonSerialized] public float leftPrimaryIndexTrigger;
    [System.NonSerialized] public float rightPrimaryIndexTrigger;
    [System.NonSerialized] public float leftPrimaryHandTrigger;
    [System.NonSerialized] public float rightPrimaryHandTrigger;
    RaycastHit rightRayhit;
    // Start is called before the first frame update
    void Start()
    {
        rightController = rightOVAControllerPrefab.GetComponent<OVRControllerHelper>().m_controller;
        leftController = leftOVAControllerPrefab.GetComponent<OVRControllerHelper>().m_controller;
    }

    // Update is called once per frame
    void Update()
    {
        controllerRotationOfLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, leftController);
        controllerRotationOfRight = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController);
        leftPrimaryIndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, leftController);
        rightPrimaryIndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, rightController);
        leftPrimaryHandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, leftController);
        rightPrimaryHandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, rightController);


        
        if (Physics.Raycast(rightOVAControllerPrefab.transform.position, rightOVAControllerPrefab.transform.forward, out rightRayhit, 20))
        {
            VRPushAbleUI ui = rightRayhit.collider.gameObject.GetComponent<VRPushAbleUI>();
            if (ui != null)
            {
                ui.Seeking();
                if(rightPrimaryHandTrigger > 0.7f|| rightPrimaryIndexTrigger > 0.7f)
                {
                    ui.Pushed();
                }
            }

        }
    }
}
    
        

