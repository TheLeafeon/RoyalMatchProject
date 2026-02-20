using UnityEngine;
using System.Collections;
using RoyalMatch.Util;

namespace RoyalMatch.Stage
{
    //코루틴을 이용한 플레이 액션을 총괄하는 클래스
    //ActionManage는 액션의 흐름을 관리하고 실제 액션은 액션의 대상에게 위임
    //블럭을 이동하는 액션이 필요한 경우에 블럭 레퍼런스를 직접 구해서 액션을 실행하지 않고, 블럭을 관리하고 있는 대상에게 액션을 요청하는 식
    //블럭이라는 존재가 있는지 조차 알지 못한다.  단지 액션이 필요하다 요청을 받으면 그 대상에게 필요한 액션을 수행하라고 요청할 뿐이다.
    //요청한 액션이 종료 되었는지 확인한다.
    public class ActionManager
    {
        Transform m_Container; // Board GameObject
        Stage m_Stage;
        MonoBehaviour m_MonoBehaviour; //코루틴 호출 시 필요한 monobehaviour
        bool m_bRunning; //액션 실행상태 확인용 bool 액션중이라면 true

        public ActionManager(Transform container, Stage stage)
        {
            m_Container = container;
            m_Stage = stage;

            m_MonoBehaviour = container.gameObject.GetComponent<MonoBehaviour>();
        }

        //코루틴을 수행하는 StartCoroutine()의 Wrapper 메소드.
        //
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return m_MonoBehaviour.StartCoroutine(routine);
        }

        //스와이프 액션 수행을 요청 받는 메소드
        public void DoSwipeAction(int nRow, int nCol, Swipe swipeDir)
        {
            //스와이프 클릭한게 실제로 스와이프 가능한 블럭이라면, 
            Debug.Assert(nRow >=0 && nRow < m_Stage.maxRow && nCol >=0 && nCol < m_Stage.maxCol);

            //조건 충족, 스와이프 가능하다면
            if(m_Stage.IsValideSwipe(nRow, nCol, swipeDir))
            {
                StartCoroutine(CoDoSwipeAction(nRow, nCol, swipeDir));
            }
        }

        //스와이프 액션을 수행하는 코루틴, Stage 객체에게 스와이프 액션을 위임
        IEnumerator CoDoSwipeAction(int nRow, int nCol, Swipe swipeDir)
        {
            if(!m_bRunning)
            {
                m_bRunning = true;

                //코루틴 실행 결과를 전달받을 Returnable 객체를 생성
                //코루틴은 IEnumerator를 리턴할 뿐 코루틴 수행 결과값을 리턴해주지 않는다. 그래서 Returanable 객체를 인자로 전달한다.
                Returnable<bool> bSwipedBlock = new Returnable<bool>(false);
                //Stage 객체의 코루틴 CoDoSwipeAction()을 실행하고, 코루틴이 종료될 때까지 기다린다.
                yield return m_Stage.CoDoSwipeAction(nRow, nCol,swipeDir ,bSwipedBlock);

                m_bRunning = false;
            }

            yield break;
        }

    }
}

