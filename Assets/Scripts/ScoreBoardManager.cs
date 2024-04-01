using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardManager : MonoBehaviour
{
    public TextMeshProUGUI textScores;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(UIManager.Instance.scoreBoardTexts);
        
        textScores.text = UIManager.Instance.scoreBoardTexts;
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
