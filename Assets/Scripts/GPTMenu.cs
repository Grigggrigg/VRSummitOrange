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
    private AudioClip alcohol;
    [SerializeField]
    private AudioClip empathy;

    [SerializeField]
    private string meditationScene;
    [SerializeField] private string alcoholScene;
    [SerializeField] private string empathyScene;

 [SerializeField]
    private AudioSource audioSource;

    public async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);
        await PlayAudio(introduction);
    }

    public async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            await PlayAudio(meditation);
            SceneManager.LoadScene(meditationScene);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            await PlayAudio(alcohol);
            SceneManager.LoadScene(alcoholScene);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            await PlayAudio(empathy);
            SceneManager.LoadScene(empathyScene);
        }
    }

    private async Task PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        await UniTask.Delay(TimeSpan.FromSeconds(clip.length + 1), ignoreTimeScale: false);
    }
}
