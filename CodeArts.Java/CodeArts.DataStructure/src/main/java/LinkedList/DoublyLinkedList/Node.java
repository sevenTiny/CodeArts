package LinkedList.DoublyLinkedList;

public class Node {
    public Object data;
    public Node previous;
    public Node next;

    public Node(Object element) {
        this.data = element;
        this.previous = null;
        this.next = null;
    }

    public Node Add(Object obj) {
        if (this.next == null){
            Node newNode=new Node(obj);
            this.next = newNode;
            newNode.previous=this;
        }
        else{
            this.next.Add(obj);
        }
        return this;
    }

    public void Remove(Object data) {
        if (this.data.equals(data)) {
            if (previous != null && next != null) {
                previous.next=next;
                next.previous=previous;
            } else if (previous != null) {
                previous.next=null;
            } else if (next != null) {
                next.previous=null;
            }
        }else{
            this.next.Remove(data);
        }
    }

    public void TraverseView(Node node) {
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
