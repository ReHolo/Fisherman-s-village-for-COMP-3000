using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f; // 行走速度
    public float runSpeed = 10f; // 奔跑速度
    public float crouchSpeed = 2.5f; // 蹲下速度
    private float currentSpeed; // 当前移动速度

    public float jumpForce = 5f; // 跳跃力度
    public float gravity = -9.8f; // 重力加速度

    [Header("Ground Detection")]
    public Transform groundCheck; // 检测地面的空物体
    public float groundDistance = 0.4f; // 地面检测半径
    public LayerMask groundMask; // 地面图层
    private bool isGrounded;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f; // 蹲下时的高度
    public float standingHeight = 2f; // 站立时的高度
    public float crouchTransitionSpeed = 5f; // 蹲下或站立的过渡速度
    private bool isCrouching = false;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f; // 鼠标灵敏度
    private float xRotation = 0f; // 垂直方向旋转角度

    private CharacterController characterController; // 角色控制器
    private Vector3 velocity; // 当前速度

    void Start()
    {
        // 初始化
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标
        currentSpeed = walkSpeed; // 默认行走速度
    }

    void Update()
    {
        GroundCheck();  // 检测是否在地面
        MovePlayer();   // 玩家移动
        RotateCamera(); // 控制摄像机旋转
        ApplyGravity(); // 处理重力
        HandleCrouch(); // 处理蹲下逻辑
    }

    // 地面检测逻辑
    void GroundCheck()
    {
        // 使用 Physics.CheckSphere 检测地面
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 确保紧贴地面
        }
    }

    // 玩家移动逻辑
    void MovePlayer()
    {
        // 获取玩家输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 检查是否在奔跑状态
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

        // 计算移动方向
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        // 应用移动
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        // 跳跃逻辑
        if (isGrounded && Input.GetButtonDown("Jump") && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    // 控制摄像机旋转
    void RotateCamera()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 控制垂直方向（上下）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 限制垂直旋转范围

        // 应用旋转
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 控制摄像机上下旋转
        transform.Rotate(Vector3.up * mouseX); // 控制角色左右旋转
    }

    // 重力处理逻辑
    void ApplyGravity()
    {
        // 应用重力
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    // 处理蹲下逻辑
    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C)) // 按下C键切换蹲下状态
        {
            isCrouching = !isCrouching; // 切换蹲下状态
        }

        // 根据是否蹲下调整角色高度
        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);
    }
}
