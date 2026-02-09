using RoyalMatch.Scriptable;
using UnityEngine;

namespace RoyalMatch.Board
{
    public class BlockBehaviour : MonoBehaviour
    {
        Block m_Block;
        SpriteRenderer m_SpriteRenderer;
        [SerializeField] BlockConfig m_BlockConfig;

        void Start()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            UpdateView(false);
        }

        internal void SetBlock(Block block)
        {
            m_Block = block;
        }

        public void UpdateView(bool bValueChanged)
        {
            if (m_Block.type == BlockType.EMPTY)
            {
                m_SpriteRenderer.sprite = null;
            }
            else if(m_Block.type == BlockType.BASIC)
            {
                m_SpriteRenderer.sprite = m_BlockConfig.basicBlockSprites[(int)m_Block.breed];

            }

        }

    }
}


