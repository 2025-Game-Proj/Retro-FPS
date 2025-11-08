using System;
using UnityEngine;

// 총기 기능을 담당하는 클래스
public class Gun : MonoBehaviour
{

    public float range = 20f;// 총의 사거리   
    public float verticalRange = 20f;    // 총의 수직 범위 (위아래 범위)

    public float damage = 2f;
    public float fireRate;    // 발사 속도 (초 단위로 다음 발사까지의 대기 시간)

    private float nextTimeToFire;   // 다음 발사 가능 시간

    private BoxCollider gunTrigger;   // 적 감지를 위한 박스 콜라이더


    public EnemyManager enemyManager;

    public LayerMask raycastLayerMask;     // 레이캐스트가 충돌 검사할 레이어 마스크

    // 시작 시 호출되는 함수
    void Start()
    {
        // BoxCollider 컴포넌트 가져오기
        gunTrigger = GetComponent<BoxCollider>();

        gunTrigger.size = new Vector3(1, verticalRange, range);   // 총의 사거리에 맞게 트리거 박스 크기 설정

        gunTrigger.center = new Vector3(0, 0, range * 0.5f);         // 트리거 박스의 중심을 앞쪽으로 이동 (사거리의 절반만큼)
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 && 발사 가능 시간이 되었는지 확인
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }


    void Fire()
    {
        // 트리거 영역 내의 모든 적에 대해 처리
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            // 총에서 적까지의 방향 벡터 계산
            var dir = enemy.transform.position - transform.position;


            RaycastHit hit;

            // 레이캐스트를 발사하여 적과의 시야가 확보되었는지 확인
            // (벽이나 장애물에 가려지지 않았는지 체크)
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {

                if (hit.transform == enemy.transform)
                {

                    enemy.TakeDamage(damage);


                    Debug.DrawRay(transform.position, dir, Color.green);  // 디버그용: 레이를 녹색으로 그려서 시각화

                    Debug.Break();    // 디버그용: 게임 일시정지 (테스트 목적)
                }
            }
        }

        // 다음 발사 가능 시간 설정 (현재 시간 + 발사 대기 시간)
        nextTimeToFire = Time.time + fireRate;
    }


    private void OnTriggerEnter(Collider other)
    {

        Enemy enemy = other.transform.GetComponent<Enemy>();

        // Enemy 컴포넌트가 있다면 (적이라면)
        if (enemy)
        {
            // 적 관리자에 해당 적 추가
            enemyManager.AddEnemy(enemy);
        }
    }


    private void OnTriggerExit(Collider other)
    {

        Enemy enemy = other.transform.GetComponent<Enemy>();

        // Enemy 컴포넌트가 있다면 (적이라면)
        if (enemy)
        {
            // 적 관리자에서 해당 적 제거
            enemyManager.RemoveEnemy(enemy);
        }
    }
}