using UnityEngine;

namespace RoyalMatch.Board
{
    public class Board
    {
        //보드의 크기(행,열) 정보를 저장하는 멤버 및 속성을 선언하고 정의한다.
        int m_nRow;
        int m_nCol;

        public int maxRow {  get { return m_nRow; } }
        public int maxCol { get { return m_nCol; } }


        //보드를 구성하는 Cell을 저장하는 2차원 배열을 선언한다.
        Cell[,] m_Cells;
        public Cell[,] cells { get { return m_Cells; } }

        //보드를 구성하는 Block을 저장하는 2차원 배열을 선언한다.
        Block[,] m_Blocks;
        public Block[,] blocks { get { return m_Blocks; } }

        Transform m_Container;
        GameObject m_CellPrefab;
        GameObject m_BlockPrefab;


        //생성자. 보드 크기 정보를 저장하고, 보드 크기만큼을 저장할 수 있는 Cell과 Block 배열을 생성한다.
        public Board(int nRow,int nCol)
        {
            m_nRow = nRow;
            m_nCol = nCol;

            m_Cells = new Cell[nRow,nCol];
            m_Blocks = new Block[nRow,nCol];
        }


        //주어진 리소스를 참조하여 보드 구성
        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            //스테이지 구성에 필요한 Cell, Block, Container 정보를 저장
            m_CellPrefab = cellPrefab;
            m_BlockPrefab = blockPrefab;
            m_Container = container;

            
            //Cell, Block Prefab을 이용해서 Board에 Cell/Block GameObject를 추가
            //row=0, col=0 에 해당되는 화면 position을 구한다.
            float initX = CalcInitX(0.5f);
            float initY = CalcInitY(0.5f);
           

            //모든 cell/block을 처리하기 위한 loop
            for (int nRow = 0; nRow < m_nRow; nRow++)
            {
                for (int nCol = 0;nCol < m_nCol; nCol++)
                {
                    //해당 row, col에 위치한 Cell객체에게 CellGameObject를 생성 하도록 요청
                    Cell cell = m_Cells[nRow, nCol]?.InstantiateCellObj(cellPrefab, container);
                    //생성된 Cell 객체에게 Cell GameObject의 초기 위치 설정
                    cell?.Move(initX + nCol, initY + nRow);

                    Block block = m_Blocks[nRow, nCol]?.InstantiateBlockObj(blockPrefab, container);
                    block?.Move(initX + nCol, initY + nRow);
                }
            }
            
        }


        //row=0, col=0일때 해당되는 화면 위치 Xposition을 구한다. 9*9 보드일때 -4가 리턴된다. 
        public float CalcInitX(float offset = 0)
        {
            return -m_nCol / 2.0f + offset;
        }
        //row=0, col=0일때 해당되는 화면 위치 Yposition을 구한다. 9*9 보드일때 -4가 리턴된다. 
        public float CalcInitY(float offset = 0)
        {
            return -m_nRow / 2.0f + offset;
        }

    }
}

