using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isUsingComputer = false;

    private Material computerNormal;
    public Material computerHighlight;
    private Transform lastHitObject;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform playerCamera; // camera ref
    public Transform respawnPoint; // respawn ref

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsingComputer) {
            return;
        }

        if (!isUsingComputer) {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f)) {
                if (hit.transform.CompareTag("Computer")) {
                    Renderer computerRenderer = hit.transform.GetComponent<Renderer>();

                    if (lastHitObject!= hit.transform) {
                        ResetHighlight();
                        computerNormal = computerRenderer.material;
                        computerRenderer.material = computerHighlight;
                        lastHitObject = hit.transform;
                    }

                    if (Input.GetKeyDown(KeyCode.E)) {
                        ComputerScreen.Instance.Open();
                    }
                } else {
                    ResetHighlight();
                }
            } else {
                ResetHighlight();
            }
        }
        
        // ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        // input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * z) + (right * x);
        controller.Move(move * speed * Time.deltaTime);

        // jump handling
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (transform.position.y < -5) {
            Respawn();
        }
    }

    public void SetInteracting(bool interacting) {
        isUsingComputer = interacting;
    }

    private void Respawn() {
        transform.position = respawnPoint.position;
        velocity = Vector3.zero;
    }

    void ResetHighlight() {
        if (lastHitObject != null) {
            lastHitObject.GetComponent<Renderer>().material = computerNormal;
            lastHitObject = null;
            Debug.Log("Reset Highlight");
        }
    }
}
