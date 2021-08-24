using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    Transform player;
    private void OnEnable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Start()
    {
        player = Player.Instance.transform;
    }

    private void Update()
    {
        if (player.position.z >= transform.position.z)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 238, 154, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(9, 2, 0.3f));
    }

}
