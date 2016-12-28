using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class MyReplay : MonoBehaviour
{
    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private Rigidbody rigidBody;
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            gameManager.recording = !gameManager.recording;
            Debug.Log("Recording Switched to " + gameManager.recording);
        }


        if (gameManager.recording)
        {
            Record();
        }
        else
        {
            PlayBack();
        }
    }

    private void Record()
    {
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;

        rigidBody.isKinematic = false;

        //Debug.Log("Recording keyframe: " + frame);
        keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
    }

    void PlayBack()
    {
        int frame = Time.frameCount % bufferFrames;

        rigidBody.isKinematic = true;

        //Debug.Log("Recording keyframe: " + frame);
        transform.position = keyFrames[frame].position;
        transform.rotation = keyFrames[frame].rotation;
    }
}


/// <summary>
/// Struct to store time, rotation, position.
/// </summary>

public struct MyKeyFrame
{
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public MyKeyFrame(float aFrameTime, Vector3 aPosition, Quaternion aRotation)
    {
        frameTime = aFrameTime;
        position = aPosition;
        rotation = aRotation;
    }
}