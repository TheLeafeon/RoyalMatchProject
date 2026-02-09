using UnityEngine;

namespace RoyalMatch.Board
{
    public class CellBehaviour : MonoBehaviour
    {
        //참조하는 Cell 객체 선언
        Cell m_Cell;
        SpriteRenderer m_SpriteRenderer;

        private void Start()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            //Cell 종류에 해당되는 Sprite가 출력되도록 UpdateView(false) 호출
            UpdateView(false);
        }

        //Cell 객체 참조를 저장
        public void SetCell(Cell cell)
        {
            m_Cell = cell;
        }

        // Cell종류에 해당되는 Sprite를 SpriteRenderer에 저장
        public void UpdateView(bool bValueChnaged)
        {
             if(m_Cell.type == CellType.EMPTY)
            {
                m_SpriteRenderer.sprite = null;
            }
        }
    }
}

