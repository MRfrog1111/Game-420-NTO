using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotMoving : MonoBehaviour
{
    private Vector3 target;
    public bool isRobotMoving = false;
    public float plusPos;
    public float speed;
    public GameObject robot;
    private bool IsProgrammRunning = false;
    private string algo = "";

    private int currentCommand = 0;

    private Vector3 startPosition;

    private string returnToScene;

    [SerializeField] private Sprite gameOverSprite;
    private Sprite defaultSprite;

    private Vector3 startRotation;
    // Start is called before the first frame update
    void Start()
    {
        isRobotMoving = false;
        startPosition = transform.position;
        defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        returnToScene = PlayerPrefs.GetString("ReturnToSceneName");
        startRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsProgrammRunning)
        {
            if (isRobotMoving)
            {
                robot.transform.position =
                    Vector3.MoveTowards(robot.transform.position, target, speed *Time.deltaTime);
                print("time"+Time.deltaTime);
                if (Vector3.Distance(robot.transform.position, target) <= 0.0001)
                {
                    print("target");
                    isRobotMoving = false;
                    ChangeState();
                }
            }
        }

        //target = new Vector3(robot.transform.position.x+plusPos, robot.transform.position.y, robot.transform.position.z);
    }

   public  void StartProgramm(string algorithm)
   {
       transform.position = startPosition;
       transform.eulerAngles = startRotation;
        IsProgrammRunning = true;
        algo = algorithm;
        currentCommand = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        print(algo.Length);
        ChangeState();
    }

    private void ChangeState()
    {
        currentCommand++;
        if (currentCommand < algo.Length)
        {
            print("Test"+currentCommand+" "+algo[currentCommand]);
            if (algo[currentCommand] == '0')
            {
                RotateRobot();
            }
            else if(algo[currentCommand] == '1')
            {
                MoveRobot();
            }
        }
        else
        {
            IsProgrammRunning = false;
            print("END");
        }
    }
    
    public void MoveRobot()
    {
        switch (transform.eulerAngles.y)
        {
            case 0:
                target = new Vector3(robot.transform.position.x+plusPos, robot.transform.position.y, robot.transform.position.z);
                break;
            case 90:
                target = new Vector3(robot.transform.position.x, robot.transform.position.y, robot.transform.position.z-plusPos);
                break;
            case 180:
                target = new Vector3(robot.transform.position.x-plusPos, robot.transform.position.y, robot.transform.position.z);
                break;
            case 270:
                target = new Vector3(robot.transform.position.x, robot.transform.position.y, robot.transform.position.z+plusPos);
                break;
        }
        print("target" + target);
        isRobotMoving = true;
    }

    public void RotateRobot()
    {
        isRobotMoving = false;
        robot.transform.eulerAngles = new Vector3(90, robot.transform.eulerAngles.y + 90, 0);
        ChangeState();
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("StopRobot"))
        {
            IsProgrammRunning = false;
            isRobotMoving = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = gameOverSprite;
            print("gameOver");
        }
        else if (coll.gameObject.name == "Finish")
        {
            IsProgrammRunning = false;
            isRobotMoving = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerStats>().resources.stage++;
            player.GetComponent<CharacterEnabler>().ChangeState(true);
            SceneManager.LoadScene(returnToScene);
        }
    }
}
