using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    List<Vector3> initialPositions = new List<Vector3>();
    List<Quaternion> initialRotations = new List<Quaternion>();
    List<Rigidbody> rigidBodies = new List<Rigidbody>();
    public static List<Vector3> velocities = new List<Vector3>();
    public Vector3 impulse = new Vector3(0.0f, 20.0f, 20.0f);


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Rigidbody childRB = transform.GetChild(i).GetComponent<Rigidbody>();
            
            rigidBodies.Add(childRB);
            initialPositions.Add(childRB.position);
            initialRotations.Add(childRB.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        velocities.Clear();
        foreach (Rigidbody rb in rigidBodies)
        {
            velocities.Add(rb.velocity);
        }
    }


    public void RollDice()
    {  
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.AddForce(impulse, ForceMode.Impulse);
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }       
    }

    public void ResetDice()
    {
        int index = 0;
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.position = initialPositions[index];
            rb.rotation = initialRotations[index];
            index++;
        }
    }
}
