using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    public Transform[] animatedTransform;
    public ConfigurableJoint[] joints; //LAST JOINT SHOULD BE HIP

    Quaternion[] initalRotations;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initalRotations = new Quaternion[joints.Length];

        for (int i = 0; i < joints.Length; i++) {
            initalRotations[i] = joints[i].transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < joints.Length - 1; i++)
        {
            ConfigurableJointExtensions.SetTargetRotationLocal(joints[i], animatedTransform[i].rotation, initalRotations[i]);
        }
    }
}
