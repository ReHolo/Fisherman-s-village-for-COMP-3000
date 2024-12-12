using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 开始游戏
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // 替换为实际的游戏场景名称
    }

    // 打开设置菜单
    public GameObject optionsPanel; // 设置选项面板
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    // 加载游戏
    public void LoadGame()
    {
        Debug.Log("Load Game clicked!");
        // 在这里实现加载存档的功能
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("Quit Game clicked!");
        Application.Quit();
    }
}
