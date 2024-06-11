using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    private bool open;
    public GameObject click;
    public Animator book_anim;
    public Animator etagere_anim;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    private void OnMouseOver()
    {
        if (!open)
        {
            click.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                book_anim.Play("book 1");
                etagere_anim.Play("decale");
            }
                
        }
        
    }

    private void OnMouseExit()
    {
        click.SetActive(false);
    }
    
}
