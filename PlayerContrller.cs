using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContrller : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f;  // �Ѿ��� ������������ �ð�
    public float casingLifetime = 3f;  // ź�ǰ� ������������ �ð�


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
      

        // ���콺 ��ġ ���
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100); // ȭ�� �� �ָ� ������ ����
        }

        // �Ѿ� �߻� ���� ���
        Vector3 direction = (targetPoint - transform.position).normalized;
        direction.y = 0;  // ���ͺ信���� y ���� ����

        // �÷��̾ �߻� �������� ȸ��
        transform.forward = direction;

        // �Ѿ� ���� �� �߻�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

        // ���� �ð� �� �Ѿ� ����
        Destroy(bullet, bulletLifetime);

        // ź�� ���� �� ����
        GameObject casing = Instantiate(casingPrefab, firePoint.position, Quaternion.identity);

        // ź�ǿ� �ʱ� ���� ���� Ƣ��������� �ϱ�
        Rigidbody casingRb = casing.GetComponent<Rigidbody>();
        if (casingRb != null)
        {
            // ź�ǰ� �ణ�� ������ ���� Ƣ���ٰ� �Ʒ��� �������� ����
            Vector3 casingForce = new Vector3(Random.Range(-1f, 1f), 2f, Random.Range(-1f, 1f));
            casingRb.AddForce(casingForce, ForceMode.Impulse);
        }

        // ���� �ð� �� ź�� ����
        Destroy(casing, casingLifetime);
    }
}