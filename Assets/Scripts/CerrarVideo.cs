using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
//

using UnityEngine.Video;

public class CerrarVideo: MonoBehaviour
{
    public VideoClip[] videos;
    private VideoPlayer video;
    private int count = 0;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        PlayVideo();
    }

    void CheckOver (VideoPlayer vp)
    {
        if (count < videos.Length) PlayVideo();
        else SceneManager.LoadScene("Mundo1");
    }

    void PlayVideo()
    {
        video.clip = videos[count];
        video.Play();
        video.loopPointReached += CheckOver;
        count++;
    }
}