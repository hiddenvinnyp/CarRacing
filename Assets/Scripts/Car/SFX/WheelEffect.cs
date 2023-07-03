using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sidewaySlipLimit;

    [SerializeField] private GameObject skidPrefab;

    [SerializeField] private AudioSource audioSkid;
    [SerializeField] private ParticleSystem[] wheelsSmoke;

    private WheelHit wheelHit;
    private Transform[] skidTrail;
    private bool isSlip;

    private void Start()
    {
        skidTrail = new Transform[wheels.Length];
    }

    private void Update()
    {
        isSlip = false;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetGroundHit(out wheelHit);

            if (wheels[i].isGrounded)
            {
                if (Mathf.Abs(wheelHit.forwardSlip) > forwardSlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) > sidewaySlipLimit)
                {
                    if (skidTrail[i] == null)
                        skidTrail[i] = Instantiate(skidPrefab).transform;

                    if (!audioSkid.isPlaying)
                        audioSkid.Play();

                    if (skidTrail[i] != null)
                    {
                        skidTrail[i].position = wheelHit.point + new Vector3(0,0.01f,0); // wheels[i].transform.position - wheelHit.normal * wheels[i].radius; //wheelHit.point;
                        skidTrail[i].forward = -wheelHit.normal;

                        wheelsSmoke[i].transform.position = skidTrail[i].position;
                        wheelsSmoke[i].Play(); //Emit(1)
                    }
                    isSlip = true;
                    continue;
                } 
            }

            skidTrail[i] = null;
            wheelsSmoke[i].Stop();
        }

        if (!isSlip)
            audioSkid.Stop();
    }
}
