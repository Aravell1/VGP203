using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public Canvas enemyCanvas;
    public Slider healthBar;
    public Text healthText;

    int _health = 500;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value <= 0)
                Destroy(gameObject);

            healthBar.value = value;
            healthText.text = value.ToString();
            _health = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = _health;
        healthBar.value = healthBar.maxValue;
        healthText.text = _health.ToString();
    }

    private void Update()
    {
        enemyCanvas.transform.LookAt(GameObject.Find("FpsCam").transform);
    }
}
