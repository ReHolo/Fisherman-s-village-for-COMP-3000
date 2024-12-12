using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;   // 动画控制器
    public KeyCode interactKey = KeyCode.E;   // 互动按键
    private bool isOpen = false;   // 门的开关状态

    void Update()
    {
        // 检测按键并执行动作
        if (Input.GetKeyDown(interactKey))
        {
            if (!isOpen)
            {
                doorAnimator.Play("DoorOpen");   // 播放开门动画
                isOpen = true;
            }
            else
            {
                doorAnimator.Play("DoorOpen", -1, 1f);  // 反向播放动画
                isOpen = false;
            }
        }
    }
}
