using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float DumpingSpeed;
    public Vector3 FrameTurn;
    public GameObject Player;
    public GameObject DirectionPoint;
    public float DumpingForce;
    public float ShakeTime;
    public float Noize;
    public float ShakeMagnitude;

    
    private Vector3 _directionPointPosition;
    private Vector3 _playerPosition;
    private Vector3 _playerLastFramePosition;
    private Vector3 _dumpingDirection;
    private Vector3 _dumpingTarget;

    private Quaternion _playerRotation;
    private Quaternion _playerLastFrameRotation;
    private float _positionZ;
    private bool isShake;
    void Start()
    {
        _directionPointPosition = DirectionPoint.transform.position;
        _positionZ = transform.position.z;
        _playerLastFrameRotation = Player.transform.localRotation;
        _dumpingTarget = _playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
   
        _playerRotation = Player.transform.localRotation;
      

        if (!isShake)
        {
            _directionPointPosition = DirectionPoint.transform.position;
            _playerPosition = Player.transform.position;
            _dumpingTarget = new Vector3(_playerPosition.x + DumpingForce * GetDirection().x, _playerPosition.y + DumpingForce * GetDirection().y, _positionZ);
            Dumping();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isShake = true;
            StartCoroutine(ShakeCameraCor(ShakeTime, ShakeMagnitude, Noize));
        }
        _playerLastFrameRotation = Player.transform.localRotation;
    }
    private bool WasMove()
    {
        Quaternion differenceOfRotation = new Quaternion(Mathf.Abs(_playerRotation.x - _playerLastFrameRotation.x),
           Mathf.Abs(_playerRotation.y - _playerLastFrameRotation.y),
           Mathf.Abs(_playerRotation.z - _playerLastFrameRotation.z),1);
        Vector3 differenceOfPosition = _playerPosition - _playerLastFramePosition;
        return (differenceOfRotation.x + differenceOfRotation.y + differenceOfRotation.z > 0 || Mathf.Abs(differenceOfPosition.x) + Mathf.Abs(differenceOfPosition.y) + Mathf.Abs(differenceOfPosition.z)>0);
    }
    private void Dumping()
    {
        transform.position = Vector3.Lerp(transform.position, _dumpingTarget, DumpingSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, _positionZ);
    }
    private Vector3 GetDirection()
    {
        var heading =    _directionPointPosition - _playerPosition;
        return heading / heading.magnitude; 
    }

    private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
    {
        isShake = true;
        //Инициализируем счётчиков прошедшего времени
        float elapsed = 0f;
        //Сохраняем стартовую локальную позицию
        Vector3 startPosition = transform.localPosition;
        //Генерируем две точки на "текстуре" шума Перлина
        Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
        Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

        while (elapsed < duration)
        {
          
            Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
            Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
            Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
            cameraPostionDelta *= magnitude;

            transform.localPosition = startPosition + (Vector3)cameraPostionDelta;
            if (WasMove())
            {
                Dumping();

            }
           // startPosition = _dumpingTarget;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Vector3.Lerp(transform.position, _dumpingTarget, DumpingSpeed * Time.deltaTime); ;
        isShake = false;
    }
}
