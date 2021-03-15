package LinkedList;

import DataStructure.LinkedList.SinglyLinkedList.LinkedList;
import org.junit.Test;

public class SinglyLinkedListTest {
    @Test
    public void test() {
        LinkedList list = new LinkedList();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);

        list.ReverseList();

        list.TraverseView();
    }
}
