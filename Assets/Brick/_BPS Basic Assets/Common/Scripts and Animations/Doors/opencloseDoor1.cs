using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor1 : MonoBehaviour
	{

		public Animator openandclose1;
		public bool open;
		public Transform Player;
		private GameObject cle;
		public GameObject OpenNeed;
		public GameObject Click;
		private Controleur_Bryan Controle;
		

		void Start()
		{
			open = false;
			
		}

		public void SetPlayer(Transform p){
			Player = p;
			cle = Player.transform.Find("key").gameObject;
			Controle = Player.GetComponent<Controleur_Bryan>();
		}
	
		
		void OnMouseOver()
		{
			if (Player && !cle.activeSelf)
            {
				OpenNeed.SetActive(true);
            }
			
				if (Player && cle.activeSelf)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 10)
					{

						if (open == false)
						{
							Click.SetActive(true);
							if (Input.GetMouseButtonDown(0))
							{
								Click.SetActive(false);
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}
		}
        private void OnMouseExit()
        {
			OpenNeed.SetActive(false);
			Click.SetActive(false);
		}
        IEnumerator opening()
		{
			print("you are opening the door");
			openandclose1.Play("Opening 1");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose1.Play("Closing 1");
			open = false;
			yield return new WaitForSeconds(.5f);
		}

        

    }
}