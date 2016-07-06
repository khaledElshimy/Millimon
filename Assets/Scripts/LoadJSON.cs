using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class LoadJSON : MonoBehaviour
{
    public  List<Question> QuestList;
    IEnumerator Start()
    {
        QuestList = new List<Question>();
        //Load JSON data from a URL
        string json = File.ReadAllText(Path.Combine(Application.dataPath,"questions.json")); 
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

    //Converts a JSON string into Book objects and shows a book out of it on the screen
    private void ProcessQuest(String jsonString)
    {
        JsonData jsonQuest = JsonMapper.ToObject(jsonString);
        Debug.Log("jsonQuest Count"+ jsonQuest["questions"].Count);

    		for(int i = 0; i< jsonQuest["questions"].Count; i++)
		{
            Question quest = new Question();
	       quest.quest = jsonQuest["questions"][i]["question"].ToString();
            quest.answers = new List<Answer>();
            Debug.Log(jsonQuest["questions"][i]["answers"]["answer"].Count);
			for(int j = 0; j < jsonQuest["questions"][i]["answers"]["answer"].Count; j++)
			{
                Debug.Log(jsonQuest["questions"][i]["answers"]["answer"][j]["ans"]);
                string answer = jsonQuest["questions"][i]["answers"]["answer"][j]["ans"].ToString();
                bool right =Convert.ToBoolean(jsonQuest["questions"][i]["answers"]["answer"][j]["right"].ToString());
                Answer ans = new Answer(answer, right);
                quest.answers.Add(ans);
			}
            QuestList.Add(quest);
            // LoadQuest(quest);
         Debug.Log(QuestList[i]);
        }
	}

    //Finds book object in application and send the Book as parameter.
    //Currently only works with two books
    private void LoadQuest(Question quest)
    {
      
        GameObject questGameObject = GameObject.Find("questions" + quest.quest.ToString());
        questGameObject.SendMessage("LoadQuest", quest);
    }

}
