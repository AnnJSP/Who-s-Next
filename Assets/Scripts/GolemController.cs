using UnityEngine;


public class GolemController : MonoBehaviour
{
    #region Golem Controller

    [SerializeField] private Transform _player;
    [SerializeField] private float _healthPoint;
    [SerializeField] private float _powerOfFirstAttack;
    [SerializeField] private float _powerOfSecondAttack;
    [SerializeField] private float _golemSpeed;
    [SerializeField] private LayerMask _mask;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        var startPosition = transform.position;
        var direction = _player.position - startPosition;

        if (Physics.Raycast(startPosition, direction, out hit, 10f, _mask))
        {
            Vector3 relativePos = _player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;

            //_rigidbody.AddForce(transform.forward * _golemSpeed, ForceMode.Force);
            transform.position += transform.forward * _golemSpeed * Time.fixedDeltaTime;

            _animator.SetFloat("Speed", 1f);
        }
        else _animator.SetFloat("Speed", 0f);

        RaycastHit attack;
        float count = 0f;

        if (Physics.Raycast(startPosition, direction, out attack, 1f, _mask))
        {
            if (count < 3f)
            {
                _animator.SetFloat("Attack", -1f);
                count++;

                //PlayerController attack01 = collision.gameObject.GetComponent<PlayerController>();
                //attack01.Hurt(_powerOfFirstAttack);
            }

            if (count == 3f)
            {
                _animator.SetFloat("Attack", 1);
                count = 0f;

                //PlayerController attack02 = collision.gameObject.GetComponent<PlayerController>();
                //attack02.Hurt(_powerOfSecondAttack);
            }
        }
    }

    #endregion


    #region CharacteristicsGolem

    public void Hurt(float Damage)
    {
        _healthPoint -= Damage;

        _animator.SetFloat("Attack", -1f);
        _animator.SetFloat("Speed", -1f);

        if (_healthPoint <= 0f)
        {
            _animator.SetTrigger("Loss");
            Destroy(gameObject, 3f);
        }
    }

    public float HealthPoint
    {
        get
        {
            return _healthPoint;
        }
    }

    #endregion
}
