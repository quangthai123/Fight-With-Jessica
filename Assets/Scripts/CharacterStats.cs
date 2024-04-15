using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    public float damage;
    [SerializeField] private Slider hpSlider;
    void Start()
    {
        hpSlider.maxValue = maxHp;
    }
    private void Update()
    {
        hpSlider.value = currentHp;
    }
    public void DeductHealth(float _damage) 
    { 
        currentHp -= _damage;
    }
}
