using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon; // ȷ����������Ѵ���
    public GameObject itemPrefab;
    public bool isStackable;
}
