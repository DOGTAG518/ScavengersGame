using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashLight : MonoBehaviour
{
    public GameObject FlashLight;

    // Start is called before the first frame update
    void Start()
    {
        FlashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            TurnOnOffFlashLight();
    }

    void TurnOnOffFlashLight()
    {
        FlashLight.SetActive(!FlashLight.activeInHierarchy);
    }
}
