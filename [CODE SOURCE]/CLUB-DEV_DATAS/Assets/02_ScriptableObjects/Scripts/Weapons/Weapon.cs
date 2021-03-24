namespace ClubDevDatas.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Weapon", menuName = "ClubDev/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] protected GameObject _bulletPrefab = null;

        [SerializeField, Min(0.01f)] private float _rate = 0.2f;
        [SerializeField, Min(1)] private int _magazineCapacity = 10;
        [SerializeField, Min(0f)] private float _reloadDuration = 1f;

        public float Rate => _rate;
        public float MagazineCapacity => _magazineCapacity;
        public float ReloadDuration => _reloadDuration;

        public virtual System.Collections.IEnumerator Shoot(Transform spawnPoint)
        {
            GameObject bulletInstance = Instantiate(
                _bulletPrefab,
                spawnPoint.position,
                _bulletPrefab.transform.rotation,
                BulletsContainer.ContainerTransform);

            bulletInstance.transform.up = spawnPoint.up;
            yield break;
        }
    }
}