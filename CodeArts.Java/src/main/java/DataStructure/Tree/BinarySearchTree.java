/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-18 17:34:07
 * Update: 2017-10-18 17:34:07
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package DataStructure.Tree;

public class BinarySearchTree {
    private BinaryTreeNode root;

    /**
     * BinarySearch Tree Add method
     *
     * @param value
     */
    public void Add(int value) {
        if (root == null)
            root = new BinaryTreeNode(value);
        else {
            BinaryTreeNode parentNode = null;
            BinaryTreeNode currentNode = root;
            while (currentNode != null) {
                if (value < currentNode.data) {
                    parentNode = currentNode;
                    currentNode = currentNode.left;
                } else if (value > currentNode.data) {
                    parentNode = currentNode;
                    currentNode = currentNode.right;
                } else {
                    return;
                }
            }
            // insert
            if (value < parentNode.data)
                parentNode.left = new BinaryTreeNode(value);
            else if (value > parentNode.data)
                parentNode.right = new BinaryTreeNode(value);
        }
    }

    public void Add(BinaryTreeNode node) {
        if (node == null)
            return;
        if (root == null)
            root = new BinaryTreeNode(node.data);
        else {
            BinaryTreeNode parentNode = null;
            BinaryTreeNode currentNode = root;
            while (currentNode != null) {
                if (node.data < currentNode.data) {
                    parentNode = currentNode;
                    currentNode = currentNode.left;
                } else if (node.data > currentNode.data) {
                    parentNode = currentNode;
                    currentNode = currentNode.right;
                } else {
                    return;
                }
            }
            // insert
            if (node.data < parentNode.data)
                parentNode.left = node;
            else if (node.data > parentNode.data)
                parentNode.right = node;
        }
    }

    public void Remove(int value) {
        if (root == null){
            return;
        }
        else {
            BinaryTreeNode parentNode = null;
            BinaryTreeNode currentNode = root;
            while (currentNode != null) {
                if (value < currentNode.data) {
                    parentNode = currentNode;
                    currentNode = currentNode.left;
                } else if (value > currentNode.data) {
                    parentNode = currentNode;
                    currentNode = currentNode.right;
                } else {
                    //match
                    if (parentNode == null)
                        parentNode = root;//necessary ?
                    break;
                }
            }

            //method-1:add renew
//            if (currentNode != null) {
//                BinaryTreeNode saveNode;
//                if (value < parentNode.data) {
//                    saveNode = parentNode.left;
//                    parentNode.left = null;
//                } else {
//                    saveNode = parentNode.right;
//                    parentNode.right = null;
//                }
//                Add(saveNode.left);
//                Add(saveNode.right);
//            }

            //method-2:update tree/performance better than method-1
            if (currentNode!=null){
                if (value<parentNode.data){
                    //match left
                    if (currentNode.left==null&&currentNode.right==null){
                        parentNode.left=null;
                    }else if (currentNode.left==null){
                        parentNode.left=currentNode.right;
                    }else if(currentNode.right==null){
                        parentNode.left=currentNode.left;
                    }else{
                        //find min value right or max value left in the tree
                        BinaryTreeNode rightMin=currentNode.right;
                        while (rightMin.left!=null){
                            rightMin=rightMin.left;
                        }
                        //delete minNode
                        Remove(rightMin.data);
                        //let current data = rightMin data
                        currentNode.data=rightMin.data;
                    }
                }else{
                    //match right
                    if (currentNode.left==null&&currentNode.right==null){
                        parentNode.right=null;
                    }else if (currentNode.left==null){
                        parentNode.right=currentNode.right;
                    }else if(currentNode.right==null){
                        parentNode.right=currentNode.left;
                    }else{
                        //find min value right or max value left in the tree
                        BinaryTreeNode rightMin=currentNode.right;
                        while (rightMin.left!=null){
                            rightMin=rightMin.left;
                        }
                        //delete minNode
                        Remove(rightMin.data);
                        //let current data = rightMin data
                        currentNode.data=rightMin.data;
                    }
                }
            }
        }
    }

    public boolean Find(int value) {
        return FindValue(root, value) == null;
    }

    private BinaryTreeNode FindValue(BinaryTreeNode node, int value) {
        if (node.data == value)
            return node;
        else if (node.data < value)
            return FindValue(node.right, value);
        else if (node.data > value)
            return FindValue(node.left, value);
        return node;
    }


    public void PreOrderTraversal() {
        root.PreOrderTraversal(root);
    }

    public void InOrderTraversal() {
        root.InOrderTraversal(root);
    }

    public void PostOrderTraversal() {
        root.PostOrderTraversal(root);
    }


}
