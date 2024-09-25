using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Player ref

    private float xRotation = 0f;
    private bool canLook = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (canLook) {
            // inputs
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // transforms
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // player rotation
            playerBody.Rotate(Vector3.up * mouseX);

            
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
                canLook = !canLook;
            }
    }

    public void SetInteracting(bool isInteracting) {
        canLook = !isInteracting;
    }
}
