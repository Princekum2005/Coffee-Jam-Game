using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [Tooltip("The tag you use on every Tray root object.")]
    [SerializeField] private string trayTag = "Tray";

    private Camera cam;
    private CoordinatesManager coordMgr;

    void Start()
    {
        cam     = Camera.main;
        coordMgr = FindObjectOfType<CoordinatesManager>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            HandleDrag();
        else if (Input.GetMouseButtonUp(0))
            HandleRelease();
    }

    private Tray currentTray;
    private Vector3 dragOffset;

    void HandleDrag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) &&
            hit.transform.CompareTag(trayTag))
        {
            if (currentTray == null)
            {
                // begin dragging
                currentTray = hit.transform.GetComponent<Tray>();
                // maintain offset so it doesn’t snap under mouse
                dragOffset = hit.transform.position - hit.point;
            }

            if (currentTray.isCollidingWithWall)
            {
                // you said “do other things” here
                Debug.Log("Stuck on wall—cannot move further");
                return;
            }

            // X/Z only, with that same dragOffset
            Vector3 desired = hit.point + dragOffset;
            Vector3 p = hit.transform.position;
            hit.transform.position = new Vector3(desired.x, p.y, desired.z);
        }
    }

    void HandleRelease()
    {
        if (currentTray != null)
        {
            // Snap to nearest valid anchor
            Vector3 pos = currentTray.transform.position;
            Vector2Int idx = coordMgr.NearestValidAnchor(pos, currentTray);
            coordMgr.SnapTrayTo(idx.x, idx.y, currentTray);

            // Clear for next drag
            currentTray = null;
        }
    }
}
