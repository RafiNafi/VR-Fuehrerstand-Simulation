using UnityEngine;

public class RoomScaleMovingFix : MonoBehaviour
{
    public CharacterController Character;

    void Start()
    {
        Character = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Character.Move(new Vector3(0.001f, -0.001f, 0.001f));
        Character.Move(new Vector3(-0.001f, 0.001f, -0.001f));
    }

}