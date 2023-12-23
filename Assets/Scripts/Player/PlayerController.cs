using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Transform feet;
    private Rigidbody rb;
    private UIController UIController;

    [Header("Looking")]
    [SerializeField][Range(0f, 100f)] private float xSensitivity;
    [SerializeField][Range(0f, 100f)] private float ySensitivity;
    [SerializeField][Range(0f, 90f)] private float topCameraClamp;
    [SerializeField][Range(0f, 90f)] private float bottomCameraClamp;
    private float xRotation;
    private float yRotation;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;

    [Header("Drag")]
    [SerializeField] private float groundDrag;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask environmentMask;
    private bool isGrounded;

    [Header("Interactables")]
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private TMP_Text speedText;

    [Header("Inventory")]
    private Inventory inventory;

    [Header("Keybinds")]
    [SerializeField] private KeyCode interactKey;

    private void Start() {

        rb = GetComponent<Rigidbody>();
        UIController = FindObjectOfType<UIController>();

        inventory = new Inventory();

    }

    private void Update() {

        // ground check
        isGrounded = Physics.CheckSphere(feet.position, groundCheckRadius, environmentMask);

        // looking
        float mouseX = Input.GetAxisRaw("Mouse X") * xSensitivity * 5f * Time.fixedDeltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * ySensitivity * 5f * Time.fixedDeltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -topCameraClamp, bottomCameraClamp);

        cameraPos.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // speed control
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed) {

            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }
        speedText.text = new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude + "";

        // drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;

        // interactable check
        if (Physics.Raycast(camera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f)), out RaycastHit hitInfo, interactDistance, interactMask) && hitInfo.transform.GetComponent<Interactable>()) {

            Interactable interactable = hitInfo.transform.GetComponent<Interactable>();
            UIController.ShowInteractableInfo(interactable);

            if (Input.GetKeyDown(interactKey))
                interactable.Interact(inventory);

            UIController.SetCrosshairText("Interact");
            UIController.ShowInteractCrosshair();

        } else {

            UIController.HideInteractableInfo();
            UIController.SetCrosshairText("");
            UIController.ShowNormalCrosshair();

        }
    }

    private void FixedUpdate() {

        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.AddForce(movementDirection.normalized * 10f * movementSpeed, ForceMode.Force);

    }
}
