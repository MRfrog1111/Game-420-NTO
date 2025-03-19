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
                print("death"+gameObject.tag);
                switch (gameObject.tag)
                {
                    case "Player1":
                        //gameObject.transform.position = new Vector3(1,1,1);
                        currentHp = maxHp;
                        gameObject.GetComponentInParent<MC_attack>().Death();
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
