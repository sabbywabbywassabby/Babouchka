using UnityEngine;

public class DoorSoundEffects : MonoBehaviour
{
  public AudioClip openingSound;
  public AudioClip closingSound;
  private bool isOpen;
  private AudioSource audioSource;

  void Start()
  {
    isOpen = false;
    audioSource = GetComponent<AudioSource>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      if (!isOpen)
      {
        audioSource.clip = openingSound;
        audioSource.Play();
        isOpen = true;
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      if (isOpen)
      {
        audioSource.clip = closingSound;
        audioSource.Play();
        isOpen = false;
      }
    }
  }
}
