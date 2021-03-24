namespace ClubDevDatas.MonoBehaviourBase
{
    using UnityEngine;

    public class ShooterEntity : MonoBehaviour
    {
        [SerializeField] protected Transform _bulletSpawn = null;
        [SerializeField] protected GameObject _bulletPrefab = null;

        [SerializeField, Min(0.01f)] private float _rate = 0.2f;
        [SerializeField, Min(1)] private int _magazineCapacity = 10;
        [SerializeField, Min(0f)] private float _reloadDuration = 1f;

        protected virtual void Shoot()
        {
            GameObject bulletInstance = Instantiate(
                _bulletPrefab,
                _bulletSpawn.position,
                _bulletPrefab.transform.rotation,
                BulletsContainer.ContainerTransform);

            bulletInstance.transform.up = _bulletSpawn.up;
        }

        private void Start()
        {
            StartCoroutine(ShootCoroutine());
        }

        private System.Collections.IEnumerator ShootCoroutine()
        {
            float timer = 0f;
            int shootCounter = 0;

            while (true)
            {
                timer += Time.deltaTime;

                if (timer > _rate)
                {
                    Shoot();
                    shootCounter++;
                    timer = 0f;

                    if (shootCounter == _magazineCapacity)
                    {
                        yield return new WaitForSeconds(_reloadDuration);
                        shootCounter = 0;
                    }
                }

                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_bulletSpawn.position, 0.2f);
            Gizmos.DrawLine(_bulletSpawn.position, _bulletSpawn.position + _bulletSpawn.up);
        }
    }
}