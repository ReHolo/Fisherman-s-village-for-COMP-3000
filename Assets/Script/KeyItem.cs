using UnityEngine;

public class KeyItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��һ��Կ���߼�
            PlayerInventory playerInv = other.GetComponent<PlayerInventory>();
            if (playerInv != null)
            {
                playerInv.hasKey = true;
                // ��ʾʰȡ��ʾ "�ѻ��Կ��"
                // ����ʰ����Ч
            }
            Destroy(gameObject); // ʰȡ���Ƴ�Կ��
        }
    }
}
