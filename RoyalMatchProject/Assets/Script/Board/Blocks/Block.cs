using UnityEngine;

namespace RoyalMatch.Board
{
    public class Block
    {

        BlockType m_BlockType;
        public BlockType type
        {
            get { return m_BlockType; }
            set { m_BlockType = value; }
        }

        protected BlockBehaviour m_BlockBehaviour;
        public BlockBehaviour blockBehaviour
        {
            get { return m_BlockBehaviour; }
            set 
            {
                m_BlockBehaviour = value;
                m_BlockBehaviour.SetBlock(this);
            }
        }
        protected BlockBreed m_Breed;   //렌더링되는 블럭 캐린터(즉, 이미지 종류)
        public BlockBreed breed
        {
            get { return m_Breed; }
            set
            {
                m_Breed = value;
                m_BlockBehaviour?.UpdateView(true);
            }
        }

        //생성자, 파라미터로 전달된 타입 정보를 m_BlockType에 저장
        public Block(BlockType blockType)
        {
            m_BlockType = blockType;
        }

        public Block InstantiateBlockObj(GameObject blockPrefab, Transform containerObj)
        {
            //유효하지 않은 블럭인 경우, Block GameObject를 생성하지 않는다.
            if (IsValidate() == false)
                return null;

            //1. Block 오브젝트를 생성한다.
            GameObject newObj = Object.Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //2. 컨테이너(Board)의 차일드로 Block을 포함시킨다.
            newObj.transform.parent = containerObj;

            //3. Block 오브젝트에 적용된 BlockBehaviour 컴포너트를 보관한다.
            this.blockBehaviour = newObj.transform.GetComponent<BlockBehaviour>();



            return this;
        }

        public void Move(float x, float y)
        {
            blockBehaviour.transform.position = new Vector3(x, y);
        }

        public bool IsValidate()
        {
            return type != BlockType.EMPTY;
        }
    }
}

