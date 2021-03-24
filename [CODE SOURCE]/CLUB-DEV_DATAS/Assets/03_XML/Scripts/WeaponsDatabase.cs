namespace ClubDevDatas.XML
{
    using System.Linq;
    using System.Xml.Linq;
    using UnityEngine;

    public class WeaponsDatabase : MonoBehaviour
    {
        [System.Serializable]
        public class BulletPrefabs
        {
            [SerializeField] private string _id = string.Empty;
            [SerializeField] private GameObject _prefab = null;

            public string Id => _id;
            public GameObject Prefab => _prefab;
        }

        [SerializeField] private TextAsset _weaponDefinitions = null;
        [SerializeField] private BulletPrefabs[] _bulletPrefabs = null;

        public static WeaponsDatabase Instance;

        public static System.Collections.Generic.Dictionary<string, Weapon> Weapons { get; private set; }

        public static GameObject GetBulletPrefab(string id)
        {
            return Instance._bulletPrefabs.First(o => o.Id == id).Prefab;
        }

        public void Deserialize()
        {
            Weapons = new System.Collections.Generic.Dictionary<string, Weapon>();

            XDocument weaponsDocument = XDocument.Parse(_weaponDefinitions.text, LoadOptions.SetBaseUri);

            XElement weaponDefinitionsElement = weaponsDocument.Element("WeaponDefinitions");
            foreach(XElement weaponElement in weaponDefinitionsElement.Elements("WeaponDefinition"))
            {
                Weapon weapon = new Weapon(weaponElement);
                Weapons.Add(weapon.Id, weapon);
            }

            Debug.Log($"Deserialized {Weapons.Count} weapons.");
        }

        private void Awake()
        {
            Instance = this;
            Deserialize();
        }
    }
}