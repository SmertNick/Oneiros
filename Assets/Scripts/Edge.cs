using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.SetActive(false); //disabling bullets or other objects going outside
    }
}
