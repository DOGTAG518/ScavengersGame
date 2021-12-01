using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestStartManager : MonoBehaviour
{
    public void GoToTestScene()
    {
        SceneManager.LoadScene(1);
    }
}
