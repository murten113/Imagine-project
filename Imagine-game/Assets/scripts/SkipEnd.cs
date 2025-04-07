using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SkipEnd : MonoBehaviour
{
    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
