using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    [RequireComponent(typeof(WeaponController))]
    public class WeaponFuelCellHandler : MonoBehaviour
    {
        [Tooltip("Retract All Fuel Cells Simultaneously")]
        public bool SimultaneousFuelCellsUsage = false;

        [Tooltip("List of GameObjects representing the fuel cells on the weapon")]
        public GameObject[] FuelCells;

        [Tooltip("Cell local position when used")]
        public Vector3 FuelCellUsedPosition;

        [Tooltip("Cell local position before use")]
        public Vector3 FuelCellUnusedPosition = new Vector3(0f, -0.1f, 0f);

        WeaponController m_Weapon;
        bool[] m_FuelCellsCooled;

        void Start()
        {
        }

        void Update()
        {

        }
    }
}