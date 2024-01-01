using UnityEngine;

[RequireComponent(typeof(Moteur))]
public class Controleur : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Moteur motor;

    public float sensitivity = -2f;
    private Vector3 rotate;

    void Start()
    {
        motor = GetComponent<Moteur>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        //Velocit�, mouvement
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 moveH = transform.right * xMov;
        Vector3 moveV = transform.forward * zMov;
        Vector3 velocite = (moveH + moveV).normalized * speed;

        motor.Move(velocite);

        //Deplacement de la souris
        float y = Input.GetAxisRaw("Mouse X");
        float x = Input.GetAxisRaw("Mouse Y");
        rotate = new Vector3(x, y * sensitivity, 0);
        
        transform.eulerAngles = transform.eulerAngles - rotate;

    }
}
