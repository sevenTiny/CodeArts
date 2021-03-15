package Tree;

public class BinaryTreeNode {
    public int data;
    public BinaryTreeNode left;
    public BinaryTreeNode right;

    public BinaryTreeNode(int value) {
        data = value;
    }

    public void PreOrderTraversal(BinaryTreeNode root) {
        if (root == null)
            return;

        System.out.print(root.data+ " ");

        PreOrderTraversal(root.left);

        PreOrderTraversal(root.right);
    }

    public void InOrderTraversal(BinaryTreeNode root) {
        if (root == null)
            return;

        InOrderTraversal(root.left);

        System.out.print(root.data+ " ");

        InOrderTraversal(root.right);
    }

    public void PostOrderTraversal(BinaryTreeNode root) {
        if (root == null)
            return;

        PostOrderTraversal(root.left);

        PostOrderTraversal(root.right);

        System.out.print(root.data+ " ");
    }
}
