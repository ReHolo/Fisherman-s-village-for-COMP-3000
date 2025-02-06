using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // ����ģʽ
    public List<Item> items = new List<Item>(); // �洢������Ʒ
    public int maxSlots = 10; // �����������

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback; // UI ���»ص�

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots) return false; // ��������
        items.Add(item);
        onInventoryChangedCallback?.Invoke(); // ֪ͨ UI ����
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        onInventoryChangedCallback?.Invoke();
    }
}
