using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public void OpenLevelMenu(int levelId)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
