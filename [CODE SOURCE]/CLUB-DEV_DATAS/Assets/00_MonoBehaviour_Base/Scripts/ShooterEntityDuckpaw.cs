namespace ClubDevDatas.MonoBehaviourBase
{
    using UnityEngine;

    public class ShooterEntityDuckpaw : ShooterEntity
    {
        [SerializeField, Range(0f, 90f)] private float _angle = 25f;
        [SerializeField, Min(1)] private int _count = 4;

        protected override void Shoot()
        {
            float delta = _angle / (_count - 1);

            for (int i = 0; i < _count; ++i)
            {
                GameObject bulletInstance = Instantiate(
                    _bulletPrefab,
                    _bulletSpawn.position,
                    _bulletPrefab.transform.rotation,
                    BulletsContainer.ContainerTransform);

                bulletInstance.transform.up = Quaternion.Euler(0f, 0f, i * delta - _angle * 0.5f) * _bulletSpawn.transform.up;
            }
        }
    }
}