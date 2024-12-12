using UnityEngine;
using UnityEngine.UI;

public class LockedDoorInteract : MonoBehaviour
{
    public Animator doorAnimator;
    public Text interactText;
    public string unlockTriggerName = "UnlockTrigger";
    public string openTriggerName = "OpenTrigger";
    public string closeTriggerName = "CloseTrigger";

    private bool canInteract = false;
    private bool isOpened = false;   // 门是否打开（当前状态下：false表示门关闭）
    private bool isLocked = true;    // 门是否上锁
    private PlayerInventory playerInv;

    void Start()
    {
        if (interactText != null)
            interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                // 门锁定状态，需钥匙解锁
                if (playerInv != null && playerInv.hasKey)
                {
                    // 使用钥匙解锁门
                    doorAnimator.SetTrigger(unlockTriggerName);
                    isLocked = false;
                    playerInv.hasKey = false; // 耗费钥匙或保留根据设计决定
                    UpdateUI(); // 更新UI提示为可开门状态
                }
                else
                {
                    // 没有钥匙：提示需要钥匙
                    if (interactText != null)
                        interactText.text = "需要钥匙";
                }
            }
            else
            {
                // 门已解锁，按E开关门
                if (!isOpened)
                {
                    // 开门
                    doorAnimator.SetTrigger(openTriggerName);
                    isOpened = true;
                    // 可选择更新UI提示为“按E关闭门”
                    UpdateUI();
                }
                else
                {
                    // 关门
                    doorAnimator.SetTrigger(closeTriggerName);
                    isOpened = false;
                    UpdateUI();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            playerInv = other.GetComponent<PlayerInventory>();
            UpdateUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            playerInv = null;
            if (interactText != null)
                interactText.gameObject.SetActive(false);
        }
    }

    void UpdateUI()
    {
        if (interactText == null) return;

        if (!canInteract)
        {
            interactText.gameObject.SetActive(false);
            return;
        }

        interactText.gameObject.SetActive(true);

        if (isLocked)
        {
            // 门上锁状态，如果玩家有钥匙，则提示使用钥匙解锁，否则提示需要钥匙
            if (playerInv != null && playerInv.hasKey)
                interactText.text = "Press E Open";
            else
                interactText.text = "Locked";
        }
        else
        {
            // 门已解锁
            interactText.text = isOpened ? "Open" : "Close";
        }
    }
}
