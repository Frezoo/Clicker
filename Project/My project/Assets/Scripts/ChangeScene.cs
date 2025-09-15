using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeActiveScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
