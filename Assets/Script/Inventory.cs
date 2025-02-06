using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // 单例模式
    public List<Item> items = new List<Item>(); // 存储所有物品
    public int maxSlots = 10; // 背包最大容量

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback; // UI 更新回调

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots) return false; // 背包已满
        items.Add(item);
        onInventoryChangedCallback?.Invoke(); // 通知 UI 更新
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        onInventoryChangedCallback?.Invoke();
    }
}
