using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    InputActionMain control;
    PhotonView pv;
    private Camera mainCamera;

    public GameObject bullet;
    float time;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        control = new InputActionMain();
        control.Enable();
        mainCamera = Camera.main; // Cache the main camera
        control.MainActionMap.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (time < .5f) return;
        var b = Instantiate(bullet, transform.position+transform.forward + Vector3.up * .5f, Quaternion.identity);
        b.transform.LookAt(transform.position-transform.forward*2);
        time = 0;

    }

    private void Update()
    {
        if (!pv.IsMine) return;

        time += Time.deltaTime;
        // Handle movement
        var movement = control.MainActionMap.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, 0, movement.y) * Time.deltaTime;
        mainCamera.transform.position = transform.position + Vector3.up * 15;

        // Handle rotation based on mouse position
        var mousePosition = control.MainActionMap.MousePosition.ReadValue<Vector2>();
        RotateTowardsMouse(mousePosition);
    }

    private void RotateTowardsMouse(Vector2 mousePosition)
    {
        // Convert mouse position from screen to world space
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Get the point on the ground plane (or any surface) where the mouse is pointing
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep player's y position to avoid tilting

            // Calculate direction from player to mouse point
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Rotate player to face the mouse point
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Lock rotation to Y-axis
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }
    }
    private void OnDestroy()
    {
        control.Disable(); // Clean up input system
    }
}