using UnityEngine;


public class DragonController : MonoBehaviour
{
    #region Dragon Caracteristic

    [SerializeField] private Transform _playerTransfotm;
    [SerializeField] private DragonData _dragonData;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private float _rightValue;
    [SerializeField] private float _leftValue;
    private Animator _animator;

    private void Start()
    {
        _targetTransform = GameObject.FindGameObjectWithTag("Enemies").transform;
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();

        Attack();
    }

    #endregion


    #region Follow

    private void FollowPlayer()
    {
        RaycastHit hitFollow;
        var startPosition = transform.position;
        var directionFollow = _playerTransfotm.position - startPosition;

        if (Physics.Raycast(startPosition, directionFollow, out hitFollow, 4f, _mask))
        {
            transform.position = Vector3.Lerp(transform.position, GerCurrentPlayerPositon(), _lerpSpeed);
        }
    }

    private Vector3 GerCurrentPlayerPositon()
    {
        return _playerTransfotm.position + Vector3.up * 2 + Vector3.forward * 2 + Vector3.right * _rightValue + Vector3.left * _leftValue;
    }

    #endregion


    #region Dragon Attack

    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _particleAttack;// проще создавать GO поэтому сменил тип
    [SerializeField] private Transform _startAttackPosition;
    [SerializeField] private float _rotationSpeed;
    private GameObject _tempSystem;
    private Light _lightAttack;
    private Transform _targetTransform;

    private void Attack()
    {
        //Определяем направление нормализуем вектор, передаем его в LookRotation, обнуляя x z ось для того чтобы враг не заваливался на спину
        Vector3 direction = (_targetTransform.position - transform.position).normalized;
        Quaternion lookRotationRes = Quaternion.LookRotation(direction);
        lookRotationRes.x = 0f;
        lookRotationRes.z = 0f;
        //Используя сферическую интерполяцию поворачиваем врага лицом к игроку
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationRes, Time.deltaTime * _rotationSpeed);

        RaycastHit hit;//Создаем структуру хранения для данных возвращаемых лучом
        if (Physics.Raycast(transform.position, direction, out hit, 10f, _mask))// Касстуем луч через физику, если луч вернул объект
        {
            if (_tempSystem == null)//Проверяем пуста ли ссылка на систему частиц
            {
                //Если пуста - то создаем систему частиц и сохраняем ссылку в _tempSystem
                _tempSystem = Instantiate(_particleAttack, _startAttackPosition.position, Quaternion.identity);
                _tempSystem.transform.parent = transform;//Прикрепляем объект системы частиц как дочерний к врагу, для того чтобы эмиттер не висел в воздухе
                ParticleSystem TempParticle = _tempSystem.GetComponent<ParticleSystem>();//Добавляем ссылку на компонент системы частиц
                _animator.SetTrigger("DragonAttack");
                TempParticle.Play();//Запускаем частицы
                _tempSystem.transform.rotation = Quaternion.LookRotation(direction);// используем простой поворот частиц в камеру игрока
                _lightAttack = _tempSystem.AddComponent<Light>();//Прикрепляем компонент света к объекту с частицами
                _lightAttack.color = Color.red;//Устанавливаем цвет
                Destroy(_tempSystem, 3f); //Уничтожаем систему частиц через 3 секунды, хотя дешевле включать и выключать, здесь для примера
            }
        }
    }

    #endregion
}
