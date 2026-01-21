using UnityEngine;

namespace Ninez.Board
{
    public class CellBehaviour : MonoBehaviour
    {
        Cell m_Cell;
        SpriteRenderer m_SpriteRenderer;

        private void Start()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            UpdateView(false);
        }

        public void SetCell(Cell cell)
        {
            m_Cell = cell;
        }

        public void UpdateView(bool bValueChanged)
        {
            if(m_Cell.type == CellType.EMPTY)
            {
                m_SpriteRenderer.sprite = null; 
            }
        }
    }
}

