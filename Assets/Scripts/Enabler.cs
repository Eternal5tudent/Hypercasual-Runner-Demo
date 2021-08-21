using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    /// <summary>
    /// Enable a GameObject when the player passes from the position of this gameObject
    /// </summary>
    [SerializeField] GameObject objectToEnable;
    Transform player;

    private void Start()
    {
        player = Player.Instance.transform;
    }

    private void Update()
    {
        if (player.position.z >= transform.position.z)
        {
            objectToEnable.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 238, 154, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(9, 2, 0.3f));
    }

}
