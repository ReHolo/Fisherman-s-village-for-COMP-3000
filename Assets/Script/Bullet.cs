using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // 子弹速度
    public float lifetime = 3f; // 子弹存活时间

    void Start()
    {
        // 销毁子弹，避免长期占用内存
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 子弹沿前方向飞行
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 子弹碰撞时销毁
        Destroy(gameObject);

        // 可选：在此处添加命中效果或伤害逻辑
    }
}
