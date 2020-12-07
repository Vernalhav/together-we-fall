using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.4f;
    public float continousShakeAmount = 0.004f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	void OnEnable()
	{
		originalPos = transform.position;
	}


    public IEnumerator Shake()
    {   
        float time = 0;
        
        while(time < shakeDuration){
            time += Time.deltaTime * 10;
            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            yield return null;
        }

        Vector3 antes = transform.position;
        transform.position = originalPos;
    }
}