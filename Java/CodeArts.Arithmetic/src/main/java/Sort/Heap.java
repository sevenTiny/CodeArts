package Sort;

public class Heap {
    public static void Sort(int[] array) {
        if (array == null || array.length <= 1) {
            return;
        }

        //build maxHeap, the heap root index = 0;
        int half = (array.length-1) / 2;
        for (int i = half; i >= 0; i--) {
            maxHeap(array, array.length, i);
        }

        //sort heap
        for (int i = array.length - 1; i >= 1; i--) {
            int temp = array[0];
            array[0]=array[i];
            array[i]=temp;
            maxHeap(array, i, 0);
        }
    }

    private static void maxHeap(int[] array, int heapSize, int index) {
        int left = index * 2 + 1;
        int right = index * 2 + 2;

        int largest = index;

        /**
         * left < heapSize check if has left child
         */
        if (left < heapSize && array[left] > array[index]) {
            largest = left;
        }

        if (right < heapSize && array[right] > array[largest]) {
            largest = right;
        }

        if (index != largest) {
            int temp = array[index];
            array[index]=array[largest];
            array[largest]=temp;

            maxHeap(array, heapSize, largest);
        }
    }
}
