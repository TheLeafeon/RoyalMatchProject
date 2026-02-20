using UnityEngine;
using System.Collections;

namespace RoyalMatch.Util
{
    //지정된 시간동안 지정된 위치로 GameObject를 이동시키는 MoveTo 애니메이션을 수행
    public static class Action2D
    {
        //target:이동시킬 대상, to: 이동할 목표 위치, duration : 이동 시간, bSeflRmove : 애니메이션 종료후 삭제 여부 플래그
        public static IEnumerator MoveTo(Transform target, Vector3 to, float duration, bool bSelfRemove = false)
        {
            //시작위치 저장
            Vector2 startPos =target.transform.position;

            float elapsed = 0.0f;
            //주어진 이동시간이 얼마나 남았는지 체크
            while (elapsed < duration)
            {
                elapsed += Time.smoothDeltaTime;
                /*
                 * 선형 보간법을 사용해서  GameObject의 좌표(transform.position)을 이동시킨다
                 * 경과시간을 전체 이동시간으로 나누어서 시간의 변화량을 0~ 1.0 으로 계산한다.
                 */
                target.transform.position = Vector2.Lerp(startPos, to, elapsed / duration);

                yield return null;
            }

            target.transform.position = to;

            if(bSelfRemove )
                Object.Destroy(target.gameObject,0.1f);

            yield break;
        }
    }
}

