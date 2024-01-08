using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moteur : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocite;
    private Vector3 rotation;
    private Vector3 camerarotation;

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

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _camerarotation)
    {
        camerarotation = _camerarotation;
    }

    private void PerformMovement()
    {
        if(velocite != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocite * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-camerarotation);
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Lorsque le personnage touche le sol, il peut sauter à nouveau
        if (collision.gameObject.CompareTag("Sol"))
        {
            peutSauter = true;
        }
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
