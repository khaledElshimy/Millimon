using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Start");
	}
}
