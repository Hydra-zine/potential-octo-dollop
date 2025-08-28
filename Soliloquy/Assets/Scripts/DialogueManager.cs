using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Queue<string> sentances;
    void Start()
    {
        sentances = new Queue<string>();
    }

}
