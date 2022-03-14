using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class TvController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    public void TurnOnOff()
    {
        if (this.videoPlayer.isPlaying)
        {
            this.videoPlayer.Pause();
        }
        else
        {
            this.videoPlayer.Play();
        }
    }

    public void ChangeVideo(string _url)
    {
        this.videoPlayer.url = _url;
        this.videoPlayer.Prepare();
        StartCoroutine(loadVideoFromURL());
    }

    private IEnumerator loadVideoFromURL()
    {
        while (this.videoPlayer.isPrepared == false)
        {
            yield return null;
        }
        this.videoPlayer.Play();
    }
}
