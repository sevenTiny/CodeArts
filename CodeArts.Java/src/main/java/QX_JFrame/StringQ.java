/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: qixiao(柒小)
 * Address: TianJin,China
 * Create: 2017-10-12 14:07:41
 * Update: 2017-10-12 14:07:41
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame;

public class StringQ{
    //String Default value
    public static final String Null = null;
    public static final String Empty = "";

    //String Check Null or Empty
    public static boolean IsNullOrEmpty(String value) {
        return value == null || value.equals("");
    }

    //String Check Null or WhiteSpace
    public static boolean IsNullOrWhiteSpace(String value) {
        if (value != null) {
            char[] items = value.toCharArray();
            for (char item : items) {
                if (item == ' ')
                    return false;
            }
        }
        return value == null || value.equals(" ");
    }
}
