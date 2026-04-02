using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movimentação com Mouse")]
    [SerializeField] private float dragSpeed = 2f;
    [SerializeField] private MouseButton dragButton = MouseButton.Right;

    [Header("Zoom com Scroll")]
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 20f;

    [Header("Limites de Movimentação (Opcional)")]
    [SerializeField] private bool useBounds = false;
    [SerializeField] private float minX = -50f;
    [SerializeField] private float maxX = 50f;
    [SerializeField] private float minY = -50f;
    [SerializeField] private float maxY = 50f;

    private Camera cam;
    private Vector3 dragOrigin;

    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 2
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandleDrag();
        HandleZoom();
    }

    void HandleDrag()
    {
        // Inicia o arrasto
        if (Input.GetMouseButtonDown((int)dragButton))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        // Arrasta a câmera
        if (Input.GetMouseButton((int)dragButton))
        {
            Vector3 currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentMousePos;

            Vector3 newPosition = transform.position + difference;

            // Aplica limites se ativado
            if (useBounds)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            }

            transform.position = newPosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            float newSize = cam.orthographicSize - scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}