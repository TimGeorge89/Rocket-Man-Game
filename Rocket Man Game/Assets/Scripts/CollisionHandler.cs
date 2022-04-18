using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    //VARIABLES

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
                Debug.Log("You hit an Obstacle!");
                break;
            default:
                Debug.Log("You Died");
                break;
        }
    }
}
