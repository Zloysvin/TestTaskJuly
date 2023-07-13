using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoEffect : MonoBehaviour
{
    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    public Gradient rainbow;

    [Range(0.0f, 5.0f)]public float xOffset = 3.3f;
    [Range(0.0f, 5.0f)] public float yOffset = 2.5f;
    [Range(0.0f, 5.0f)] public float timeOffset = 1f;
    [Range(0.0f, 5.0f)] public float timeMultiplyer = 1.0f;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        Color[] colors = mesh.colors;

        //for (int w = 0; w < wordIndexes.Count; w++)
        //{
        //    int wordIndex = wordIndexes[w];
        //    Vector3 offset = Wobble(Time.time + w);

        //    for (int i = 0; i < wordLengths[w]; i++)
        //    {
        //        TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex + i];
        //        TMP_CharacterInfo cww = textMesh.textInfo.characterInfo[i];

        //        int index = c.vertexIndex;
        //        int indexww = cww.vertexIndex;

        //        colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
        //        colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
        //        colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
        //        colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));

        //        vertices[indexww] += offset;
        //        vertices[indexww + 1] += offset;
        //        vertices[indexww + 2] += offset;
        //        vertices[indexww + 3] += offset;


        //    }

        //}

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i) * timeMultiplyer;

            colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.0001f, timeOffset));
            colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.0001f, timeOffset));
            colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.0001f, timeOffset));
            colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.0001f, timeOffset));

            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }



        mesh.vertices = vertices;
        mesh.colors = colors;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * xOffset), Mathf.Cos(time * yOffset));
    }
}
