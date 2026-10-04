// ========================================================
// Projeto: Retro Arcade Launcher - Sistema de Conquistas
// Autor: Kauan Miguel Eugenio
// ========================================================

using System.Collections;
using UnityEngine;
using TMPro;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }

    [Header("Componentes do Pop-up")]
    [SerializeField] private GameObject achievementBanner;
    [SerializeField] private TextMeshProUGUI achievementTitleText;
    [SerializeField] private AudioSource achievementAudioSource;
    [SerializeField] private AudioClip unlockSound;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (achievementBanner != null)
            achievementBanner.SetActive(false);
    }

    public void Unlock(string achievementName)
    {
        StopAllCoroutines();
        StartCoroutine(ShowAchievementRoutine(achievementName));
    }

    private IEnumerator ShowAchievementRoutine(string title)
    {
        achievementTitleText.text = "Conquista: " + title;
        achievementBanner.SetActive(true);

        if (achievementAudioSource != null && unlockSound != null)
        {
            achievementAudioSource.PlayOneShot(unlockSound);
        }

        yield return new WaitForSeconds(3.5f);

        achievementBanner.SetActive(false);
    }
}