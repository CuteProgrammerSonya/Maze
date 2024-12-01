using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] points; // ������ �����, � ������� ����� ��������� ����
    public GameObject bulletPrefab;  // ������ ����
    public Transform shootPoint;  // �����, �� ������� ����� �������� ����
    public float shootingRange = 10f;  // ���������� ��� ��������
    public float shootingInterval = 2f;  // �������� ����� ����������
    public float detectionRange = 15f;  // ������ ����������� ������

    private NavMeshAgent agent;  // ������ �� NavMeshAgent
    private Transform player;  // ������ �� ������
    private int currentPoint = 0;  // ������ ������� ����
    private float nextShotTime = 0f;  // ����� ���������� ��������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // ������� ������ �� ����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ������������� ������ ����
        MoveToNextPoint();
    }

    void Update()
    {
        // ���������, ������ �� ���� ����
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // ���� ������, ��������� � ��������� �����
            MoveToNextPoint();
        }

        // �������� �� ����������� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // ���� ������� �� ������
            transform.LookAt(player);

            // ���� ���� � �������� ������� ��� ��������
            if (distanceToPlayer <= shootingRange && Time.time >= nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + shootingInterval;
            }
        }
    }

    void MoveToNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[currentPoint].position;
        currentPoint = (currentPoint + 1) % points.Length;  // �������� ��������� �����
    }

    void Shoot()
    {
        // ������������ ����� �� ������
        transform.LookAt(player);

        // ����������� �������� (���������� forward, ����� ���� ��������� � ������� �����)
        Vector3 shootingDirection = shootPoint.forward;

        // ������� ���� �� ����� �������� � ������ �����������
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(shootingDirection));
    }


}
