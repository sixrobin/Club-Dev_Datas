namespace ClubDevDatas.MonoBehaviourWeapons
{
    using UnityEngine;

    public class ShooterEntity : MonoBehaviour
    {
        [SerializeField] protected Transform _bulletSpawn = null;
        [SerializeField] protected GameObject _bulletPrefab = null;

        [SerializeField] protected Weapon[] _weapons = null;

        protected int _currentWeaponIndex;

        public void SelectNextWeapon()
        {
            _currentWeaponIndex = ++_currentWeaponIndex % _weapons.Length;
        }

        public void SelectPreviousWeapon()
        {
            _currentWeaponIndex = (--_currentWeaponIndex % _weapons.Length + _weapons.Length) % _weapons.Length;
        }

        private void Shoot()
        {
            if (_weapons[_currentWeaponIndex].Count > 1)
            {
                float delta = _weapons[_currentWeaponIndex].Angle / (_weapons[_currentWeaponIndex].Count - 1);

                for (int i = 0; i < _weapons[_currentWeaponIndex].Count; ++i)
                {
                    GameObject bulletInstance = Instantiate(
                        _bulletPrefab,
                        _bulletSpawn.position,
                        _bulletPrefab.transform.rotation,
                        BulletsContainer.ContainerTransform);

                    bulletInstance.transform.up = Quaternion.Euler(0f, 0f, i * delta - _weapons[_currentWeaponIndex].Angle * 0.5f) * _bulletSpawn.transform.up;
                }
            }
            else
            {
                GameObject bulletInstance = Instantiate(
                    _bulletPrefab,
                    _bulletSpawn.position,
                    _bulletPrefab.transform.rotation,
                    BulletsContainer.ContainerTransform);

                bulletInstance.transform.up = _bulletSpawn.up;
            }
        }

        private void Start()
        {
            _currentWeaponIndex = 0;
            StartCoroutine(ShootCoroutine());
        }

        private System.Collections.IEnumerator ShootCoroutine()
        {
            float timer = 0f;
            int shootCounter = 0;

            while (true)
            {
                timer += Time.deltaTime;

                if (timer > _weapons[_currentWeaponIndex].Rate)
                {
                    Shoot();
                    shootCounter++;
                    timer = 0f;

                    if (shootCounter == _weapons[_currentWeaponIndex].MagazineCapacity)
                    {
                        yield return new WaitForSeconds(_weapons[_currentWeaponIndex].ReloadDuration);
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