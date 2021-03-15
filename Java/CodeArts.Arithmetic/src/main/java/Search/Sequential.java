package Search;

public class Sequential {
    public static int Search (int[] array,int value){
        for (int i = 0; i < array.length; i++) {
            if (array[i]==value)
                return i;
        }
        return -1;
    }
}
