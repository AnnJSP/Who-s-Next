using UnityEngine;
using UnityEngine.SceneManagement;


public class ChancheScene : MonoBehaviour
{
    #region ChangeScene

    [SerializeField] private string _nameOfScene;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(_nameOfScene);
        }
    }

    #endregion
}
