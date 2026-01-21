using UnityEngine;
using Ninez.Board;

namespace Ninez.Stage
{
    public class Stage
    {
        int m_nRow;
        int m_nCol;

        StageBuilder m_StageBuilder;

        public int maxRow { get { return m_nRow; } }
        public int maxCol { get { return m_nCol; }}

        public Stage(StageBuilder stageBuilder, int nRow, int nCol)
        {
            m_StageBuilder = stageBuilder;

            m_Board = new Ninez.Board.Board(nRow, nCol);
        }

        public Block[,] blocks { get { return m_Board.blocks; } }
        public Cell[,] cells { get { return m_Board.cells; } }

        Ninez.Board.Board m_Board;
        public Ninez.Board.Board board { get { return m_Board; } }

        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            m_Board.ComposeStage(cellPrefab, blockPrefab, container);
        }

        public void PrintAll()
        {
            System.Text.StringBuilder strCells =new System.Text.StringBuilder();
            System.Text.StringBuilder strBlocks = new System.Text.StringBuilder();

            for(int nRow = maxRow-1; nRow>=0; nRow--)
            {
                for(int nCol=0; nCol<maxCol; nCol++)
                {
                    strCells.Append($"{cells[nRow, nCol].type}");
                    strBlocks.Append($"{blocks[nRow, nCol].type}");
                }

                strCells.Append('\n');
                strBlocks.Append('\n');
            }

            Debug.Log(strCells.ToString());
            Debug.Log(strBlocks.ToString());
        }


    }

}

