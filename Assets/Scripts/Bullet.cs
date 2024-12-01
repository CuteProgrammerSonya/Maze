using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  // �������� ����
    public float lifetime = 3f;  // ����� ����� ����
    public float damage = 10f;  // ���� ����

    void Start()
    {
        // ���������� ���� ����� �������� �����
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // ����������� ���� �����
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
