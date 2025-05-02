using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tray : MonoBehaviour
{
    [Tooltip("How many cells this tray occupies in X direction.")]
    [SerializeField] private int maxX = 1;
    [Tooltip("How many cells this tray occupies in Z direction.")]
    [SerializeField] private int maxY = 1;
    [Tooltip("Which tag marks your walls?")]
    [SerializeField] private string wallTag = "Wall";

    public int Width  => maxX;
    public int Height => maxY;

    // true while any part of this tray is overlapping a Wall trigger
    public bool isCollidingWithWall { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(wallTag))
            isCollidingWithWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(wallTag))
            isCollidingWithWall = false;
    }
}
