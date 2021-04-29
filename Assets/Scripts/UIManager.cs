using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject coinCountDisplay;
    [SerializeField] private GameObject livesCountDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI (int newCoinCount, int newLivesCount)
    {       
        coinCountDisplay.GetComponent<TextMeshProUGUI>().text = "Coins x" + newCoinCount;
        livesCountDisplay.GetComponent<TextMeshProUGUI>().text = "Lives: " + newLivesCount;
    }
}
