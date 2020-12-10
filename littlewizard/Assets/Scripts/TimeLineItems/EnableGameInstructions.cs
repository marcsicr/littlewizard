using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameInstructions : MonoBehaviour
{
    public GameObject instructionsPrefab;
    public GameObject playerStatusBar;

    private void OnEnable() {
        Instantiate(instructionsPrefab, transform.Find("/UILayout"), false);
        playerStatusBar.SetActive(false);
    }
}
