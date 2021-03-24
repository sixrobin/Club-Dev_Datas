namespace ClubDevDatas.XML
{
    using System.Xml.Linq;
    using UnityEngine;

    public class Gun : WeaponBehaviour
    {
        public Gun(XContainer container, Weapon owner) : base(container, owner)
        {
        }

        public override float ShootDuration => 0f;

        public override void Shoot(Transform spawnPoint)
        {
            GameObject bullet = WeaponsDatabase.GetBulletPrefab(InstantiatedAmmoPrefabId);

            GameObject bulletInstance = Object.Instantiate(
                bullet,
                spawnPoint.position,
                bullet.transform.rotation,
                BulletsContainer.ContainerTransform);

            bulletInstance.transform.up = spawnPoint.up;
        }

        public override void Deserialize(XContainer container)
        {
            base.Deserialize(container);

            XElement gunElement = container as XElement;

            XElement bulletPrefabIdElement = gunElement.Element("BulletPrefabId");
            InstantiatedAmmoPrefabId = bulletPrefabIdElement.Value;
        }
    }
}