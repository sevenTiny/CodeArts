/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-11-09 10:31:11
 * Update: 2017-11-09 10:31:11
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package Arithmetic.Sort;

public class Quick {
    public static void Sort(int[] array) {
        QuickSort(array, 0, array.length - 1);
    }

    private static void QuickSort(int[] array, int start, int end) {
        if (end > start) {
            int pivotIndex = QuickSortPartition(array, start, end);
            QuickSort(array, start, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, end);
        }
    }

    private static int QuickSortPartition(int[] array, int start, int end) {

        int i = start;
        int j = end;

        while (i < j) {
            while (i < j) {
                if (array[i] > array[j]) {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    break;
                }
                j--;
            }
            while (i < j) {
                if (array[i] > array[j]) {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    break;
                }
                i++;
            }
        }
        return j;
    }
}
