using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class laserMount : MonoBehaviour
{

    [SerializeField]
    private float baseRotationSpeed;

    [SerializeField]
    private List<GameObject> lasers;

    private float rotationSpeed;
    private bool isShooting;


    private void Awake()
    {
        rotationSpeed = baseRotationSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActiveAndEnabled && isShooting)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }

    }

    public void SetActive(bool active) {
        gameObject.SetActive(active);
    }

    private void OnEnable()
    {
        Debug.Log("Laser is active");
        isShooting = true;
        phaseOne();
    }

    private void OnDisable()
    {
        Debug.Log("Laser is inactive");
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }

    public void phaseOne() {
        int i = 2;
        foreach (GameObject laser in lasers)
        {
            if(i % 2 == 0)
                laser.SetActive(true);
            i++;
        }
    }

    public void phaseTwo()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }
}
