using UnityEngine;

namespace RoyalMatch.Util
{
    //코루틴의 결과를 수신하기 위한 범용 클래스
    public class Returnable<T>
    {
        public T value { get; set; }

        public Returnable(T value)
        {
            this.value = value;
        }
    }
}

