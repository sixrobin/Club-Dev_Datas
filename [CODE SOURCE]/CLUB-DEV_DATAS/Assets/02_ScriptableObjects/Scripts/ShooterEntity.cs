namespace ClubDevDatas.ScriptableObjects
{
    using UnityEngine;

    public class ShooterEntity : MonoBehaviour
    {
        [SerializeField] protected Transform _bulletSpawn = null;
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
                    yield return _weapons[_currentWeaponIndex].Shoot(_bulletSpawn);

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