using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private float _heaithPoint;
    private float _startHealthPoint;

    private void Start()
    {
        _startHealthPoint = _heaithPoint;
    }

    private void Update()
    {
        SetHealthBarValue(_heaithPoint);
    }

    //#region Attack

    //[SerializeField] private float _powerOfAttack;

    //private void Attack(float _powerOfAttack)
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        GolemController attack = gameObject.GetComponent<GolemController>();
    //        attack.Hurt(_powerOfAttack);
    //    }
    //}

    //#endregion


    #region CharacteristicsPlayer

    public void Hurt(float Damage)
    {
        _heaithPoint -= Damage;

        if (_heaithPoint <= 0)
        {
            //SceneManager.LoadSceneAsync("Die");
        }
    }

    public void Treatment(float Helth)
    {
        _heaithPoint += Helth;

        if (_heaithPoint > 100)
        {
            _heaithPoint = 100;
        }
    }

    #endregion


    #region HP Bar

    private void SetHealthBarValue(float HealthPoint)
    {
        _healthBarImage.fillAmount = HealthPoint;
        if (_healthBarImage.fillAmount < 0.15f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (_healthBarImage.fillAmount < 0.5f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    private void SetHealthBarColor(Color healthColor)
    {
        _healthBarImage.color = healthColor;
    }

    #endregion
}
