using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;   // ����������
    public KeyCode interactKey = KeyCode.E;   // ��������
    private bool isOpen = false;   // �ŵĿ���״̬

    void Update()
    {
        // ��ⰴ����ִ�ж���
        if (Input.GetKeyDown(interactKey))
        {
            if (!isOpen)
            {
                doorAnimator.Play("DoorOpen");   // ���ſ��Ŷ���
                isOpen = true;
            }
            else
            {
                doorAnimator.Play("DoorOpen", -1, 1f);  // ���򲥷Ŷ���
                isOpen = false;
            }
        }
    }
}
