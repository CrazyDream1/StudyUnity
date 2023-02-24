using Sirenix.OdinInspector;
using UnityEngine;

namespace NavMeshTest.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [TabGroup("Common")][SerializeField] protected int _prise;
    }
}