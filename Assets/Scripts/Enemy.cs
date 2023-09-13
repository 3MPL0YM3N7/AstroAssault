using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionVFX;
    [SerializeField] AudioClip enemyExplosionSFX;
    [SerializeField] Transform runtimeParent;

    // SP = Score Points
    [SerializeField] int enemySP = 10;
    // HP = Hit Points
    [SerializeField] int enemyHP = 10;
    int currentScore;

    void Start()
    {
        this.gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        enemyHP -= 1;
        FindObjectOfType<ScoreBoard>().IncreaseScore(enemySP);

        if (enemyHP <= 0)
        {
            EnemyDeath();

            currentScore = FindObjectOfType<ScoreBoard>().GetScore();
            Debug.Log($"current score: {currentScore}");
        }
    }

    void EnemyDeath()
    {
        Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity, runtimeParent);
        GetComponent<AudioSource>().PlayOneShot(enemyExplosionSFX);

        // Disable Renderer and Collider
        foreach (MeshRenderer childRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.enabled = false;
        }
        foreach (BoxCollider childCollider in GetComponentsInChildren<BoxCollider>())
        {
            childCollider.enabled = false;
        }
        Destroy(gameObject, 1f);
    }
}
