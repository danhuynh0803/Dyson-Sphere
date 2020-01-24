using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour {

    [Header("Health Settings")]
    public int maxHealth;
    protected int health;
    public Image healthBarImage;

    public int damage;
    protected abstract void Death();

    public virtual void TakeDamage(int damage)
    {
        health = (int)Mathf.Clamp(health - damage, 0f, maxHealth);
        healthBarImage.fillAmount = (float)health / maxHealth;
        if (health <= 0)
        {
            Death();
        }
    }
}
