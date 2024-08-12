using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float moveDistance = 1f;

    private Vector3 startPosition;
    private float maxLifetime = 0f;
    private float lifeTime = 1f;

    // ---------- Unity messages

    void Update()
    {
        if (maxLifetime > 0f)
        {
            lifeTime -= Time.deltaTime;

            Color color = text.color;
            color.a = lifeTime / maxLifetime;
            text.color = color;

            transform.position = startPosition + Vector3.up * moveDistance * lifeTime / maxLifetime;

            if (lifeTime < 0f)
                Destroy(gameObject);
        }
    }

    // ---------- public methods

    public void Init(float _maxLifetime, string message)
    {
        startPosition = transform.position;
        lifeTime = _maxLifetime;
        maxLifetime = _maxLifetime;
        text.text = message;
    }
}
