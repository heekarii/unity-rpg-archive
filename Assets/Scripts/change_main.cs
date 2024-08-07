using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerMain1 : MonoBehaviour
{
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("main");
    }
}
