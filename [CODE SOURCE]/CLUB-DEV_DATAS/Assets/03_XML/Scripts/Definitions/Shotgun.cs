namespace ClubDevDatas.XML
{
    using System.Xml.Linq;
    using UnityEngine;

    public class Shotgun : WeaponBehaviour
    {
        public Shotgun(XContainer container, Weapon owner) : base(container, owner)
        {
        }

        public float Angle { get; private set; }

        public float Count { get; private set; }

        public float Delta => Angle / (Count - 1);

        public override float ShootDuration => 0f;

        public override void Shoot(Transform spawnPoint)
        {
            GameObject bullet = WeaponsDatabase.GetBulletPrefab(InstantiatedAmmoPrefabId);

            for (int i = 0; i < Count; ++i)
            {
                GameObject bulletInstance = Object.Instantiate(
                    bullet,
                    spawnPoint.position,
                    bullet.transform.rotation,
                    BulletsContainer.ContainerTransform);

                bulletInstance.transform.up = Quaternion.Euler(0f, 0f, i * Delta - Angle * 0.5f) * spawnPoint.up;
            }
        }

        public override void Deserialize(XContainer container)
        {
            base.Deserialize(container);

            XElement shotgunElement = container as XElement;

            XElement bulletPrefabIdElement = shotgunElement.Element("BulletPrefabId");
            InstantiatedAmmoPrefabId = bulletPrefabIdElement.Value;

            XElement angleElement = shotgunElement.Element("Angle");
            if (!float.TryParse(angleElement.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float angle))
            {
                return;
            }

            Angle = angle;

            XElement countElement = shotgunElement.Element("Count");
            if (!float.TryParse(countElement.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float count))
            {
                return;
            }

            Count = count;
        }
    }
}