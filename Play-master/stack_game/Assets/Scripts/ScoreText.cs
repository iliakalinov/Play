using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Отображения прогресса во время игры
/// </summary>
public class ScoreText : MonoBehaviour
{
    public int score=-1;            //текущий счет 
    private TextMeshProUGUI text;   // TextMeshProUGUI для отображения числа объектов поставленных
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.OnCubeSpawned += GameManager_OnCubeSpawner;

    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawned -= GameManager_OnCubeSpawner;

    }

    /// <summary>
    /// при создании куба увеличить счет 
    /// </summary>
    private void GameManager_OnCubeSpawner()
    {

        score++;
        text.text = "Score :" + score.ToString();

    }


}
