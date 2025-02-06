using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // 按 'I' 打开/关闭背包
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
