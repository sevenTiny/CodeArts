package LinkedList;

import DataStructure.LinkedList.DoublyLinkedList.LinkedList;

public class Queue {
    private LinkedList linkedList;
    public Queue(){
        this.linkedList=new LinkedList();
    }
    public Queue(Object object){
        this.linkedList=new LinkedList(object);
    }

    public void EnQueue(Object object){
        this.linkedList.AddLast(object);
    }

    public Object DeQueue(){
        return this.linkedList.RemoveFirst();
    }
}
