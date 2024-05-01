using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    [SerializeField] public Text Answer;
    public string Real = "1234";
    public KeyPad_Door door;

    public void Number(int n)
    {
        if(Answer.text == "Nop")
        {
            Answer.text = "";
        }
        Answer.text += n.ToString();
    }

    public void Execute()
    {
        if(Answer.text == Real)
        {
            Answer.text = "Correct";
            door.CmdOpenDoor();
        }
        else
        {
            Answer.text = "Nop";
            
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
