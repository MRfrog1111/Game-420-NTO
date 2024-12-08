using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoChek : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject button;
    public GameObject image;
    
    
    
    private void Start()
    {
        videoPlayer.Play();
        button.SetActive(false);
        image.SetActive(false);
        StartCoroutine(ShowButton());
    }
    
    void Update()
    {
        if (button != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
        
    }
    private IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(120f / videoPlayer.playbackSpeed);
        button.SetActive(true);
        image.SetActive(true);
        
    }
}
