namespace ClubDevDatas.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Laser", menuName = "ClubDev/Laser")]
    public class Laser : Weapon
    {
        [SerializeField] private float _laserDur = 0.1f;

        public override System.Collections.IEnumerator Shoot(Transform spawnPoint)
        {
            GameObject bulletInstance = Instantiate(
                _bulletPrefab,
                spawnPoint.position,
                _bulletPrefab.transform.rotation,
                BulletsContainer.ContainerTransform);

            yield return new WaitForSeconds(_laserDur);

            Destroy(bulletInstance);
        }
    }
}