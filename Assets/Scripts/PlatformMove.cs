using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SliderJoint2D))]
public class PlatformMove : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private SliderJoint2D _sliderJoint;

    private void Start()
    {
        _sliderJoint = GetComponent<SliderJoint2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layerMask.Contains(collision.gameObject.layer))
        {
            JointMotor2D motor = _sliderJoint.motor;
            motor.motorSpeed = -motor.motorSpeed;
            _sliderJoint.motor = motor;
        }
    }
}
