using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBtn : MonoBehaviour
{
    GameObject spellBook;
    void Start()
    {
        spellBook = transform.parent.parent.Find("SpellsBook").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnClick() {


        spellBook.SetActive(!spellBook.activeInHierarchy);
    }
}
