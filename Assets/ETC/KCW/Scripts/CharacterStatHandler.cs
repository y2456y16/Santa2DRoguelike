using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats; //인스펙터에서 객체의 기본 정보들을 입력할 수 있다.
    public CharacterStat CurrentStats { get; private set; } //현재의 스텟 정보

    public List<CharacterStat> statsModifiers = new List<CharacterStat>(); //추가 스텟의 경우 만들어 두었지만 현재는 사용하지 않음.

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats() //베이스의 능력치를 기반으로 현재 능력치 설정
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }
        CurrentStats = new CharacterStat { attackSO = attackSO };
        CurrentStats.maxHealth = baseStats.maxHealth;
        CurrentStats.speed = baseStats.speed;
        CurrentStats.characterName = baseStats.characterName;
        CurrentStats.atk = baseStats.atk;
        CurrentStats.def = baseStats.def;
    }
}
