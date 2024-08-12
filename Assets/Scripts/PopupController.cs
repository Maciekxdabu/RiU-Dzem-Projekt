using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private float popupLifetime = 2f;

    //singleton
    private static PopupController _instance;
    public static PopupController Instance { get { return _instance; } }

    // ---------- Unity methods

    private void Awake()
    {
        _instance = this;
    }

    // ---------- public static methods

    public static void SpawnPopup(string message, Vector3 position)
    {
        Popup obj = Instantiate(Instance.popupPrefab, position, Quaternion.identity).GetComponent<Popup>();

        if (obj != null)
        {
            obj.Init(Instance.popupLifetime, message);
        }
    }
}
