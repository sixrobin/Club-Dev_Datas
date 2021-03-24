namespace ClubDevDatas.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Shotgun", menuName = "ClubDev/Shotgun")]
    public class Shotgun : Weapon
    {
        [SerializeField, Range(0f, 90f)] private float _angle = 25f;
        [SerializeField, Min(1)] private int _count = 4;

        public float Angle => _angle;
        public float Count => _count;
        public float Delta => Angle / (Count - 1);

        public override System.Collections.IEnumerator Shoot(Transform spawnPoint)
        {
            for (int i = 0; i < Count; ++i)
            {
                GameObject bulletInstance = Instantiate(
                    _bulletPrefab,
                    spawnPoint.position,
                    _bulletPrefab.transform.rotation,
                    BulletsContainer.ContainerTransform);

                bulletInstance.transform.up = Quaternion.Euler(0f, 0f, i * Delta - Angle * 0.5f) * spawnPoint.up;
            }

            yield break;
        }
    }
}