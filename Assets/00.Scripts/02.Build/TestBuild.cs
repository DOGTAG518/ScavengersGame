using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetPosition
{
    Vector3 startPosition = new Vector3();
    public List<Vector3> stepPositions = new List<Vector3>();
    public List<TestBuildUnit> unit = new List<TestBuildUnit>();

    public SetPosition() { }
    public SetPosition(Vector3 s_position)
    {
        startPosition = s_position;
    }

    public void SavePosition(Vector3 targetPosition, TestBuildUnit originUnit)
    {
        CalcPosition(targetPosition, originUnit);
    }

    public void CalcPosition(Vector3 targetPosition, TestBuildUnit originUnit)
    {
        var originScale = originUnit.ReturnTopObjScale();
        var distance = Vector3.Distance(startPosition, targetPosition);
        var direction = (targetPosition - startPosition).normalized;

        stepPositions.Clear();

        var objCount = (int)(distance / originScale.x);
        float objRemain = distance - (objCount * originScale.x);

        float numDistance = (objRemain / objCount) + originScale.x;

        for (int i = 0; i < objCount; i++)
        {
            var newPosition = startPosition + (direction * numDistance * i);

            stepPositions.Add(newPosition);
        }

        if (unit.Count < objCount)
        {
            int countTemp = unit.Count;

            for (int i = 0; i < objCount - countTemp; i++)
                unit.Add(GameObject.Instantiate(originUnit, originUnit.transform.parent));
        }
        else if (unit.Count > objCount && objCount > 0)
        {
            for (int i = objCount - 1; i < unit.Count; i++)
                unit[i].gameObject.SetActive(false);
        }

        if (objCount == 0)
        {
            if(unit.Count == 0)
                unit.Add(GameObject.Instantiate(originUnit, originUnit.transform.parent));

            unit[0].gameObject.SetActive(true);

            unit[0].transform.position = startPosition;
            unit[0].transform.LookAt(targetPosition, Vector3.up);

            var rotationTemp = unit[0].transform.localEulerAngles;
            rotationTemp.x = 0;
            rotationTemp.z = 0;

            unit[0].transform.localEulerAngles = rotationTemp;

        }
        else
        {
            for (int i = 0; i < objCount; i++)
            {
                unit[i].gameObject.SetActive(true);

                if (i < objCount - 1)
                {
                    unit[i].transform.LookAt(unit[i + 1].transform.position, Vector3.up);
                }
                else
                {
                    unit[i].transform.LookAt(targetPosition, Vector3.up);

                    var rotationTemp = unit[i].transform.localEulerAngles;
                    rotationTemp.x = 0;
                    rotationTemp.z = 0;

                    unit[i].transform.localEulerAngles = rotationTemp;
                }

                unit[i].transform.position = stepPositions[i];
            }
        }
    }
}

public class TestBuild : MonoBehaviour
{
    public TestBuildUnit OriginUnit;

    public List<SetPosition> positions = new List<SetPosition>();

    Camera playerCam;

    bool isBuild = false;
    public LayerMask mask;
    public int buildCursor = 0;

    // Start is called before the first frame update
    void Start()
    {
        isBuild = true;

        playerCam = Camera.main;
    }

    private void Update()
    {
        if (isBuild)
        {
            CalcBuildState();
        }
    }

    void CalcBuildState()
    {
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 rayDir = playerCam.transform.forward;

        Ray ray = new Ray(rayOrigin, rayDir);

        var rayHit = Physics.Raycast(ray, out RaycastHit hitInfo, 5f, mask);

        Vector3 hitPoint;

        if(rayHit)
        {
            hitPoint = hitInfo.point + Vector3.up;
        }
        else
        {
            hitPoint = rayOrigin + (rayDir * 5) + Vector3.up;
        }

        if (buildCursor > 0)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                // 저장된 포지션의 카운트가 2개 이상이고 마지막 포지션이 아닌 경우(마지막 포지션은 추가로 계산해야함)
                if(positions.Count > 1 && i < positions.Count - 1)
                {

                }

                positions[buildCursor - 1].CalcPosition(hitPoint, OriginUnit);
            }
        }
        else
        {
            transform.position = hitPoint;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (buildCursor > 0)
                positions[buildCursor - 1].SavePosition(hitPoint, OriginUnit);

            buildCursor++;
            OriginUnit.gameObject.SetActive(false);
            positions.Add(new SetPosition(hitPoint));
        }

        if(buildCursor > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                positions[buildCursor - 1].SavePosition(hitPoint, OriginUnit);

                isBuild = false;
            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        var setPosition = new SetPosition();
        setPosition.SavePosition(position, OriginUnit);
        positions.Add(setPosition);
    }

    public void EndBuild()
    {
        isBuild = false;
    }

    bool IsCanBuild()
    {
        return true;
    }

}