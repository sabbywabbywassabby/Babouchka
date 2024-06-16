using UnityEngine;
using Mirror;

public class Controleur_Bryan : NetworkBehaviour
{
    
    public Camera cam;    
    public GameObject menu_pause;
    public Animator anim_control;
    public GameObject blue_onplayer;
    public GameObject black_on_player;
    public GameObject red_on_player;
    public GameObject green_on_player;
    public GameObject gold_on_player;

    private float rotation_speed = 200f;
    private float sensitivity = -2f;
    private float mouseX, mouseY;

    public PickUp key_on_player;
    public bool stop_moving;
    public bool only_cam;
    
       
    // Start is called before the first frame update
    void Start()
    {
        stop_moving = false;
        only_cam = false;
        anim_control = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;        
    }


    public void Drop()
    {
        // Récupérer la position du personnage
        Vector3 characterPosition = transform.position;
        /*
        // Récupérer la direction dans laquelle le personnage regarde
        Vector3 characterForward = transform.forward;
        // Calculer la position devant le personnage
        Vector3 dropPosition = characterPosition + characterForward + new Vector3(0,0.5f,0);*/

        if(key_on_player != null){
            key_on_player.Drop(characterPosition);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if(menu_pause.activeSelf || stop_moving){
            if(Cursor.lockState != CursorLockMode.None){
                Cursor.lockState = CursorLockMode.None;
            }
            return;
        }

        if(Cursor.lockState != CursorLockMode.Locked){
                Cursor.lockState = CursorLockMode.Locked;
            }
        // Controler la rotation de la caméra par la souris
        mouseX -= Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f); // Limiter le mouvement vertical

        // Appliquer la rotation de la caméra
        cam.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        if (only_cam) //limiter les mouvements seulement au regard
        {
            return;
        }

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


        //Faire tomber la cle :
        if (Input.GetKey(KeyCode.Q)) // Si le joueur appuie sur la touche Q
        {
            Drop();
        }

        
        
    }
}

