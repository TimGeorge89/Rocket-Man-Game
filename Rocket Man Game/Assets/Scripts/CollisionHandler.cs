using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //VARIABLES
    AudioSource audioSource;
    [SerializeField] float invokeDelay = 1.5f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip successJingle;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Reloads Current Level
    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Loads next level
    void NextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void OnCollisionEnter(Collision other) 
    {

        switch (other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("Launch");
                break;
            case "Finish":
                // Loads next level
                StartLevelProgression();
                break;
            case "Fuel":
                Debug.Log("Fuel Refilled"); 
                break;
            case "Obstacle":
                //Resets Scene to Start of current level.
                StartCrashSequence();
                break;
            default:
                Debug.Log("You Died");
                break;
        }
    }
    
    // Removes player control and sound on Collision
    void StartCrashSequence()
    {
        audioSource.PlayOneShot(explosion);
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke("ReloadLevel", invokeDelay);
        
    }

    void StartLevelProgression()
    {
        audioSource.PlayOneShot(successJingle);
        GetComponent<Movement>().enabled = false;
        //GetComponent<AudioSource>().enabled = false;
        Invoke("NextLevel", invokeDelay);
        
    }
}
