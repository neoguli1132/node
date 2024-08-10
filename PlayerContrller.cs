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
    public float bulletLifetime = 5f;  // 총알이 사라지기까지의 시간
    public float casingLifetime = 3f;  // 탄피가 사라지기까지의 시간


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
      

        // 마우스 위치 얻기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100); // 화면 밖 멀리 점으로 설정
        }

        // 총알 발사 방향 계산
        Vector3 direction = (targetPoint - transform.position).normalized;
        direction.y = 0;  // 쿼터뷰에서는 y 축을 무시

        // 플레이어를 발사 방향으로 회전
        transform.forward = direction;

        // 총알 생성 및 발사
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

        // 일정 시간 후 총알 삭제
        Destroy(bullet, bulletLifetime);

        // 탄피 생성 및 제거
        GameObject casing = Instantiate(casingPrefab, firePoint.position, Quaternion.identity);

        // 탄피에 초기 힘을 가해 튀어오르도록 하기
        Rigidbody casingRb = casing.GetComponent<Rigidbody>();
        if (casingRb != null)
        {
            // 탄피가 약간의 힘으로 위로 튀었다가 아래로 떨어지게 설정
            Vector3 casingForce = new Vector3(Random.Range(-1f, 1f), 2f, Random.Range(-1f, 1f));
            casingRb.AddForce(casingForce, ForceMode.Impulse);
        }

        // 일정 시간 후 탄피 삭제
        Destroy(casing, casingLifetime);
    }
}