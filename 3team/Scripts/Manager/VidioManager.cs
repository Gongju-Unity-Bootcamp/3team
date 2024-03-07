using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidioManager : MonoBehaviour
{
    public VideoPlayer video;

    public void Init()
    {
        video = GetComponent<VideoPlayer>();
    }
    public void VideoPlay(string video)
    {
        this.video.clip = Manager.Resources.LoadVideo(video);
    }
    public void VideoStop()
    {
        this.video.Stop();
    }

    public bool IsVideoPlay()
    {
        return video.isPlaying;
    }
}
