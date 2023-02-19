using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GameControllerScripts
{
    public class StreamVideo : MonoBehaviour
    {
        public RawImage cover;
        public VideoPlayer videoPlayer;
        //public AudioSource audioSource;

        private void Awake() { StartCoroutine(Player()); }

        IEnumerator Player() {
            if (videoPlayer.isPlaying) {
                yield return new WaitForSeconds(1.5f);
                cover.gameObject.SetActive(false);
            }
        }
    }
}
