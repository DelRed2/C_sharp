namespace StackCalculator {


    public class MyStack<T> {

        private class StackElement<T1> {

            public readonly StackElement<T1> Prev;
            public readonly T1 Value ;

            public StackElement(T1 vl, StackElement<T1> pr) {
                Value = vl;
                Prev = pr;
            }
        }

        private StackElement<T> last = null;
        private int count = 0;

        public void Push(T val) {  
            var newElem = new StackElement<T>(val, last);
            last = newElem;
            count++;
        }

        public T Pop() {
            var res = last.Value;
            last = last.Prev;
            count--;
            return res;
        }

        public T Peek() {
            return last.Value;
        }

        public int Count {
            get { return count; }
        }

    }

}
