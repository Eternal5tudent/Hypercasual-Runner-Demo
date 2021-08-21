using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    [SerializeField] private Material fallingMaterial; // Material of terrain block after touched by player
    [SerializeField] private float gravityAcceleration = 9f;
    [SerializeField] private float destroyAfterSeconds = 0.2f;

    private bool touchedByPlayer = false;
    private MeshRenderer meshRenderer;
    private Collider meshCollider;
    private float timeFalling = 0; // Time since the block turned red

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (touchedByPlayer)
        {
            Fall();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (!touchedByPlayer) // if it's the first time being touched by the player
            {
                touchedByPlayer = true;
                meshRenderer.material = fallingMaterial;
                timeFalling = 0;
                meshCollider.enabled = false;
                StartCoroutine(Deactivate_Cor(destroyAfterSeconds));
            }

        }
    }

    private void Fall() // Could do this with a RigidBody but this is more performant
    {
        timeFalling += Time.deltaTime;
        float velocity = gravityAcceleration * timeFalling;
        transform.position += Time.deltaTime * velocity * Vector3.down;
    }

    private IEnumerator Deactivate_Cor(float awaitSeconds)
    {
        yield return new WaitForSeconds(awaitSeconds);
        Destroy(gameObject);
    }

}
