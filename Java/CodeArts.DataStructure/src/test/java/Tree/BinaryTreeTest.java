package Tree;

import org.junit.Test;

public class BinaryTreeTest {
    @Test
    public void test() {
        BinarySearchTree binarySearchTree=new BinarySearchTree();

        binarySearchTree.Add(8);
        binarySearchTree.Add(4);
        binarySearchTree.Add(10);
        binarySearchTree.Add(3);
        binarySearchTree.Add(6);
        binarySearchTree.Add(5);
        binarySearchTree.Add(7);


        binarySearchTree.PreOrderTraversal();
        System.out.println("PreOrderTraversal");

        binarySearchTree.InOrderTraversal();
        System.out.println("InOrderTraversal");

        binarySearchTree.PostOrderTraversal();
        System.out.println("PostOrderTraversal");
    }
}
