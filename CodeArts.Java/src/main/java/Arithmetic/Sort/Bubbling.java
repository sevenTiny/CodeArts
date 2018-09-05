/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-19 18:13:17
 * Update: 2017-10-19 18:13:17
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package Arithmetic.Sort;

public class Bubbling {

    /**
     * Bubbling sort:Time complexity O(n2)
     * let min or max value into boundary like bubbling each sort
     * flag effect:if no sort once,sort complete
     * @param array array
     */

    public static void Sort(int[]array){
        boolean flag=true;
        for (int i = 0; i < array.length&&flag;i++) {
            flag=false;
            for (int j =0; j < array.length-1-i; j++) {
                if (array[j]>array[j+1]){
                    int temp =array[j];
                    array[j]=array[j+1];
                    array[j+1]=temp;
                    flag=true;
                }
            }
        }
    }
}
