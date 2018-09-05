package DataStructure.Array;

import org.junit.Test;

public class StackTest {
    @Test
    public void test(){
        Stack stack = new Stack();
        stack.PUSH(1);
        stack.PUSH(2);
        stack.PUSH(3);
        stack.PUSH(4);
        stack.PUSH(5);
        stack.PUSH(6);

        int length = stack.length;

        for (int i = 0; i < length; i++) {
            System.out.println(stack.POP());
        }

    }
}
