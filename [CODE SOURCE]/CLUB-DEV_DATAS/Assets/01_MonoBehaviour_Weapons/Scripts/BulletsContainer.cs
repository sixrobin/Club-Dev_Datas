namespace ClubDevDatas.MonoBehaviourWeapons
{
    using UnityEngine;

    public class BulletsContainer : MonoBehaviour
    {
        public static BulletsContainer Instance;

        public static Transform ContainerTransform => Instance.transform;

        private void Awake()
        {
            Instance = this;
        }
    }
}