using UnityEngine;
namespace RoyalMatch.Board
{
    public class Cell
    {
        protected CellType m_CellType;
        public CellType type
        {
            get {  return m_CellType; }
            set { m_CellType = value; }
        }

        //CellBehaviour를 참조하기 위하여 멤버 변수를 선언하고 프로퍼티를 작성
        protected CellBehaviour m_CellBehaviour;
        public CellBehaviour cellBehaviour
        {
            get { return m_CellBehaviour; }
            set
            {
                m_CellBehaviour = value;
                m_CellBehaviour.SetCell(this);
            }
        }

        //생성자, 파라미터로 전달된 타입 정보를 m_CellType에 저장
        public Cell(CellType cellType)
        {
            m_CellType=cellType;
        }

        //파라미터로 전달된 리소스를 이용해서 CellGameObject를 생성하고 Container의 자식으로 둔다.
        public Cell InstantiateCellObj(GameObject cellPrefab, Transform containerObj)
        {
            //cell 오브젝트 생성
            GameObject newObj = Object.Instantiate(cellPrefab, new Vector3(0,0,0) , Quaternion.identity);

            //Container(Board)의 자식으로 Cell을 포함시킨다.
            newObj.transform.parent = containerObj;

            //Cell 오브젝트에 적용된 CellBehaviour 컴포넌트를 보관한다.
            this.cellBehaviour = newObj.transform.GetComponent<CellBehaviour>();

            return this;
        }

        //지정된 위치로 Cell이 참조하는 GameObject의 위치를 변경한다.
        public void Move(float x, float y)
        {
            cellBehaviour.transform.position = new Vector3(x, y);
        }
    }
}

