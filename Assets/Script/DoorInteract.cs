using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    public Animator doorAnimator;
    public string openDoorTriggerName = "OpenTrigger";
    public string closeDoorTriggerName = "CloseTrigger";
    public Text interactText;

    private bool canOpen = false;
    private bool isOpened = false; // 初始是false表示门处于关闭状态

    void Start()
    {
        if (interactText != null)
            interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        // 当玩家在触发范围内按下E键
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpened)
            {
                // 门当前是关闭状态，按E开门
                doorAnimator.SetTrigger(openDoorTriggerName);
                isOpened = true;
                if (interactText != null)
                    interactText.gameObject.SetActive(false);
            }
            else
            {
                // 门当前是打开状态，按E关闭门
                doorAnimator.SetTrigger(closeDoorTriggerName);
                isOpened = false;
                if (interactText != null)
                    interactText.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
            if (interactText != null)
            {
                // 根据门状态显示不同提示
                interactText.text = isOpened ? "按 E 关闭门" : "按 E 开门";
                interactText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            if (interactText != null)
                interactText.gameObject.SetActive(false);
        }
    }
}
