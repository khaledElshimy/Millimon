using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class FixArabic : MonoBehaviour
{
    public Text[] uiElemts;

    // Use this for initialization
    void Start()
    {
        foreach (Text item in uiElemts)
        {
            if (item ==null)
            {
                continue;
            }
            item.text = ArabicFixer.Fix(item.text);


        }
    }
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);

    }
    public static string flipFont(string str, int numberOfAlphabetsInSingleLine )
    {
        string individualLine = ""; //Control individual line in the multi-line text component.

        List<string> listofWords = str.Split(' ').ToList(); //Extract words from the sentence

        foreach (string s in listofWords)
        {

            if (individualLine.Length >= numberOfAlphabetsInSingleLine)
            {
                str += Reverse(individualLine) + "\n"; //Add a new line feed at the end, since we cannot accomodate more characters here.
                individualLine = ""; //Reset this string for new line.
            }

            individualLine += s + " ";
            Debug.Log("individualLine " + str);
        }
        return str;

    }

    // Update is called once per frame
    void Update()
{

}
}
