using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    public Animator doorAnimator;
    public string openDoorTriggerName = "OpenTrigger";
    public string closeDoorTriggerName = "CloseTrigger";
    public Text interactText;

    private bool canOpen = false;
    private bool isOpened = false; // ��ʼ��false��ʾ�Ŵ��ڹر�״̬

    void Start()
    {
        if (interactText != null)
            interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        // ������ڴ�����Χ�ڰ���E��
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpened)
            {
                // �ŵ�ǰ�ǹر�״̬����E����
                doorAnimator.SetTrigger(openDoorTriggerName);
                isOpened = true;
                if (interactText != null)
                    interactText.gameObject.SetActive(false);
            }
            else
            {
                // �ŵ�ǰ�Ǵ�״̬����E�ر���
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
                // ������״̬��ʾ��ͬ��ʾ
                interactText.text = isOpened ? "�� E �ر���" : "�� E ����";
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
