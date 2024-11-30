using UnityEngine;
using UnityEngine.SubsystemsImplementation;

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
            if (currentHp <= 0)
            {
                switch (gameObject.tag)
                {
                    case "Player":
                        gameObject.GetComponent<MC_attack>().Death();
                        break;
                    case "Bug":
                        gameObject.GetComponent<BugBehavior>().Death();
                        break;
                    default:
                        print("can't find entity");
                        break;
                }
            }
        }
    }
}
