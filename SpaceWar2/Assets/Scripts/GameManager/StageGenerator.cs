using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct GenSet
{
    public BelongEnum belongEnum;
    public Vector3 minVecPos;
    public Vector3 maxVecPos;
    public GameObject fleetObj;
    public int generateTime;
}
public class StageGenerator : MonoBehaviour
{
    public bool isStageGenerated;
    [SerializeField] List<GenSet> genSets; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StageGenerate()
    {
        foreach (GenSet set in genSets)
        {
            GenerateFleet(set);
        }
        isStageGenerated = true;
    }
    void GenerateFleet(GenSet genSet)
    {
        
        for(int i = 0; i < genSet.generateTime; i++)
        {
            Vector3 pos = RamdomVec(genSet.minVecPos, genSet.maxVecPos);
            GameObject fleetObj = Instantiate(genSet.fleetObj, pos, Quaternion.identity);
            Belong belong = fleetObj.GetComponent<Belong>();
            belong.belongEnum =genSet.belongEnum;
            ShipFleetSetter fleetSetter = fleetObj.GetComponent<ShipFleetSetter>();
            fleetSetter.GenerateFleet();
        }
    }
    Vector3 RamdomVec(Vector3 min,Vector3 max)
    {
        Vector3 ret;
        ret.x = Random.Range(min.x ,max.x);
        ret.y = Random.Range(min.y, max.y);
        ret.z = Random.Range(min.z, max.z);
        return ret;
    }
}
