using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowHolder : MonoBehaviour {

    public int itemID;
    public Text titel;
    public Button background;
    public Image image;
    //public Text happinessFactor;

    void Start()
    {
        Button btn = background.GetComponent<Button>();
        btn.onClick.AddListener(hideModal);
    }

    public void hideModal()
    {
        Destroy(this.gameObject);
    }
}
