using RoyalMatch.Board;
using UnityEngine;

namespace RoyalMatch.Stage
{
    public class Stage
    {
        int m_nRow;
        int m_nCol;

        public int maxRow { get { return m_nRow; } }
        public int maxCol { get { return m_nCol; } }


        
        RoyalMatch.Board.Board m_Board;
        public RoyalMatch.Board.Board board { get { return m_Board; } }

        StageBuilder m_StageBuilder;
        public Block[,] blocks { get { return m_Board.blocks; } }
        public Cell[,] cells {get {return m_Board.cells;} } 
        

        //생성자, 주어진 크기를 갖는 Board를 생성
        public Stage(StageBuilder stageBuilder, int nRow, int nCol)
        {
            m_StageBuilder = stageBuilder;

            m_Board = new RoyalMatch.Board.Board(nRow, nCol);
        }



        //Board를 구성하는 Cell과 Block 정보를 확인할 수 있는 디버깅 코드
        public void PrintAll()
        {
            System.Text.StringBuilder strCells = new System.Text.StringBuilder();
            System.Text.StringBuilder strBlocks = new System.Text.StringBuilder();

            for (int nRow = maxRow - 1; nRow >= 0; nRow--)
            {
                for (int nCol = 0; nCol < maxRow; nCol++)
                {
                    strCells.Append($"{cells[nRow, nCol].type},");
                    strBlocks.Append($"{blocks[nRow, nCol].type},");
                }

                strCells.Append('\n');
                strBlocks.Append('\n');
            }

            Debug.Log(strCells.ToString());
            Debug.Log(strBlocks.ToString());
        }

        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            m_Board.ComposeStage(cellPrefab, blockPrefab, container);
        }

    }
}

