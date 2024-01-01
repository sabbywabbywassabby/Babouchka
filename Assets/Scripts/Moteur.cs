using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moteur : MonoBehaviour
{
    private Vector3 velocite;
    private Rigidbody rb;
    
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

    private void FixedUpdate()
    {
        PerformMovement();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Vertical");

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && x==0)
        {
            rb.AddForce(Vector3.up * 400);
        }
    }
}
