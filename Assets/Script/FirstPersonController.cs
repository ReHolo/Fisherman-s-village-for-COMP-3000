using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f; // �����ٶ�
    public float runSpeed = 10f; // �����ٶ�
    public float crouchSpeed = 2.5f; // �����ٶ�
    private float currentSpeed; // ��ǰ�ƶ��ٶ�

    public float jumpForce = 5f; // ��Ծ����
    public float gravity = -9.8f; // �������ٶ�

    [Header("Ground Detection")]
    public Transform groundCheck; // ������Ŀ�����
    public float groundDistance = 0.4f; // ������뾶
    public LayerMask groundMask; // ����ͼ��
    private bool isGrounded;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f; // ����ʱ�ĸ߶�
    public float standingHeight = 2f; // վ��ʱ�ĸ߶�
    public float crouchTransitionSpeed = 5f; // ���»�վ���Ĺ����ٶ�
    private bool isCrouching = false;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f; // ���������
    private float xRotation = 0f; // ��ֱ������ת�Ƕ�

    private CharacterController characterController; // ��ɫ������
    private Vector3 velocity; // ��ǰ�ٶ�

    void Start()
    {
        // ��ʼ��
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // �������
        currentSpeed = walkSpeed; // Ĭ�������ٶ�
    }

    void Update()
    {
        GroundCheck();  // ����Ƿ��ڵ���
        MovePlayer();   // ����ƶ�
        RotateCamera(); // �����������ת
        ApplyGravity(); // ��������
        HandleCrouch(); // ��������߼�
    }

    // �������߼�
    void GroundCheck()
    {
        // ʹ�� Physics.CheckSphere ������
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ȷ����������
        }
    }

    // ����ƶ��߼�
    void MovePlayer()
    {
        // ��ȡ�������
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ����Ƿ��ڱ���״̬
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            currentSpeed = runSpeed;
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // �����ƶ�����
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        // Ӧ���ƶ�
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        // ��Ծ�߼�
        if (isGrounded && Input.GetButtonDown("Jump") && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    // �����������ת
    void RotateCamera()
    {
        // ��ȡ�������
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ���ƴ�ֱ�������£�
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���ƴ�ֱ��ת��Χ

        // Ӧ����ת
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ���������������ת
        transform.Rotate(Vector3.up * mouseX); // ���ƽ�ɫ������ת
    }

    // ���������߼�
    void ApplyGravity()
    {
        // Ӧ������
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    // ��������߼�
    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C)) // ����C���л�����״̬
        {
            isCrouching = !isCrouching; // �л�����״̬
        }

        // �����Ƿ���µ�����ɫ�߶�
        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);
    }
}
