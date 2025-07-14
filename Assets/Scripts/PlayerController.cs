using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActionMain control;
    PhotonView pv;
    private Camera mainCamera;
    [SerializeField] float walkSpeed = 2,runSpeed = 3;

    public List<Gun> gunsCarrying;
    public Gun currentGun;
    public GameObject bullet;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        currentGun = gunsCarrying[^1];
        if (!pv.IsMine) return;
        control = new InputActionMain();
        control.Enable();
        mainCamera = Camera.main; // Cache the main camera
        control.MainActionMap.Shoot.performed +=x=> Shoot();
    }

    private void Shoot()
    {
        if (!currentGun.CanFire()) return;
        currentGun.Fire();
        var b = PhotonNetwork.Instantiate("Bullet", transform.position + transform.forward + Vector3.up * .5f, transform.rotation);
    }

    private void Update()
    {
        if (!pv.IsMine) return;
        if (currentGun.CanFire() && control.MainActionMap.Shoot.inProgress && currentGun.firingMode == FiringMode.automatic)
        {
            currentGun.Fire();
            Shoot();
        }
        // Handle movement
        var movement = control.MainActionMap.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(movement.x, 0, movement.y) * Time.deltaTime*runSpeed;
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
    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;
        if (other.CompareTag("Bullet"))
        {
            var pv = other.GetComponent<PhotonView>();
            if (pv.IsMine) return;
            transform.position = new Vector3(Random.Range(-10,10),transform.position.y,Random.Range(-10,10));
        }
        
    }
    private void OnDestroy()
    {
        control?.Disable(); // Clean up input system
    }
}