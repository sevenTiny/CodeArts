package DataStructure.Array;

import DataStructure.LinkedList.Queue;
import org.junit.Test;

public class QueueTest {
    @Test
    public void test(){
        Queue queue=new Queue();
        queue.EnQueue(1);
        queue.EnQueue(2);
        queue.EnQueue(3);
        queue.EnQueue(4);
        queue.EnQueue(5);

        for (int i = 0; i <5;i++) {
            System.out.println(queue.DeQueue());
        }
    }
}
