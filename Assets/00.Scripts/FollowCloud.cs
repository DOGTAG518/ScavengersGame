using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCloud : MonoBehaviour
{
    Transform TargetTransform;

    private void Start()
    {
        TargetTransform = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = TargetTransform.position;
    }
}
