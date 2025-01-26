using UnityEngine;

public class CreatChainSpikeBall : MonoBehaviour
{
    [SerializeField] HingeJoint2D hJ;
    [SerializeField] GameObject chain;
    [SerializeField] float distance;
    void Start()
    {
        CreateChain.CreateChain2Point(hJ.connectedAnchor,transform.position,chain,transform,distance);
    }
}
