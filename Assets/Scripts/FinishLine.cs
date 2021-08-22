using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.WinGame();
    }
}
