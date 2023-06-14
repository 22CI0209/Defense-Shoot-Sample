using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public enum NextSceneState
    {
        Title,
        Game,
        Result
    }
    [SerializeField] NextSceneState state;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(state.ToString());
        }
    }
}
