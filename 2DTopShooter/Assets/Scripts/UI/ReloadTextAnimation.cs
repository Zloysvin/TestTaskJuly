using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReloadTextAnimation : MonoBehaviour
{
    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    private PlayerController _player;

    [Range(0.0f, 5.0f)] public float xOffset = 3.3f;
    [Range(0.0f, 5.0f)] public float yOffset = 2.5f;
    [Range(0.0f, 5.0f)] public float timeOffset = 1f;
    [Range(0.0f, 5.0f)] public float timeMultiplyer = 1.0f;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _player.OnDamageTaken += _player_OnDamageTaken;
    }

    private void _player_OnDamageTaken(object sender, DamageTakenEventArgs e)
    {
        xOffset = (3.5f * (e.MaxHp - e.Hp) / e.MaxHp) + 1.5f;
        yOffset = 3.5f * (e.MaxHp - e.Hp) / e.MaxHp;
        timeMultiplyer = (4f * (e.MaxHp - e.Hp) / e.MaxHp) + 1f;
    }

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();

        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();

        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i) * timeMultiplyer;

            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }



        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * xOffset), Mathf.Cos(time * yOffset));
    }
}
