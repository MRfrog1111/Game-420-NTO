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
                        //gameObject.transform.position = new Vector3(1,1,1);
                        gameObject.GetComponent<MC_attack>().Death();
                        currentHp = maxHp;
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
