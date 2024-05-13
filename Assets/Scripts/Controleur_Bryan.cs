using UnityEngine;
using Mirror;

public class Controleur_Bryan : NetworkBehaviour
{
    public Camera cam;
    public Camera otherCamera;
    public bool isdead = false;
    public bool IsDead { get => isdead; }
    public GameObject menu_pause;

    private Animator anim_control;
    private float rotation_speed = 200f;
    private float sensitivity = -2f;
    private float mouseX, mouseY;
    private PickUp p;
    private GameObject Cle;
    private GameObject GoldKey;
    private GameObject TheEnd;
    private GameObject TheEndTxt;
    private GameObject Txt;
    private GameObject Click;
    private GameObject TxtClick;
    private GameObject Player2;
    private Controleur_Bryan controle_2;
    public bool stop_moving;
       
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        anim_control = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cle = GameObject.Find("key");
        p = Cle.GetComponentInChildren<PickUp>();
        GoldKey = GameObject.Find("Take");
        Click = GameObject.Find("Click");
        TheEnd = GameObject.Find("TheEnd");
        TheEndTxt = TheEnd.transform.Find("Txt").gameObject;
        Txt = GoldKey.transform.Find("E").gameObject;
        TxtClick = Click.transform.Find("Click").gameObject;

        Player2 = GameObject.Find("Player_2");
        otherCamera = Player2.GetComponentInChildren<Camera>();
        controle_2 = Player2.GetComponent<Controleur_Bryan>();
        
    }

    public void Die()
    {
        RpcDie();
        isdead = true;
    }

    void RpcDie()
    {
        anim_control.SetBool("is_dying", true);
    }

    [Command]
    public void GameOver()
    {
        RpcOver();
    }

    [ClientRpc]
    public void RpcOver()
    {
        TheEndTxt.SetActive(true);
    }

    public void Look(Vector3 direction)
    {
        cam.transform.rotation = Quaternion.LookRotation(direction - cam.transform.position);
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

        if(Input.GetKey(KeyCode.M))
        {
            anim_control.SetBool("dods", true);
        }

        if (Input.GetKey(KeyCode.L))
        {
            Die();
        }
        if (isdead)
        {
            if (anim_control.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !controle_2.IsDead)
            {
                cam.enabled = false;
                otherCamera.enabled = true;

                //gameObject.SetActive(false);
            }
            if(anim_control.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && controle_2.IsDead)
            {
                GameOver();
            }
        }
        
    }
}

