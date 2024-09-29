using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks; 

public class GPTMenu : MonoBehaviour
{
    [SerializeField] private string openAiApiKey;
    private OpenAIAPI api;
    private AudioClip audioClip;
    private const int sampleRate = 44100;

    public void Start()
    {
        api = new OpenAIAPI(openAiApiKey);
    }
}
