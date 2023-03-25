using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPopupHandle : MonoBehaviour
{
    public static AllPopupHandle Instance;
    [SerializeField] public List<GameObject> LstPopup;
    [SerializeField] GameObject canvas;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AllPopupHandle>();
        }

        if (canvas == null)
        {
            canvas = FindObjectOfType<GameCanvas>().gameObject;
        }
    }


    public void InitPopup(NamePopups Name)
    {
        if (canvas == null)
        {
            canvas = FindObjectOfType<GameCanvas>().gameObject;
        }
        GameObject Popup = LstPopup.Find(popup => popup != null && popup.GetComponent<PopupBase>().GetNamePopup() == Name);

        if (Popup.gameObject)
        {
            var popup = Instantiate(Popup.gameObject, canvas.transform);
            popup.GetComponent<PopupBase>().Show();
        }

    }
}
