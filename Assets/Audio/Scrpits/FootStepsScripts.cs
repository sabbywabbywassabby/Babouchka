using UnityEngine;

namespace Audio.Scrpits
{
    public class FootStepsScripts : MonoBehaviour
    {
        public GameObject footstep;

        void Start()
        {
            footstep.SetActive(false);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                footstep.SetActive(true);
            }

            else if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                footstep.SetActive(false);

            }
        }
    }
}