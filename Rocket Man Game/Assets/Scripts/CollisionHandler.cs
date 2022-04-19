using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //VARIABLES
    
    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("Launch");
                break;
            case "Finish":
                Debug.Log("Level Complete");
                break;
            case "Fuel":
                Debug.Log("Fuel Refilled"); 
                break;
            case "Obstacle":
                //Resets Scene to Start of current level.
                ReloadLevel();
                Debug.Log("You hit an Obstacle!");
                break;
            default:
                Debug.Log("You Died");
                break;
        }
    }
}
