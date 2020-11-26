using UnityEngine;


[CreateAssetMenu(fileName = "New DragonData", menuName = "Dragon Data", order = 51)]
public class DragonData : ScriptableObject
{
    #region Dragon Data

    [SerializeField] private float _powerOfAttack;
    [SerializeField] private float _powerOfTreatment;
    [SerializeField] private float _powerOfProtect;

    public float PowerOfAttack
    {
        get
        {
            return _powerOfAttack;
        }
    }

    public float PowerOfTreatment
    {
        get
        {
            return _powerOfTreatment;
        }
    }

    public float PowerOfProtect
    {
        get
        {
            return _powerOfProtect;
        }
    }

    #endregion
}
