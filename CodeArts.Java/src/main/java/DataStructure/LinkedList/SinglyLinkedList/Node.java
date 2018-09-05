/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-16 18:09:42
 * Update: 2017-10-16 18:09:42
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package DataStructure.LinkedList.SinglyLinkedList;

public class Node {
    public Object data;
    public Node next;

    public Node() {
        this.data = null;
        this.next = null;
    }

    public Node(Object element) {
        this.data = element;
        this.next = null;
    }

    public Node Add(Object obj) {
        if (this.next == null)
            this.next = new Node(obj);
        else
            this.next.Add(obj);
        return this;
    }

    public void Remove(Node previousNode, Object data) {
        if (previousNode==null||previousNode.next==null)
            return;
        if (previousNode.next.data.equals(data)) {
                previousNode.next = previousNode.next.next;
        }else{
            this.Remove(previousNode.next,data);
        }
    }

    public void TraverseView(Node node){
        while (node != null) {
            System.out.println(node.data);
            node = node.next;
        }
    }

    public void ReverseView(Node node) {
        if (node != null) {
            ReverseView(node.next);
            System.out.println(node.data);
        }
    }
}
