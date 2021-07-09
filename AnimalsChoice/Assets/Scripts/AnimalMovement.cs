using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _turnSpeed;
    [SerializeField] float _maxX;

    FloatingJoystick joy;

    float rotationY;
    float positionX;

    void Awake()
    {
        joy = FindObjectOfType<FloatingJoystick>();
    }

    void Update()
    {
        if (!GameManager._instance.gameIsOver)
        {
            transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
            //player control
            var horizontal = joy.Horizontal;
            positionX += horizontal * _turnSpeed * Time.deltaTime;
            positionX = Mathf.Clamp(positionX, -_maxX, _maxX);

            if (positionX < _maxX && positionX > -_maxX)
            {
                transform.localPosition = new Vector3(positionX, transform.position.y, transform.position.z);
            }

            rotationY += horizontal * 150f * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -10, 10);
            if (rotationY < 10 && rotationY > -10)
            {
                transform.localEulerAngles = new Vector3(0, rotationY,0);
            }
            if (horizontal == 0)
            {
                rotationY -= rotationY *4f* Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            GameManager._instance.GameWin();
        }
        if (other.tag == "DeadZone")
        {
            AnimalTransformation._instance.playerAnimator.SetBool("Death", true);
            GameManager._instance.gameIsOver = true;
            Vibration.Vibrate(40);
            GameManager._instance.CharacterDead();
        }
    }
}
