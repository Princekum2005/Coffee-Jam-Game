using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tray : MonoBehaviour
{
    [SerializeField] private int maxX = 1;
    [SerializeField] private int maxY = 1;
    [SerializeField] private string wallTag = "Wall";
    [SerializeField] private string trayTag = "Tray";

    public int Width  => maxX;
    public int Height => maxY;
    public bool IsCollidingWithWall { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(wallTag) || other.CompareTag(trayTag))
            IsCollidingWithWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(wallTag) || other.CompareTag(trayTag))
            IsCollidingWithWall = false;
    }
}
