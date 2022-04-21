using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // VARIABLES
    Rigidbody rb;
    AudioSource audioSource; 
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float roatationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Audio Source
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        //Rigidbody
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust() 
    {
        //Up movement
        if (Input.GetKey(KeyCode.Space)) {

            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            //Checks to see if audio is playing
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
            
        } else if (!Input.GetKey(KeyCode.Space)) {
            audioSource.Stop();
        }
    }

    void ProcessRotation() 
    {
        //Right & Left Movement
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(roatationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-roatationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing roatation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
