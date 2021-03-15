/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-19 20:33:07
 * Update: 2017-10-19 20:33:07
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description: Insert Sort
 * Thx , Best Regards ~
 *********************************************************/
package Sort;

public class Insert {

    public static void Sort(int[] array) {
        for (int i = 1; i < array.length; i++) {
            int j = i - 1;  //new array length
            int temp = array[i];
            while (j >= 0 && temp < array[j]) {
                array[j+1]=array[j];
                j--;
            }
            array[j+1]=temp;
        }
    }
}
