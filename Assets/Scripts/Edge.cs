using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
