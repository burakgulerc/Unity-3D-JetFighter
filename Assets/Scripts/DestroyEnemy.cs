using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    GameObject parentGameObject;

    [SerializeField] int point = 100;
    [SerializeField] int hitPoints = 2;

    ScoreBoard scoreBoard;


    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidBody();
        parentGameObject = FindObjectOfType<GameObject>();
        
    }

    void AddRigidBody()
    {
        Rigidbody rBody = gameObject.AddComponent<Rigidbody>();
        rBody.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(point);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
        
    }
}
