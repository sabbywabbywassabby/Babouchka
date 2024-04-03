using UnityEngine;

public class Controleur_Bryan : MonoBehaviour
{
    public Camera cam;
    private Animator anim_control;
    private float rotation_speed = 200f;
    public float sensitivity = -2f;
    private float mouseX, mouseY;
    private PickUp p;
    private GameObject Cle;
    private GameObject GoldKey;
    public GameObject Txt;
    private GameObject Click;
    public GameObject TxtClick;
    // Start is called before the first frame update
    void Start()
    {
        anim_control = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cle = GameObject.Find("key");
        p = Cle.GetComponentInChildren<PickUp>();
        GoldKey = GameObject.Find("Take");
        Click = GameObject.Find("Click");
        Txt = GoldKey.transform.Find("E").gameObject;
        TxtClick = Click.transform.Find("Click").gameObject;
    }

    

    // Update is called once per frame
    void Update()
    {       
        // Controler la rotation de la caméra par la souris
        mouseX -= Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f); // Limiter le mouvement vertical

        // Appliquer la rotation de la caméra
        cam.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        // Controler le personnage
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = cam.transform.forward * verticalInput + cam.transform.right * horizontalInput;
        moveDirection.y = 0f; // Ne pas bouger sur l'axe Y

        // Inverser la direction si le personnage recule
        if (verticalInput < 0f)
            moveDirection *= -1f;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotation_speed * Time.deltaTime);
        }

        // Gérer les animations
        bool isWalking = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;
        anim_control.SetBool("is_walking", isWalking);

        bool isWalkingBack = verticalInput < -0.1f;
        anim_control.SetBool("is_walking_back", isWalkingBack);

        //animation attraper un objet
        if ((Input.GetKey(KeyCode.E) && Txt.activeSelf) || (Input.GetMouseButtonDown(0) && TxtClick.activeSelf))
        {
            anim_control.SetBool("is_picking", true);
        }
        else
        {
            anim_control.SetBool("is_picking", false);
        }
        

        //Faire tomber la cle :
        if (Input.GetKey(KeyCode.Q)) // Si le joueur appuie sur la touche Q
        {
            // Récupérer la position du personnage
            Vector3 characterPosition = transform.position;
            // Récupérer la direction dans laquelle le personnage regarde
            Vector3 characterForward = transform.forward;
            // Calculer la position devant le personnage
            Vector3 dropPosition = characterPosition + characterForward * 2f;
            p.Drop(dropPosition);
        }
    }
}

