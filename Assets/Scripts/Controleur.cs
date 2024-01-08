using UnityEngine;

[RequireComponent(typeof(Moteur))]
public class Controleur : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float lookSensitivity = 5f;

    private Moteur motor;


    void Start()
    {
        motor = GetComponent<Moteur>();

        Cursor.lockState = CursorLockMode.Locked;
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

        //Deplacement de la souris
        float yrot = Input.GetAxisRaw("Mouse X");
        float xrot = Input.GetAxisRaw("Mouse Y");
        //Rotation du joueur
        Vector3 rotation = new Vector3(0, yrot, 0) * lookSensitivity;
        motor.Rotate(rotation);
        //rotation de la camera
        Vector3 cameraRot = new Vector3(xrot, 0, 0) * lookSensitivity;
        motor.RotateCamera(cameraRot);

        
        //rotate = new Vector3(x, y * sensitivity, 0);
        //transform.eulerAngles = transform.eulerAngles - rotate;

    }
}
