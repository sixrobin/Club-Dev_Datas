namespace ClubDevDatas.MonoBehaviourWeapons
{
    using UnityEngine;

    [System.Serializable]
    public class Weapon
    {
        [SerializeField, Min(0.01f)] private float _rate = 0.2f;
        [SerializeField, Min(1)] private int _magazineCapacity = 10;
        [SerializeField, Min(0f)] private float _reloadDuration = 1f;
        [SerializeField, Range(0f, 90f)] private float _angle = 25f;
        [SerializeField, Min(1)] private int _count = 4;

        public float Rate => _rate;
        public float MagazineCapacity => _magazineCapacity;
        public float ReloadDuration => _reloadDuration;
        public float Angle => _angle;
        public float Count => _count;
    }
}