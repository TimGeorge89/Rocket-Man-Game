using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //VARIABLES
    [SerializeField] float invokeDelay = 1.5f;

    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip successJingle;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    //STATE
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        SkipLevelCheat();
    }

    void SkipLevelCheat()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            // toggle collision
            collisionDisabled = !collisionDisabled;
        }
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
        if (isTransitioning || collisionDisabled) {return;}

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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", invokeDelay);
        
    }

    void StartLevelProgression()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successJingle);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", invokeDelay);
        
    }
}
