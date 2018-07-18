using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapestryHyperimage : MonoBehaviour
{
    public RawImage Image;
    public Camera Camera;
    public Canvas Self;
    public GameObject Parent;
    public bool IsLeft;
    public string Host;
    public string Dataset;
    public int Quality;
    public float Scale;

    private Texture2D Texture = null;
    private IEnumerator Coroutine = null;
    private IEnumerator[] Coroutines = new IEnumerator[16];

    // Use this for initialization
    void Start()
    {
        Coroutine = DownloadLoop();
        StartCoroutine(Coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DownloadLoop()
    {
        int index = 0;

        while (true)
        {
            if (Coroutines[index] != null)
            {
                StopCoroutine(Coroutines[index]);
            }

            Vector3 camera = UnityEngine.VR.InputTracking.GetLocalPosition(
                IsLeft ? UnityEngine.VR.VRNode.LeftEye : UnityEngine.VR.VRNode.RightEye
            );
            

            Vector3 position = GetTapestryPosition((transform.position - camera)).normalized;
            Vector3 forward = GetTapestryRotation(-(transform.position - Camera.transform.position)).normalized;
            Vector3 up = Vector3.up; // GetTapestryPosition(_camera.transform.up);

            string url = "";
            url += "http://" + host;
            url += "/image";
            url += "/" + Dataset;
            url += "/" + +position.x * Scale;
            url += "/" + +position.y * Scale;
            url += "/" + +position.z * Scale;
            url += "/" + +up.x;
            url += "/" + +up.y;
            url += "/" + +up.z;
            url += "/" + +forward.x;
            url += "/" + +forward.y;
            url += "/" + +forward.z;
            url += "/" + Quality;
            url += "/format,png";

            Coroutines[index] = DownloadTexture(url);
            StartCoroutine(Coroutines[index]);
            yield return new WaitForSeconds(0.05f);

            index = (index + 1) % Coroutines.Length;
        }
    }

    private IEnumerator DownloadTexture(string url)
    {
        Debug.Log("Download " + url);

        WWW www = new WWW(url);
        yield return www;

        if (Texture != null)
        {
            DestroyImmediate(Texture);
        }

        Texture = www.texture;
        Image.texture = Texture;
    }
    

    private Vector3 GetTapestryPosition(Vector3 unityPosition)
    {
        return new Vector3(unityPosition.z, -unityPosition.y, unityPosition.x);
    }

    private Vector3 GetTapestryRotation(Vector3 unityRotation)
    {
        return new Vector3(unityRotation.z, -unityRotation.y, unityRotation.x);
    }
    
}
