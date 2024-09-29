using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;
using System.Net.Http;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GPTMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip introduction;
    [SerializeField]
    private AudioClip meditation;
    [SerializeField]
    private AudioClip meditationUser;
    [SerializeField]
    private AudioClip alcohol;
    [SerializeField]
    private AudioClip alcoholUser;
    [SerializeField]
    private AudioClip empathy;
    [SerializeField]
    private AudioClip empathyUser;

    [SerializeField]
    private string meditationScene;
    [SerializeField] private string alcoholScene;
    [SerializeField] private string empathyScene;

 [SerializeField]
    private AudioSource audioSource;

    private bool canPlay;

    public async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);
        PlayAudio(introduction);
        await UniTask.Delay(TimeSpan.FromSeconds(introduction.length + 1), ignoreTimeScale: false);
        canPlay = true;
    }

    public async void Update()
    {
        if (!canPlay) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            canPlay = false;
            PlayAudio(meditationUser);
            await UniTask.Delay(TimeSpan.FromSeconds(meditationUser.length + 1), ignoreTimeScale: false);
            PlayAudio(meditation);
            await UniTask.Delay(TimeSpan.FromSeconds(meditation.length + 1), ignoreTimeScale: false);
            SceneManager.LoadScene(meditationScene);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            canPlay = false;
            PlayAudio(alcoholUser);
            await UniTask.Delay(TimeSpan.FromSeconds(alcoholUser.length + 1), ignoreTimeScale: false);
            PlayAudio(alcohol);
            await UniTask.Delay(TimeSpan.FromSeconds(alcohol.length + 1), ignoreTimeScale: false);
            SceneManager.LoadScene(alcoholScene);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            canPlay = false;
            PlayAudio(empathyUser);
            await UniTask.Delay(TimeSpan.FromSeconds(empathyUser.length + 1), ignoreTimeScale: false);
            PlayAudio(empathy);
            await UniTask.Delay(TimeSpan.FromSeconds(empathy.length + 1), ignoreTimeScale: false);
            SceneManager.LoadScene(empathyScene);
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
