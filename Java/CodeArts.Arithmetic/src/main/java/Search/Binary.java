/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-23 21:35:02
 * Update: 2017-10-23 21:35:02
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package Search;

public class Binary {
    public static int Search(int[] array, int data) {

        int low = 0;
        int high = array.length - 1;

        while (low <= high) {
            int partition = (low + high) / 2;
            if (data < array[partition]) {
                high = partition - 1;
            } else if (data > array[partition]) {
                low = partition + 1;
            } else {
                return partition;
            }
        }
        return -1;
    }
}
