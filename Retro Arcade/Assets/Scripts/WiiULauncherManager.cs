// ========================================================
// Projeto: Retro Arcade Launcher (Wii U Style)
// Autor: Kauan Miguel Eugenio
// Versão: 1.0 - Unity 6.3
// ========================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WiiULauncherManager : MonoBehaviour
{
    [Header("UI - Menu Principal")]
    [SerializeField] private GameObject mainGridPanel;
    [SerializeField] private AudioSource bgmMenuSource;

    [Header("UI - Canal do Jogo (Pop-up Wii U)")]
    [SerializeField] private GameObject channelPanel;
    [SerializeField] private TextMeshProUGUI gameTitleText;
    [SerializeField] private TextMeshProUGUI gameDescriptionText;
    [SerializeField] private Image gameBannerImage;
    [SerializeField] private AudioSource channelAudioSource;

    [Header("Efeitos Sonoros (SFX)")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip clickSound;

    private string currentGamePath;

    private void Start()
    {
        mainGridPanel.SetActive(true);
        channelPanel.SetActive(false);

        if (bgmMenuSource != null && !bgmMenuSource.isPlaying)
        {
            bgmMenuSource.loop = true;
            bgmMenuSource.Play();
        }
    }

    public void OpenGameChannel(string title, string description, Sprite banner, AudioClip channelJingle, string gamePath)
    {
        PlaySFX(clickSound);

        currentGamePath = gamePath;
        gameTitleText.text = title;
        gameDescriptionText.text = description;
        gameBannerImage.sprite = banner;

        StartCoroutine(FadeAudio(bgmMenuSource, 0.2f, 0.4f));

        if (channelJingle != null)
        {
            channelAudioSource.clip = channelJingle;
            channelAudioSource.Play();
        }

        channelPanel.SetActive(true);
    }

    public void CloseChannel()
    {
        PlaySFX(clickSound);

        channelAudioSource.Stop();
        StartCoroutine(FadeAudio(bgmMenuSource, 1.0f, 0.4f));
        channelPanel.SetActive(false);
    }

    public void LaunchSelectedGame()
    {
        PlaySFX(clickSound);
        Debug.Log("Iniciando o jogo localizado em: " + currentGamePath);
        // Executa o núcleo do PICO-8 / Emulador
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        if (audioSource == null) yield break;

        float startVolume = audioSource.volume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
}