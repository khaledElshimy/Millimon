using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Quiz : MonoBehaviour {

	public void startQuizScene()
    {
        SceneManager.LoadScene("Quiz");
    }
    public void aboutScene()
    {
        SceneManager.LoadScene("About");
    }
    public void startScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
