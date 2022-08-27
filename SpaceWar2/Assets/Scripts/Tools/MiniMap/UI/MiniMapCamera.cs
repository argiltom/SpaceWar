using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] Transform basePos;
    [SerializeField] Transform cameraPole;
    Transform mainCameraTransform;
    [SerializeField] float scaleRate;

    [SerializeField] Transform axisSet;
    [SerializeField] bool isFixCameraRotX;
    [SerializeField] bool isFixCameraRotY;
    [SerializeField] bool isFixCameraRotZ;
    [SerializeField] bool isFixCameraX;
    [SerializeField] bool isFixCameraY;
    [SerializeField] bool isFixCameraZ;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fixRotVector = new Vector3(1, 1, 1);
        if (isFixCameraRotX)
        {
            fixRotVector.x = 0;
        }
        if (isFixCameraRotY)
        {
            fixRotVector.y = 0;
        }
        if (isFixCameraRotZ)
        {
            fixRotVector.z = 0;
        }
        Vector3 rotVector = mainCameraTransform.rotation.eulerAngles;
        rotVector.x = rotVector.x * fixRotVector.x;
        rotVector.y = rotVector.y * fixRotVector.y;
        rotVector.z = rotVector.z * fixRotVector.z;

        cameraPole.rotation = player.transform.rotation;
        transform.rotation = Quaternion.Euler(rotVector);

        Vector3 fixVector = new Vector3(1, 1, 1);
        if (isFixCameraX)
        {
            fixVector.x = 0;
        }
        if (isFixCameraY)
        {
            fixVector.y = 0;
        }
        if (isFixCameraZ)
        {
            fixVector.z = 0;
        }
        
        Vector3 moveVector = new Vector3(0,0,0);
        
        moveVector.x = fixVector.x * player.transform.position.x * scaleRate + basePos.position.x;
        moveVector.y = fixVector.y * player.transform.position.y * scaleRate + basePos.position.y;
        moveVector.z = fixVector.z * player.transform.position.z * scaleRate + basePos.position.z;
        cameraPole.transform.position = moveVector;
        axisSet.position = player.transform.position * scaleRate + basePos.position;
    }
}
