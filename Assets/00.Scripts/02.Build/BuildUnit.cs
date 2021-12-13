using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUnit : MonoBehaviour
{
    protected Collider[] AllCollider = null;

    protected void SetCollider()
    {
        AllCollider = GetComponentsInChildren<Collider>();

        for (int i = 0; i < AllCollider.Length; i++)
            AllCollider[i].enabled = false;
    }

    public virtual void EndBuild()
    {
        for (int i = 0; i < AllCollider.Length; i++)
            AllCollider[i].enabled = true;
    }

    // Start is called before the first frame update
    public virtual Vector3 ReturnTopObjScale()
    {
        return new Vector3();
    }
}
