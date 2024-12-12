using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ��ʼ��Ϸ
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // �滻Ϊʵ�ʵ���Ϸ��������
    }

    // �����ò˵�
    public GameObject optionsPanel; // ����ѡ�����
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    // ������Ϸ
    public void LoadGame()
    {
        Debug.Log("Load Game clicked!");
        // ������ʵ�ּ��ش浵�Ĺ���
    }

    // �˳���Ϸ
    public void QuitGame()
    {
        Debug.Log("Quit Game clicked!");
        Application.Quit();
    }
}
