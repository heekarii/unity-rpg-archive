using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger1 : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("scene1");
    }
}
