using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnFall : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y <= -4)
            Destroy(gameObject);
    }
}
