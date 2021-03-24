namespace ClubDevDatas.XML
{
    using System.Xml.Linq;
    using UnityEngine;

    public class Laser : WeaponBehaviour
    {
        public Laser(XContainer container, Weapon owner) : base(container, owner)
        {
        }

        public float Duration { get; private set; }

        public override float ShootDuration => Duration;

        public override void Shoot(Transform spawnPoint)
        {
            GameObject bullet = WeaponsDatabase.GetBulletPrefab(InstantiatedAmmoPrefabId);

            GameObject laserInstance = Object.Instantiate(
                bullet,
                spawnPoint.position,
                bullet.transform.rotation,
                BulletsContainer.ContainerTransform);

            Object.Destroy(laserInstance, Duration);
        }

        public override void Deserialize(XContainer container)
        {
            base.Deserialize(container);

            XElement laserElement = container as XElement;

            XElement bulletPrefabIdElement = laserElement.Element("LaserPrefabId");
            InstantiatedAmmoPrefabId = bulletPrefabIdElement.Value;

            XElement durationElement = laserElement.Element("Duration");
            if (!float.TryParse(durationElement.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float duration))
            {
                return;
            }

            Duration = duration;
        }
    }
}