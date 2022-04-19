using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //VARIABLES


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
                NextLevel();
                break;
            case "Fuel":
                Debug.Log("Fuel Refilled"); 
                break;
            case "Obstacle":
                //Resets Scene to Start of current level.
                ReloadLevel();
                break;
            default:
                Debug.Log("You Died");
                break;
        }
    }
}
