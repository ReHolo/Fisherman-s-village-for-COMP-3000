using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item; // ��Ʒ����
    private bool isPlayerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("������Ʒ: " + item.itemName + "���� E ʰȡ");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("�뿪��Ʒ��Χ");
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            bool wasPickedUp = Inventory.instance.AddItem(item);
            if (wasPickedUp) Destroy(gameObject); // �ɹ�ʰȡ��������Ʒ
        }
    }
}
