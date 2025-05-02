using UnityEngine;

public class CoordinatesManager : MonoBehaviour
{
    [SerializeField] private Transform[] xAnchors;
    [SerializeField] private Transform[] zAnchors;

    /// <summary>
    /// Returns true if a tray of size (Width×Height) can sit at (xIndex, zIndex)
    /// without any part running off the anchors array.
    /// </summary>
    public bool IsValidMove(int xIndex, int zIndex, Tray tray)
    {
        if (xIndex < 0 || zIndex < 0) return false;
        if (xIndex + tray.Width  > xAnchors.Length) return false;
        if (zIndex + tray.Height > zAnchors.Length) return false;
        return true;
    }

    /// <summary>
    /// Finds the nearest valid anchor indices (x,z) for this world position.
    /// Clamps into the valid range so the tray never overflows.
    /// </summary>
    public Vector2Int NearestValidAnchor(Vector3 worldPos, Tray tray)
    {
        // find closest xAnchor
        int xi = 0;
        float minDX = Mathf.Abs(worldPos.x - xAnchors[0].position.x);
        for (int i = 1; i < xAnchors.Length; i++)
        {
            float dx = Mathf.Abs(worldPos.x - xAnchors[i].position.x);
            if (dx < minDX) { minDX = dx; xi = i; }
        }
        // find closest zAnchor
        int zi = 0;
        float minDZ = Mathf.Abs(worldPos.z - zAnchors[0].position.z);
        for (int i = 1; i < zAnchors.Length; i++)
        {
            float dz = Mathf.Abs(worldPos.z - zAnchors[i].position.z);
            if (dz < minDZ) { minDZ = dz; zi = i; }
        }

        // clamp so tray won’t overflow off the grid
        xi = Mathf.Clamp(xi, 0, xAnchors.Length  - tray.Width);
        zi = Mathf.Clamp(zi, 0, zAnchors.Length - tray.Height);

        return new Vector2Int(xi, zi);
    }

    /// <summary>
    /// Snap the tray’s transform to the (x,z) anchor coordinates.
    /// </summary>
    public void SnapTrayTo(int xIndex, int zIndex, Tray tray)
    {
        Vector3 target = new Vector3(
            xAnchors[xIndex].position.x,
            tray.transform.position.y,
            zAnchors[zIndex].position.z
        );
        tray.transform.position = target;
    }
}
