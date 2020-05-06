using UnityEngine;
using UnityEngine.UIElements;

namespace TicToc.Controls
{
    public class LookAtPoint : MonoBehaviour
    {
        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(new Vector3(mousePosition.x, mousePosition.y, transform.position.z) - transform.position);
        }
    }
}