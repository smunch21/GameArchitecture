using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float gravity;
    [SerializeField] private float SprintMultiplier;
    [SerializeField] private float jumpVelocity;
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [Header("Shooting")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce;

    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask buttonLayer;
    [SerializeField] private float interactionDistance;

    [Header("Pick ANd Drop")]
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    private float moveMultiplier = 1;
    private Vector3 playerVelocity;
    private bool mouseInvert;
    private bool isGrounded;
    private float camXRotation;
    private float horizontal, vertical;
    private float mouseX, mouseY;

    private CharacterController characterController;

    private RaycastHit raycastHit;
    private ISelectable selectable;

    private bool isPicked = false;
    private IPickable pickable;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        GroundCheck();
        JumpCheck();
        ShootBullet();
        ShootRocket();
        Interact();
        PickAndDrop();

    }



    void GetInput()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        moveMultiplier = Input.GetButton("Sprint") ? SprintMultiplier : 1;
    }

    void MovePlayer()
    {
        characterController.Move((transform.forward * vertical + transform.right * horizontal) * moveSpeed * moveMultiplier * Time.deltaTime);
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        camXRotation += Time.deltaTime * mouseY * turnSpeed * (mouseInvert ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -85, 85f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundLayer);
    }

    void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpVelocity;
        }
    }

    void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce); 

        }    
    }

    void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody rocket = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            rocket.AddForce(shootPoint.forward * shootForce);
            Destroy(rocket.gameObject, 5.0f);

        }
    }

    void Interact()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray,out raycastHit, interactionDistance, buttonLayer))
        {
            selectable = raycastHit.transform.GetComponent<ISelectable>();

            if(selectable != null)
            {
                selectable.OnHoverEnter();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    selectable.OnSelect();
                }
            }
        }

        if(raycastHit.transform == null && selectable != null) 
        {
            selectable.OnHoverExit();
            selectable = null;
        }

    }

    void PickAndDrop()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2 , Screen.height/2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isPicked)
            {
                pickable = raycastHit.transform.GetComponent<IPickable>();
                if (pickable == null)
                {
                    return;
                }
                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }

        }
        if(Input.GetKeyDown(KeyCode.E)&& isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }
    }

}