using UnityEngine;

public class DepositoAgua : MonoBehaviour
{
    [Header("Simulación")]
    public float initialVolume = 0f;
    public float actualVolume;
    public float waterInput = 30f;
    public float waterOutput = 20f;
    public float maxCapacity = 500f;

    [Header("Tiempo")]
    public float secondsPerMinute = 1f;
    private int minute = 0;
    private float timer = 0;

    [Header("Visual")]
    public GameObject water;
    public float tankHeight = 5f;

    void Start()
    {
        actualVolume = initialVolume;

        UpdateWater();

        Debug.Log("Minuto " + minute + ": " + actualVolume + " L de agua");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsPerMinute)
        {
            timer = 0;
            SimulateMinute();
        }
    }

    void SimulateMinute()
    {
        minute++;

        float input = 0;
        float output = 0;

        if (actualVolume < maxCapacity)
        {
            input = waterInput;
        }

        if (actualVolume > 0)
        {
            output = waterOutput;
        }

        actualVolume = actualVolume + input - output;

        if (actualVolume > maxCapacity)
        {
            actualVolume = maxCapacity;
        }

        if (actualVolume < 0)
        {
            actualVolume = 0;
        }

        UpdateWater();

        Debug.Log("Minuto " + minute + ": " + actualVolume + " L de agua");

        if (actualVolume >= maxCapacity)
        {
            Debug.Log("El tanque esta lleno");
        }
        else if (actualVolume <= 0)
        {
            Debug.Log("El tanque esta vacio");
        }
    }

    void UpdateWater()
    {
        float percentage = actualVolume / maxCapacity;

        Vector3 scale = water.transform.localScale;
        scale.y = percentage * tankHeight;

        water.transform.localScale = scale;

        Vector3 position = water.transform.localPosition;
        position.y = (scale.y - tankHeight) / 2;

        water.transform.localPosition = position;
    }
}