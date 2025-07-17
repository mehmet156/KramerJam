using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Mouse & Kamera")]
    [SerializeField] private float mouseSense = 100f;
    [SerializeField] private Transform cameraTransform;

    [Header("Hareket")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;

    private CharacterController controller;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private int verticalLimit = 60;

    [SerializeField] private float crouchCamYOffset = -0.5f;
    private Vector3 defaultCameraPos;
    private bool isCrouching = false;

    Rigidbody rb;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        defaultCameraPos = cameraTransform.localPosition;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        

        
            // Ctrl tuþuna basýldýðýnda çömel
            if (Input.GetKeyDown(KeyCode.LeftControl))
                isCrouching = true;
            else if (Input.GetKeyUp(KeyCode.LeftControl))
                isCrouching = false;

            // Hedef kamera pozisyonunu belirle
            Vector3 targetPos = defaultCameraPos + (isCrouching ? new Vector3(0, crouchCamYOffset, 0) : Vector3.zero);

            // Kamerayý yumuþakça hedef pozisyona geçir
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPos, Time.deltaTime * 10f);
        



        CameraLook();
        
       
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;
        /*
       if(Mathf.Abs(mouseY)>0.1|| Mathf.Abs(mouseX) > 0.1)
        {
            AudioController.Instance.SetMovementState(true);
        }
       else
        {
            AudioController.Instance.SetMovementState(false);
        }
        */
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60, verticalLimit);
        yRotation += mouseX;

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        // controller.Move(move * currentSpeed * Time.deltaTime);

        Vector3 movePosition = rb.position + move * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(movePosition);
    }


    private void OnEnable()
    {
        // Event'e abone ol
        if (RoundTimer.Instance != null)
        {
            RoundTimer.Instance.OnTimerEnd += GameOver;
            
        }
        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.GameRestart += RestartGame;
            GameUIManager.Instance.GameOverEvent += GameOver;
        }
    }

    private void OnDisable()
    {
        // Event'ten ayrýl (önemli!)
        if (RoundTimer.Instance != null)
        {
            RoundTimer.Instance.OnTimerEnd -= GameOver;
            
        }
        if(GameUIManager.Instance!=null)
        {
            GameUIManager.Instance.GameRestart -= RestartGame;
            GameUIManager.Instance.GameOverEvent -= GameOver;
        }
        
    }

    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f; // Oyunu durdurmak istersen
    }
}
