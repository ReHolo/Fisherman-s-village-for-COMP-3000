using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // �� 'I' ��/�رձ���
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
