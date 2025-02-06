using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item; // 物品数据
    private bool isPlayerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("靠近物品: " + item.itemName + "，按 E 拾取");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("离开物品范围");
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            bool wasPickedUp = Inventory.instance.AddItem(item);
            if (wasPickedUp) Destroy(gameObject); // 成功拾取后销毁物品
        }
    }
}
