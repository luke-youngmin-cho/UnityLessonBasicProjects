using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMover : MonoBehaviour
{
    public void MoveSceneTo(string name)
    {
        SceneManager.LoadScene("name");
    }
    public void MoveSceneTo(int index)
    {
        SceneManager.LoadScene(index);
    }
}
