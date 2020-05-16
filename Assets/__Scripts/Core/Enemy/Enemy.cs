using PinguinoKatano.Core.Movement;
using UnityEngine;

namespace PinguinoKatano.Core.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth;
        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth < 0)
            {
                Debug.Log("Enemy is dead");
            }
        }
    }
}