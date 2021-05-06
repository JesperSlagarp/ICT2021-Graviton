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
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActiveAndEnabled && isShooting)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }

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
        Debug.Log("Laser mount phase 1");
        foreach (GameObject laser in lasers)
        {
            if(i % 2 == 0)
                laser.SetActive(true);
            i++;
        }
    }

    public void phaseTwo()
    {
        Debug.Log("Laser mount phase 2");
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }
}
