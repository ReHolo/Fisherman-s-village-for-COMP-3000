using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // �ӵ��ٶ�
    public float lifetime = 3f; // �ӵ����ʱ��

    void Start()
    {
        // �����ӵ������ⳤ��ռ���ڴ�
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // �ӵ���ǰ�������
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �ӵ���ײʱ����
        Destroy(gameObject);

        // ��ѡ���ڴ˴��������Ч�����˺��߼�
    }
}
