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
package DataStructure.LinkedList.SinglyLinkedList;

public class LinkedList {
    private Node root;

    public LinkedList() {
    }

    public LinkedList(Object data) {
        this.root = new Node(data);
    }

    public LinkedList Add(Object data) {
        if (this.root==null)
            this.root=new Node(data);
        else
            this.root = this.root.Add(data);

        return this;
    }

    public void Remove(Object data) {
        if (this.root.data.equals(data)) {
            if (root.next == null)
                root = null;
            else
                root = root.next;
        } else {
            root.Remove(this.root, data);
        }
    }

    public void ReverseList() {
        Node preNode = null;
        Node currentNode = root;
        Node nexeNode=null;

        while (currentNode != null) {
            nexeNode=currentNode.next;

            if(nexeNode==null)
                root=currentNode;

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
