using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberGenerator : MonoBehaviour
{
    public static DamageNumberGenerator _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private GameObject damageNumberPrefab;
    [SerializeField]
    private Transform damageNumberParent;


    public void InstantiateDamageNumber(int dmg, Vector3 pos, Color color, TMPro.FontStyles fontStyle)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-1f,1f), 0.5f, 0f);

        //Transform dmgNumber = Instantiate(damageNumberPrefab, pos + randomOffset, Quaternion.identity).transform;
        Transform dmgNumber = Instantiate(damageNumberPrefab, damageNumberParent).transform;
        dmgNumber.position += randomOffset;
        dmgNumber.GetComponentInChildren<DamageNumber>().Setup(dmg, color, fontStyle);
    }
}
