using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon; // 确保这个变量已存在
    public GameObject itemPrefab;
    public bool isStackable;
}
