namespace ClubDevDatas.XML
{
    using System.Xml.Linq;

    public abstract class WeaponBehaviour
    {
        public WeaponBehaviour(XContainer container, Weapon owner)
        {
            Owner = owner;
            Deserialize(container);
        }

        public Weapon Owner { get; private set; }

        public string InstantiatedAmmoPrefabId { get; protected set; }

        public abstract float ShootDuration { get; }

        public abstract void Shoot(UnityEngine.Transform spawnPoint);

        public virtual void Deserialize(XContainer container)
        {
        }
    }
}