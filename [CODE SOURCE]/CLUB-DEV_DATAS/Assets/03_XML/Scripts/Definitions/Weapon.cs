namespace ClubDevDatas.XML
{
    using System.Linq;
    using System.Xml.Linq;

    public class Weapon
    {
        public Weapon(XContainer container)
        {
            Deserialize(container);
        }

        public string Id { get; private set; }

        public float Rate { get; private set; }

        public float ReloadDuration { get; private set; }

        public int MagazineCapacity { get; private set; } = 1;

        public System.Collections.Generic.List<WeaponBehaviour> Behaviours { get; private set; }

        public System.Collections.IEnumerator Shoot(UnityEngine.Transform spawnPoint)
        {
            if (Behaviours == null || Behaviours.Count == 0)
                yield break;

            foreach (WeaponBehaviour behaviour in Behaviours)
                behaviour.Shoot(spawnPoint);

            yield return new UnityEngine.WaitForSeconds(Behaviours.OrderBy(o => o.ShootDuration).Last().ShootDuration);
        }

        public virtual void Deserialize(XContainer container)
        {
            XElement weaponElement = container as XElement;

            XAttribute idAttribute = weaponElement.Attribute("Id");
            Id = idAttribute.Value;

            XElement rateElement = weaponElement.Element("Rate");
            if (!float.TryParse(rateElement.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float rate))
            {
                return;
            }

            Rate = rate;

            XElement reloadDurationElement = weaponElement.Element("ReloadDuration");
            if (!float.TryParse(reloadDurationElement.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float reloadDuration))
            {
                return;
            }

            ReloadDuration = reloadDuration;

            XElement magazineCapacityElement = weaponElement.Element("MagazineCapacity");
            if (magazineCapacityElement != null)
            {
                if (!int.TryParse(magazineCapacityElement.Value, out int magazineCapacity))
                {
                    return;
                }

                MagazineCapacity = magazineCapacity;
            }

            Behaviours = new System.Collections.Generic.List<WeaponBehaviour>();

            foreach (XElement behaviour in weaponElement.Element("Behaviour").Elements())
            {
                switch (behaviour.Name.LocalName)
                {
                    case "Laser":
                        Behaviours.Add(new Laser(behaviour, this));
                        break;

                    case "Shotgun":
                        Behaviours.Add(new Shotgun(behaviour, this));
                        break;

                    case "Gun":
                        Behaviours.Add(new Gun(behaviour, this));
                        break;
                }
            }
        }
    }
}