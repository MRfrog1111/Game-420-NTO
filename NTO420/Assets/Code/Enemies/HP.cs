using UnityEngine;

public class HP : MonoBehaviour
{
    public int currentHp;
    public int maxHp;
    public bool isInvulnerable;

    public void GiveDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHp -= damage;
        }
    }
}
