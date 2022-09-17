using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipControllerVR : ControllerBase
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

    // Start is called before the first frame update
    void Start()
    {
        ControllerBaseStart();
        gameManager.playerIship = this;
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


        transform.position += CenterEyeAnchor.transform.forward * move.moveSpeed * controllerRotationOfLeft.y * Time.deltaTime;
        transform.position += CenterEyeAnchor.transform.right * move.moveSpeed * controllerRotationOfLeft.x * Time.deltaTime;


        //押しっぱなし
        //if (OVRInput.Get(OVRInput.Button.Two, rightController))
        //{
        //    weapons[0].Fire();
        //}
        //if (OVRInput.Get(OVRInput.Button.Two, leftController))
        //{
        //    weapons[1].Fire();
        //}
        if (rightPrimaryIndexTrigger>0.75f)
        {
            weapons[0].Fire();
        }
        if (leftPrimaryIndexTrigger > 0.75f)
        {
            weapons[1].Fire();
        }
    }

}
