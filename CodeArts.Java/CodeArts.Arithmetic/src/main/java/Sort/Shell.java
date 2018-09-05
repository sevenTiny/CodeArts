/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-19 21:55:43
 * Update: 2017-10-19 21:55:43
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package Sort;

public class Shell {
    public static void Sort(int[] array) {
        //希尔排序
        int d = array.length;
        while (true) {
            d = d / 2;
            for (int i = 0; i < d; i++) {
                for (int j = i + d; j < array.length; j += d) {
                    int temp = array[j];
                    int k = j-d ;
                    while (k >= 0 && temp < array[k]) {
                        array[k+d]=array[k];
                        k-=d;
                    }
                    array[k+d]=temp;
                }
            }
            if (d==1)
                break;
        }
    }
}
