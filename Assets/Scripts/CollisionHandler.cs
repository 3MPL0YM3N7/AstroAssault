using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem playerExplosionVFX;
    [SerializeField] AudioClip playerExplosionSFX;

    // code snippet executed only once
    bool isCrashed = false;

    void OnCollisionEnter(Collision collision)
    {
        StartCrashSequence();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void StartCrashSequence()
    {
        if (!isCrashed)
        {
            GetComponent<MeshRenderer>().enabled = false;
            FindObjectOfType<PlayerControls>().enabled = false;
            playerExplosionVFX.Play();
            GetComponent<AudioSource>().PlayOneShot(playerExplosionSFX);
            Invoke("RestartLevel", 1f);
            isCrashed = true;
        }
    }

    public bool GetIsCrashed()
    {
        return isCrashed;
    }
}
