using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats; //�ν����Ϳ��� ��ü�� �⺻ �������� �Է��� �� �ִ�.
    public CharacterStat CurrentStats { get; private set; } //������ ���� ����

    public List<CharacterStat> statsModifiers = new List<CharacterStat>(); //�߰� ������ ��� ����� �ξ����� ����� ������� ����.

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats() //���̽��� �ɷ�ġ�� ������� ���� �ɷ�ġ ����
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
