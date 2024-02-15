using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBoxItem : Item
{
    protected override void Start()
    {
        base.Start();
    }

    //화면 안에 보스를 제외한 모든 몬스터 사망
    public override void Use(GameObject target)
    {
        base.Use(target);
        if (data.Type == ItemType.Useable)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");

            Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.farClipPlane));

            // 지정된 영역 내의 모든 콜라이더를 찾음
            Collider2D[] enemiesInView = Physics2D.OverlapAreaAll(bottomLeft, topRight, layerMask);
            foreach (var enemyCollider in enemiesInView)
            {
                if (enemyCollider.gameObject.CompareTag("Boss"))
                {
                    continue;
                }
                // 적에게 무한 대미지를 줘서 죽임
                HealthSystem health = enemyCollider.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.ChangeHealth(-100000);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        // 카메라 뷰포트의 4개 코너를 월드 좌표로 변환
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector2 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        Vector2 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));

        // Gizmos 색상 설정
        Gizmos.color = Color.red;

        // 사각형 그리기
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}
