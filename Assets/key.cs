using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    
    public GameObject hand;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public float movementThreshold = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        // Enregistre la position et la rotation initiales
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    { 
        // Calcule la différence de position entre la main et la position initiale
        Vector3 positionDifference = hand.transform.position - initialPosition;

        // Vérifie si le mouvement est inférieur au seuil défini
        if (positionDifference.magnitude < movementThreshold)
        {
            // Réinitialise la position à la position initiale
            transform.position = initialPosition;
        }
        else
        {
            // Applique la différence de position à la position initiale de l'objet
            transform.position = initialPosition + positionDifference + new Vector3(0, -0.05f, 0.1f);
        }
        // Assurez-vous que la rotation est synchronisée avec la main
        //transform.rotation = hand.transform.rotation * initialRotation;

    }
}
