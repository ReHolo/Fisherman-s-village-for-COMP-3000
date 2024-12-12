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
    private bool isOpened = false;   // ���Ƿ�򿪣���ǰ״̬�£�false��ʾ�Źرգ�
    private bool isLocked = true;    // ���Ƿ�����
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
                // ������״̬����Կ�׽���
                if (playerInv != null && playerInv.hasKey)
                {
                    // ʹ��Կ�׽�����
                    doorAnimator.SetTrigger(unlockTriggerName);
                    isLocked = false;
                    playerInv.hasKey = false; // �ķ�Կ�׻���������ƾ���
                    UpdateUI(); // ����UI��ʾΪ�ɿ���״̬
                }
                else
                {
                    // û��Կ�ף���ʾ��ҪԿ��
                    if (interactText != null)
                        interactText.text = "��ҪԿ��";
                }
            }
            else
            {
                // ���ѽ�������E������
                if (!isOpened)
                {
                    // ����
                    doorAnimator.SetTrigger(openTriggerName);
                    isOpened = true;
                    // ��ѡ�����UI��ʾΪ����E�ر��š�
                    UpdateUI();
                }
                else
                {
                    // ����
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
            // ������״̬����������Կ�ף�����ʾʹ��Կ�׽�����������ʾ��ҪԿ��
            if (playerInv != null && playerInv.hasKey)
                interactText.text = "Press E Open";
            else
                interactText.text = "Locked";
        }
        else
        {
            // ���ѽ���
            interactText.text = isOpened ? "Open" : "Close";
        }
    }
}
