using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // variable to store scene number
    [SerializeField] int sceneToLoad = -1;

    // when the player collides with the portal collider, load the next scene
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Portal triggered.");
        if (other.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("Scene loaded.");
        Destroy(gameObject);
    }
}
