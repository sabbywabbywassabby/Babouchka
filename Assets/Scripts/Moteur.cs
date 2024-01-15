using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moteur : MonoBehaviour
{
    private Vector3 velocite;
    private Rigidbody rb;

    private bool peutSauter = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Move(Vector3 _velocite)
    {
        velocite = _velocite;
    }

    private void PerformMovement()
    {
        if(velocite != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocite * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //lorsque le joueur est au sol il peut sauter a nouveau
        if (collision.gameObject.CompareTag("Sol"))
        {
            peutSauter = true;
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    void Update()
    {

        //Jump
        if (Input.GetButtonDown("Jump") && peutSauter)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            peutSauter = false;
        }
    }
}
