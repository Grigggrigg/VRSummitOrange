using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform target;
    public GameObject objectToThrow;
    
    [Header("Settings")]
    public float throwCooldown = 0.2f;


    [Header("Throwing")]
    public bool isSelected;
    public float throwForce = 10;
    public float throwUpwardForce;

    public bool isThrowing;
    bool readyToThrow;

    void Start()
    {
        readyToThrow = true;
    }

    public void Select()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (isSelected && readyToThrow)
        {
            Throw();
        }

    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, cam.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = (target.transform.position - cam.transform.position) * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);


        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
