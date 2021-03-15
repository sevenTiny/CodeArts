/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-16 20:42:25
 * Update: 2017-10-16 20:42:25
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package LinkedList.DoublyLinkedList;

public class LinkedList {
    private Node root;

    public LinkedList() {
    }

    public LinkedList(Object data) {
        this.root = new Node(data);
    }

    public LinkedList Add(Object data) {
        if (root == null)
            root = new Node(data);
        else
            this.root = this.root.Add(data);
        return this;
    }

    public void AddLast(Object object) {
       this.Add(object);
    }

    public Object RemoveFirst() {
        Node resultNode=root;
        if (root==null){
            return null;
        }else{
            root=root.next;
        }
        return resultNode.data;
    }

    public void Remove(Object data) {
        if (this.root.data.equals(data)) {
            root = root.next;
        } else {
            root.Remove(data);
        }
    }

    public void ReverseList() {
        Node preNode = null;
        Node currentNode = root;
        Node nexeNode = null;

        while (currentNode != null) {
            nexeNode = currentNode.next;

            if (nexeNode == null)
                root = currentNode;

            currentNode.previous = nexeNode;
            currentNode.next = preNode;
            preNode = currentNode;
            currentNode = nexeNode;
        }
    }

    public void TraverseView() {
        this.root.TraverseView(this.root);
    }

    public void ReverseView() {
        this.root.ReverseView(this.root);
    }
}
