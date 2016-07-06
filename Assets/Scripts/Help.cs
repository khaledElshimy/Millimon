using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Help : MonoBehaviour {
    public AudioSource audio;
    public Button mute;
    public Button sound;
    public void Back2Menu()
    {
        SceneManager.LoadScene("Start");

    }
    public void Mute()
    {
        sound.gameObject.SetActive(false);
        mute.gameObject.SetActive(true);

        audio.mute = true;
    }
    public void Sound()
    {
        sound.gameObject.SetActive(true);
        mute.gameObject.SetActive(false);
        audio.mute = false;

    }
    public void con()
    {
        Debug.Log("ccoooonnn");
        Quizz.continuGame = true;
    }
}
