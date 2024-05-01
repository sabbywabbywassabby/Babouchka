using System;
using UnityEngine;

public class DropLamp : MonoBehaviour
{
    // Vitesse de chute du lustre
    public float fallSpeed = 10f;

    // Booléen pour vérifier si le lustre est en train de tomber
    private bool isFalling = false; 
    private bool aplique = true; 

    // Position initiale du lustre
    private Vector3 initialPosition;
    private Rigidbody rb;
    private float X;
    private float Y;
    private float Z;
    
    // Fonction appelée une fois au démarrage du script
    void Start()
    {
        // Enregistrer la position initiale du lustre
        initialPosition = transform.position;
        var rb = GetComponent<Rigidbody>();
    }

    // Fonction appelée à chaque frame
    void Update()
    {
        // Vérifier si le bouton 'G' est enfoncé et si le lustre n'est pas déjà en train de tomber
        if (Input.GetKeyDown(KeyCode.G))
        {
            Console.WriteLine("G");
            // Activer la chute du lustre
            isFalling = true;
        }

        // Si le lustre est en train de tomber
        if (isFalling)
        {
            Console.WriteLine("The chandeler is falling");
            // Calculer la nouvelle position du lustre en fonction de la vitesse de chute
            Vector3 newPosition = transform.position - Vector3.up * (fallSpeed * Time.deltaTime);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;

            // Mettre à jour la position du lustre
            transform.position = newPosition;

            // Vérifier si le lustre a atteint le sol
            if (newPosition.y <= 0.4)
            {
                if (aplique)
                {
                    Vector3 np = transform.position;
                    // Mettre à jour la position du lustre
                    X = transform.position.x;
                    Y = transform.position.y;
                    Z = transform.position.z;
                    transform.position = new Vector3(X, Y, Z);
                    // Désactiver la chute du lustre
                    isFalling = false;
                    rb.isKinematic = true;
                    aplique = false;
                }
            }
        }
    }
}