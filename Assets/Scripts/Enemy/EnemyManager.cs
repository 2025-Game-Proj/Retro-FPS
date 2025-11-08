using System.Collections.Generic;
using UnityEngine;

// 적 캐릭터들을 관리하는 매니저 클래스
public class EnemyManager : MonoBehaviour
{
    // 트리거 영역 내에 있는 적들의 리스트
    public List<Enemy> enemiesInTrigger = new List<Enemy>();

    // 적을 리스트에 추가하는 메서드
    // 적이 총의 사거리(트리거 영역) 안에 들어왔을 때 호출됨
    public void AddEnemy(Enemy enemy)
    {

        enemiesInTrigger.Add(enemy);
    }


    // 적이 총의 사거리(트리거 영역)에서 벗어났거나 처치되었을 때 호출됨
    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInTrigger.Remove(enemy);
    }
}