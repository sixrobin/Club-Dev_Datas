namespace ClubDevDatas.XML
{
    using UnityEngine;

    public class ShooterEntity : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawn = null;
        [SerializeField] private string[] _weaponIds = null;

        private Weapon _currentWeapon;
        private int _currentWeaponIndex;

        public void SelectNextWeapon()
        {
            _currentWeaponIndex = ++_currentWeaponIndex % _weaponIds.Length;
            _currentWeapon = WeaponsDatabase.Weapons[_weaponIds[_currentWeaponIndex]];
        }

        public void SelectPreviousWeapon()
        {
            _currentWeaponIndex = (--_currentWeaponIndex % _weaponIds.Length + _weaponIds.Length) % _weaponIds.Length;
            _currentWeapon = WeaponsDatabase.Weapons[_weaponIds[_currentWeaponIndex]];
        }

        private void Start()
        {
            _currentWeaponIndex = 0;
            _currentWeapon = WeaponsDatabase.Weapons[_weaponIds[_currentWeaponIndex]];
            StartCoroutine(ShootCoroutine());
        }

        private System.Collections.IEnumerator ShootCoroutine()
        {
            float timer = 0f;
            int shootCounter = 0;

            while (true)
            {
                timer += Time.deltaTime;

                if (timer > _currentWeapon.Rate)
                {
                    yield return _currentWeapon.Shoot(_bulletSpawn);

                    shootCounter++;
                    timer = 0f;

                    if (shootCounter == _currentWeapon.MagazineCapacity)
                    {
                        yield return new WaitForSeconds(_currentWeapon.ReloadDuration);
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