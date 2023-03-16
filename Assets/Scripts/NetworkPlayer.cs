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

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        var rig = FindObjectOfType<XROrigin>();
        _headRig = rig.transform.Find("Camera Offset/Main Camera");
        _leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        _rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            head.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);

            MapPosition(head, _headRig);
            MapPosition(leftHand, _leftHandRig);
            MapPosition(rightHand, _rightHandRig);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}