using UnityEngine;
using System.Collections;

public class Answer : MonoBehaviour
{

    public string answer;
    public bool right;
    public Answer(string _answer, bool _right)
    {
        answer = _answer;
        right = _right;
    }
}
