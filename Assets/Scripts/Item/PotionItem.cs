using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    protected override void Start()
    {
        base.Start();
    }
    public override void Use(GameObject target)
    {
        base.Use(target);

        PotionData data = base.data as PotionData;
        if (data.Type == ItemType.Useable)
        {
            switch (data.potionType)
            {
                case PotionType.Attack:
                    StartCoroutine(IncreaseAttack(target, 5f));
                    break;
                case PotionType.Defense:
                    StartCoroutine(IncreaseDefense(target, 5f));
                    break;
                case PotionType.GodMode:
                    StartCoroutine(IncreaseAttackAndDefense(target, 10f));
                    break;
            }
            //방 안에 보스를 제외한 모든 몬스터 사망
            //방 안에 모든 몬스터 일시정지
            //10초동안 공격력 2배 상승
            
        }
    }

    public IEnumerator IncreaseAttack(GameObject target, float durationTime)
    {
        IsUsed = true;
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.atk = playerStat.CurrentStats.atk;
        playerStat.CurrentStats.atk += newStat.atk;
        yield return new WaitForSeconds(durationTime);
        playerStat.CurrentStats.atk -= newStat.atk;
        IsUsed = false;
        if (count == 0)
        {
            ItemManager.Instance.RemoveItem(data.ID);
            Destroy(gameObject);
        }
    }
    public IEnumerator IncreaseDefense(GameObject target, float durationTime)
    {
        IsUsed = true;
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.def = playerStat.CurrentStats.def;
        playerStat.CurrentStats.def += newStat.def;
        yield return new WaitForSeconds(durationTime);
        playerStat.CurrentStats.def -= newStat.def;
        IsUsed = false;
        if (count == 0)
        {
            ItemManager.Instance.RemoveItem(data.ID);
            Destroy(gameObject);
        }
        
    }
    public IEnumerator IncreaseAttackAndDefense(GameObject target, float durationTime)
    {
        IsUsed = true;
        CharacterStatsHandler playerStat = target.GetComponent<CharacterStatsHandler>();
        CharacterStats newStat = new CharacterStats();
        newStat.atk = playerStat.CurrentStats.atk;                
        newStat.def = playerStat.CurrentStats.def;
        playerStat.CurrentStats.atk += newStat.atk;
        playerStat.CurrentStats.def += newStat.def;
        yield return new WaitForSeconds(durationTime);
        playerStat.CurrentStats.def -= newStat.def;
        playerStat.CurrentStats.atk -= newStat.atk;
        IsUsed = false;
        if (count == 0)
        {
            ItemManager.Instance.RemoveItem(data.ID);
            Destroy(gameObject);
        }
    }
}

