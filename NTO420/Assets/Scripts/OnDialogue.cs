using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class OnDialogue : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player1"))
        {
            ConversationManager.Instance.StartConversation(conversation);
            StartCoroutine(DestroyTrigger());

        }

    }

    private IEnumerator DestroyTrigger()
    {
        yield return new WaitForSeconds(15f);
        transform.gameObject.SetActive(false);
    }
}
