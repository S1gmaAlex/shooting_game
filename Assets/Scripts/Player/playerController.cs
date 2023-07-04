using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{
    public float countDownTimer;
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSens = 3.5f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float gravity = -13.0f;
    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDiretion = Vector2.zero;
    Vector2 currentDiretionVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    public  int KeyAmount;

    public float xRange = 150f;
    public float zRange = 100f;

    public Text keyAmount;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } 
    }

    void Update()
    {
        //keyAmount.text = "KeyAmount: " + KeyAmount;
        updateMouseLook();
        if (countDownTimer <= 0f)
        {
            updateMovement();
        }
        countDownTimer -= Time.deltaTime;       
    }

    void updateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraPitch -= currentMouseDelta.y * mouseSens;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSens);
    }

    void updateMovement()
    {
        Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDirection.Normalize();
        currentDiretion = Vector2.SmoothDamp(currentDiretion, targetDirection, ref currentDiretionVelocity, moveSmoothTime);
        Vector3 velocity = (transform.forward * currentDiretion.y + transform.right * currentDiretion.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            KeyAmount += 1;
            keyAmount.text = "KeyAmount: " + KeyAmount;
            Destroy(other.gameObject);
        }
    }
}
