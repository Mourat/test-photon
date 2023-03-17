using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    private PhotonView _photonView;
    private Transform _headRig;
    private Transform _leftHandRig;
    private Transform _rightHandRig;
    [SerializeField] private Animator _leftHandAnimator;
    [SerializeField] private Animator _rightHandAnimator;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        var rig = FindObjectOfType<XROrigin>();
        _headRig = rig.transform.Find("Camera Offset/Main Camera");
        _leftHandRig = rig.transform.Find("LeftHand Controller");
        _rightHandRig = rig.transform.Find("RightHand Controller");

        if(_photonView.IsMine)
            foreach (var elem in GetComponentsInChildren<Renderer>())
            {
                elem.enabled = false;
            }
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            // head.gameObject.SetActive(false);
            // leftHand.gameObject.SetActive(false);
            // rightHand.gameObject.SetActive(false);

            MapPosition(head, _headRig);
            MapPosition(leftHand, _leftHandRig);
            MapPosition(rightHand, _rightHandRig);
            
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), _leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), _rightHandAnimator);
        }
    }
    
    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}