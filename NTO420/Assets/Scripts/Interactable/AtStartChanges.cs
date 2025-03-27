using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtStartChanges : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isUpdated = false;
    private GameObject player;
    [SerializeField] private Craft craft;

    [SerializeField] private Generator1 generator;

    [SerializeField] private Paseka paseka;

    [SerializeField] private GameObject[] resources;
    [SerializeField] private Mesh[] defaultMeshes;
    //private Tutorial tutor;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         print(player.name);
         player.transform.position = new Vector3(0, 7, 0);
         foreach (Transform child in player.transform)
         {
             child.transform.localPosition = new Vector3(0, 0, 0);
         }
         player.GetComponent<CharacterEnabler>().ChangeState(true);
         foreach (Transform child in player.transform)
         {
             if (child.gameObject.name == "UI1")
             {
                 child.gameObject.SetActive(true);
             }
         }

         for(int i = 0; i < resources.Length; i++)
         {
             if (resources[i].GetComponent<MeshFilter>().mesh == null)
             {
                 resources[i].GetComponent<MeshFilter>().mesh = defaultMeshes[i];
             }
         }
         //GameObject.Find("PlayerCapsule").transform.localPosition = new Vector3(0, 0, 0);
         StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
        craft.FirstUpdate();
        player.GetComponentInChildren<Tutorial>().FirstUpdate();
        paseka.FirstUpdate();
        generator.FirstUpdate();
    }
}
