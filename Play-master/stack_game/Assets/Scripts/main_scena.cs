using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// для отображения сохраненных данных, стартовая сцена
/// </summary>
public class main_scena : MonoBehaviour
{
    private int MaxCount=0;                //максимальное значение прогресса
    public TextMeshProUGUI Textrecord;     //TextMeshProUGUI для вывода рекорда
    public TextMeshProUGUI Lastgame;       //TextMeshProUGUI для вывода результата прошлой игры


    void Start()            //чтение сохраненных данных
    {               
        int CountSave=0;
        int LastGame = 0;    
            CountSave = PlayerPrefs.GetInt("Max_count");//чтение макс значения
            LastGame = PlayerPrefs.GetInt("LastGame");//чтение предыдущей игры   
        MaxCount = CountSave;
        Textrecord.text = "Best result:" + MaxCount.ToString();
        Lastgame.text = "Last game:" + LastGame.ToString();


    }
/// <summary>
/// для запуска уровня с игрой
/// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
