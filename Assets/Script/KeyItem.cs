using UnityEngine;

public class KeyItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 玩家获得钥匙逻辑
            PlayerInventory playerInv = other.GetComponent<PlayerInventory>();
            if (playerInv != null)
            {
                playerInv.hasKey = true;
                // 显示拾取提示 "已获得钥匙"
                // 播放拾起音效
            }
            Destroy(gameObject); // 拾取后移除钥匙
        }
    }
}
