/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-20 11:06:34
 * Update: 2017-10-20 11:06:34
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package Arithmetic.Sort;

public class Merging {
    public static void Sort(int[] array) {
        if (array.length > 1) {
            int length1 = array.length / 2;
            int[] array1 = new int[length1];
            System.arraycopy(array, 0, array1, 0, length1);
            Sort(array1);

            int length2 = array.length - length1;
            int[] array2 = new int[length2];
            System.arraycopy(array, length1, array2, 0, length2);
            Sort(array2);

            int[] result = Merge(array1, array2);
            System.arraycopy(result, 0, array, 0, array.length);
        }
    }

    private static int[] Merge(int[] array1, int[] array2) {
        int length = array1.length + array2.length;
        int[] result = new int[length];

        int index1 = 0;
        int index2 = 0;
        int index3 = 0;

        while (index1 < array1.length && index2 < array2.length) {
            if (array1[index1] < array2[index2]) {
                result[index3++] = array1[index1++];
            } else {
                result[index3++] = array2[index2++];
            }
        }
        while (index1 < array1.length) {
            result[index3++] = array1[index1++];
        }
        while (index2 < array2.length) {
            result[index3++] = array2[index2++];
        }
        return result;
    }
}
