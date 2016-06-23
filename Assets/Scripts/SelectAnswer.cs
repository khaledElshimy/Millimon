using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectAnswer : MonoBehaviour
{
    public AudioClip rightAnswerClip;
    public AudioClip wrongAnswerClip;
    public AudioSource audio;
    public GameObject lastPopup;
    public Button[] btns;
    public void OnDown()
    {
        string currentAnswer = Quizz.currentAnswer;
        string answer = "";
        for (int i = 3; i >=0; i--)
        {


            answer += this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().text;
        }
        answer.Trim();

        btns[0].enabled = false;
        btns[1].enabled = false;
        btns[2].enabled = false;

        StartCoroutine(selectAnswer(answer, currentAnswer));
    }
    IEnumerator rightAnswer()
    {
        audio.clip = rightAnswerClip;
        audio.Play();
        this.gameObject.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<Image>().color = Color.white;
        if (Quizz.quizCounter == 50)
        {
            lastPopup.gameObject.SetActive(true);
            lastPopup.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            lastPopup.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "$" + Quizz.fixedRased;
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Start");
        }
        else
            Quizz.next = true;
    }
    IEnumerator wrongAnswer()
    {
        audio.clip = wrongAnswerClip;
        audio.Play();
        this.gameObject.GetComponent<Image>().color = Color.red;
        this.gameObject.transform.parent.GetChild(Quizz.currentTxt).GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(3f);
        lastPopup.gameObject.SetActive(true);
        lastPopup.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        lastPopup.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "SAR " + Quizz.fixedRased;
        yield return new WaitForSeconds(3f);
        this.gameObject.GetComponent<Image>().color = Color.white;
        lastPopup.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        lastPopup.gameObject.SetActive(false);
        Quizz.saveAndExit = true;

       
    }
    IEnumerator selectAnswer(string _answer, string _rightAnswer)
    {
         StartCoroutine(onOff());
        yield return new WaitForSeconds(10f);      
        if (_rightAnswer == _answer)
        {
            StartCoroutine(rightAnswer());
        }
        else
        {
            StartCoroutine(wrongAnswer());
        }
    }
    IEnumerator onOff()
    {
        int i = 0;
        while (i != 3)
        {
            this.gameObject.GetComponent<Image>().color = Color.yellow;
            yield return new WaitForSeconds(1f);
            this.gameObject.GetComponent<Image>().color = Color.gray;
            yield return new WaitForSeconds(1f);
            i++;
        }
        this.gameObject.GetComponent<Image>().color = Color.yellow;
    }
    public void continueGame()
    {
        Quizz.continuGame = true;
    }
}
