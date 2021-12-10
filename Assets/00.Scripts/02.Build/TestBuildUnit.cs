using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuildUnit : MonoBehaviour
{
    TestBuild testBuild;

    public Transform[] LegPoints;
    public GameObject[] LegObjects;
    public Transform TopObj;

    bool CantBuild = false;

    private void Start()
    {
        testBuild = transform.GetComponentInParent<TestBuild>();
    }

    private void Update()
    {
        CalcLegLength();
    }

    public Vector3 ReturnTopObjScale()
    {
        return TopObj.localScale;
    }

    // 다리 길이 계산
    void CalcLegLength()
    {
        for(int i = 0; i < LegPoints.Length; i++)
        {
            Ray ray = new Ray(LegPoints[i].position, Vector3.down);

            var rayHit = Physics.Raycast(ray, out RaycastHit hitInfo, 5f, testBuild.mask);

            Vector3 hitPoint;

            if (rayHit)
            {
                hitPoint = hitInfo.point;
            }
            else
            {
                hitPoint = LegPoints[i].position + (Vector3.down * 5);
            }

            var distance = Vector3.Distance(LegPoints[i].position, hitPoint);

            LegObjects[i].transform.parent = LegPoints[i];
            LegObjects[i].transform.localPosition = new Vector3(0, -(distance / 2), 0);
            var scaleTemp = LegObjects[i].transform.localScale;
            scaleTemp.y = distance;
            LegObjects[i].transform.localScale = scaleTemp;
        }
    }
}
