using UnityEngine;

[RequireComponent(typeof(Moteur))]
public class Controleur : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Moteur motor;

    private GameObject Cle;
    private PickUp p;

    public float sensitivity = -2f;
    private Vector3 rotate;

    void Start()
    {
        motor = GetComponent<Moteur>();

        Cursor.lockState = CursorLockMode.Locked;
        Cle = GameObject.Find("key");
        p = Cle.GetComponentInChildren<PickUp>();
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
/*
        if (Input.GetKey(KeyCode.Q)) // Si le joueur appuie sur la touche Q
        {
            p.Drop();
        }
*/
    }
}
