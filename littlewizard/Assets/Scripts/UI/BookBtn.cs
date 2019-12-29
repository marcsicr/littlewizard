using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBtn : MonoBehaviour
{
    bool isOpen = false;
    public GameObject bookPrefab;
    SpellsBook book;
    void Start(){
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnClick() {

        if (!isOpen) {

            book = Instantiate(bookPrefab, transform.Find("/UILayout"), false).GetComponent<SpellsBook>();
            isOpen = true;
        } else {

            Destroy(book.gameObject);
            isOpen = false;
        }
        
       
    }
}
