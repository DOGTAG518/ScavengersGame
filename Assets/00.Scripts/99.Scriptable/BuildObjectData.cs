using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildData
{
    public int itemID;
    public string ItemName;
    public GameObject itemPref;
}

[CreateAssetMenu]
public class BuildObjectData : ScriptableObject
{
    public List<BuildData> builddatas = new List<BuildData>();

    public BuildData GetItem(int itemID)
    {
        return builddatas.Find(x => x.itemID == itemID);
    }
}
