using UnityEngine;

[RequireComponent(typeof(Moteur))]
public class Controleur : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Moteur motor;
    
    void Start()
    {
        motor = GetComponent<Moteur>();
    }

    
    void Update()
    {
        //Velocité, mouvement
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveH = transform.right * xMov;
        Vector3 moveV = transform.forward * zMov;

        Vector3 velocite = (moveH + moveV).normalized * speed;

        motor.Move(velocite);
    }
}
