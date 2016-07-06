using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Quizz : MonoBehaviour
{
    public AudioClip theme;
    public Button del2anss;
    public Button changeQuest;
    public Button passQuest;
    public static int fixedRased = 0;
    public static int raseed = 0;
    public Text raseedTxt;
    public Text quizTxt;
    public Text quizTxt2;
    public Text quizTxt3;
    public Text quizTxt4;
    public Button[] btns;

    public Text questCounter;
    public GameObject[] answerTxt;
 
    public string selectedAnswer;
    public List<Question> QuestList;
    public static string currentAnswer = "";
    public static int quizCounter ;
    public static bool next;
    public static bool saveAndExit;
    public static bool continuGame;
    public static int currentTxt = 0;
    public int numberOfAlphabetsInSingleLine = 4;

    Color new_color;
    AudioSource audio;
    IEnumerator Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
        quizCounter = 1;
        // Use this for initialization
        QuestList = new List<Question>();
       // quizCounter = PlayerPrefs.GetInt("QCounter");
      //  quizCounter = quizCounter == 0 ? 1 : quizCounter;
     //   fixedRased = PlayerPrefs.GetInt("FRased", fixedRased);
     //   raseed = fixedRased;
     //   raseedTxt.text = ""+raseed;
             Color col = new Color();
         ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("color"), out col);
        new_color = col;
        this.gameObject.GetComponent<Image>().color = new_color;
        this.gameObject.transform.parent.gameObject.GetComponent<Image>().color = new_color;

        string json = Resources.Load<TextAsset>("questions").text;
        //  JsonData jsonBooks = JsonMapper.ToObject(jsn);
        //Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
        yield return json;
        if (json != null)
        {
            //Sucessfully loaded the JSON string
            Debug.Log("Loaded following JSON string" + json);
            //Process books found in JSON file
        }
        else
        {
            Debug.Log("ERROR: " + json);
        }
        ProcessQuest(json);
    }
    void Update()
    {
        if (next)
        {
            next = false;
            Debug.Log("next");
            quizTxt4.text = "";
            quizTxt3.text = "";
            quizTxt2.text = "";
            quizTxt.text = "";
            btns[0].enabled = true;
            btns[1].enabled = true;
            btns[2].enabled = true;
            raseed += 40000;
            quizCounter++;
            if (quizCounter % 5 == 1)
            {

                new_color = newColor();
                this.gameObject.GetComponent<Image>().color = new_color;
                this.gameObject.transform.parent.gameObject.GetComponent<Image>().color = new_color;
                PlayerPrefs.SetString("color", ColorUtility.ToHtmlStringRGB(new_color));
                fixedRased = raseed;

            }
            questCounter.text = "" + quizCounter;

            raseedTxt.text = "SAR " + raseed;
            displayQuestion();
        }
        if (saveAndExit)
        {
            saveAndExit = false;
           // quizCounter -= (quizCounter % 5);
           // quizCounter += 1;
            raseed = fixedRased;
            raseedTxt.text = "SAR " + fixedRased;
            if (continuGame)
            {
                continuGame = false;
                
                for (int i = 0; i < 3; i++)
                {
                    answerTxt[i].transform.parent.gameObject.GetComponent<Image>().color = Color.white;
                    answerTxt[i].transform.parent.transform.GetChild(2).gameObject.GetComponent<Button>().enabled = true;

                }
                audio.clip = theme;
                audio.Play();
            }
            else
                SceneManager.LoadScene("Start");

            /*   PlayerPrefs.SetInt("QCounter",quizCounter);
               PlayerPrefs.SetInt("FRased", fixedRased);*/
        }


    }
    private void ProcessQuest(string jsonString)
    {
        JsonData jsonQuest = JsonMapper.ToObject(jsonString);
        Debug.Log("jsonQuest Count" + jsonQuest["questions"].Count);
        for (int i = 0; i < jsonQuest["questions"].Count; i++)
        {
            Question quest = new Question();
            quest.quest = jsonQuest["questions"][i]["question"].ToString();

            quest.answers = new List<Answer>();
            Debug.Log(jsonQuest["questions"][i]["answers"]["answer"].Count);
            for (int j = 0; j < jsonQuest["questions"][i]["answers"]["answer"].Count; j++)
            {
                Debug.Log(jsonQuest["questions"][i]["answers"]["answer"][j]["ans"]);
                string answer = jsonQuest["questions"][i]["answers"]["answer"][j]["ans"].ToString();
                bool right = Convert.ToBoolean(jsonQuest["questions"][i]["answers"]["answer"][j]["right"].ToString());
                Answer ans = new Answer(answer, right);
                quest.answers.Add(ans);
            }
            QuestList.Add(quest);
            //      LoadQuest(quest);
        }
        questCounter.text = "" + quizCounter;
        displayQuestion();
    }

    void displayQuestion()
    {
        audio.clip = theme; ;
        audio.Play();
        Question q = QuestList[quizCounter - 1];// generateRandomquestion(QuestList);
        if (q == null)
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            q.answered = true;
            string qus = ArabicFixer.Fix(q.quest);
            string[] lines = qus.Split(null);
            if (lines.Length < 7 )
            {
                quizTxt.text = qus;
            }
            else
            {
                quizTxt.text = string.Empty;
                string s = "";
                int j = 0;
               // Array.Reverse(lines);
                for (int k = 0; k < lines.Length; k++)
                {
                    Debug.Log(lines[k] + " " + k);
                    quizTxt.text += lines[k];
                    quizTxt.text += " ";

                    if (k % 5 ==0 && k!=0)
                    {
                        j++;
                        //quizTxt.text += "\n";
                        if (j==1 && s!=null)
                        {
                            quizTxt2.text = quizTxt.text;
                            quizTxt.text="";
                        }
                        else if (j == 2 && s != null)
                        {
                            quizTxt3.text = quizTxt2.text;
                            quizTxt2.text = quizTxt.text;
                            quizTxt.text = "";

                        }
                        else if (j == 3 && s != null)
                        {
                            quizTxt4.text = quizTxt3.text;
                            quizTxt3.text = quizTxt2.text;
                            quizTxt2.text = quizTxt.text;
                            quizTxt.text = "";
                        }

                    }
                }
            }
            q.answers.Shuffle();
            for (int i = 0; i < 3; i++)
            {
            
                Text anpart1 = answerTxt[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                Text anpart2 = answerTxt[i].transform.GetChild(1).gameObject.GetComponent<Text>();
                Text anpart3 = answerTxt[i].transform.GetChild(2).gameObject.GetComponent<Text>();
                Text anpart4 = answerTxt[i].transform.GetChild(3).gameObject.GetComponent<Text>();
                anpart1.gameObject.SetActive(true);
                anpart2.gameObject.SetActive(false);
                anpart3.gameObject.SetActive(false);
                anpart4.gameObject.SetActive(false);
                anpart1.text="";
                anpart2.text="";
                anpart3.text="";
                anpart4.text = "";
                string[] line = ArabicFixer.Fix(q.answers[i].answer).Split(null);
                if (line.Length < 4)
                {
                    anpart1.text = ArabicFixer.Fix(q.answers[i].answer);
                    if (q.answers[i].right)
                    {
                        currentTxt = i;
                        currentAnswer = ArabicFixer.Fix(q.answers[i].answer);
                    }
                }
                else
                {
                    anpart1.gameObject.SetActive(true);
                    anpart2.gameObject.SetActive(false);
                    anpart3.gameObject.SetActive(false);
                    anpart4.gameObject.SetActive(false);
                    int j = 0;
                    for (int k = 0; k < line.Length; k++)
                    {

                        anpart1.text += line[k];
                        anpart1.text += " ";
                       if (k % 3 == 0 && k != 0)
                        {
                            j++;
                            //quizTxt.text += "\n";
                            if (j == 1 )
                            {
                                anpart2.gameObject.SetActive(true);
                                anpart2.text = anpart1.text;
                                anpart1.text = "";
                            }
                            else if (j == 2 )
                            {
                                anpart3.gameObject.SetActive(true);
                                anpart3.text = anpart2.text;
                                anpart2.text = anpart1.text;

                                anpart1.text = "";

                            }
                            else if (j == 3)
                            {
                                anpart4.gameObject.SetActive(true);
                                anpart4.text = anpart3.text;
                                anpart3.text = anpart2.text;
                                anpart2.text = anpart1.text;
                                anpart1.text = "";

                            }   
                                              
                        }
                        if (line.Length%4==0)
                        {
                            anpart1.gameObject.SetActive(false);
                        }
                            if (q.answers[i].right)
                        {
                            currentTxt = i;
                            currentAnswer = "";
                            for (int l = 3; l >= 0; l--)
                            {
                                currentAnswer += answerTxt[i].gameObject.transform.GetChild(l).gameObject.GetComponent<Text>().text;
                            }

                            currentAnswer.Trim();

                        }

                    }
             
                   
                }
                // answersTxt[i].text = ArabicFixer.Fix(q.answers[i].answer);
               
            }
        }
    }


  
    Color newColor()
    {
        Color newColor = new Color();
        System.Random random = new System.Random();
        String color = String.Format("#{0:X6}", random.Next(0x1000000));
        ColorUtility.TryParseHtmlString(color, out newColor);
        return newColor;
    }

    public void del2Ans()
    {
        int c = 0;
        for (int i = 0; i < 3; i++)
        {
     
            if (currentTxt != i)
            {
                c++;

                if (c <= 2)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        answerTxt[i].gameObject.transform.GetChild(l).gameObject.GetComponent<Text>().text = "";
                    }
                }
                else
                    break;
            }

        }
        del2anss.GetComponent<Image>().color = Color.gray;
        del2anss.enabled = false;

    }
    public void chgQuestion()
    {
        for (int i = quizCounter; i < QuestList.Count; i++)
        {
            Question q = QuestList[i-1];
            QuestList[i - 1] = QuestList[i];
            QuestList[i] = q;

        }
        Debug.Log("last q"+QuestList[49].quest);
        displayQuestion();
        changeQuest.GetComponent<Image>().color = Color.gray;
        changeQuest.enabled = false;
    }
    public void passQuestion()
    {
        next = true;
        passQuest.GetComponent<Image>().color = Color.gray;
        passQuest.enabled = false;
    }

}
