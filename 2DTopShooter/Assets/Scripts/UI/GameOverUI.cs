using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameOverUI : MonoBehaviour
{
    private PlayerController _player;

    [SerializeField] private GameObject Panel;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _player.OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(object sender, DeathEventArgs e)
    {
        Panel.SetActive(true);
        StartCoroutine(GameOverFade());
    }

    private IEnumerator GameOverFade()
    {
        Image img = Panel.GetComponent<Image>();
        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / 2f;
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
