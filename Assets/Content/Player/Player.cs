using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVec = moveVec * Time.deltaTime * moveSpeed;
        
        controller.Move(moveVec);
    }
}
