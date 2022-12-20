using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;
    

    float reloadTimeDelay = 1f;
     
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        explosionVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;

        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", reloadTimeDelay);
    }

    void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
