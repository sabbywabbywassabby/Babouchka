using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsScripts : MonoBehaviour
{
    public GameObject footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.UpArrow))
        {
            footsteps();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            footsteps();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            footsteps();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            footsteps();
        }

        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            StopFootsteps();
        }
    }

    void footsteps()
    {
        footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);
    }
}