namespace ClubDevDatas.ScriptableObjects
{
    using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _speed = 10f;
        [SerializeField] private float _lifetime = 5f;

        private void Start()
        {
            Destroy(gameObject, _lifetime);
        }

        private void Update()
        {
            transform.Translate(transform.up * Time.deltaTime * _speed);
        }
    }
}