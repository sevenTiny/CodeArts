/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-03 10:41:17
 * Update:2017-09-03 10:41:17
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * Personal WebSit: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Bantina.Extends;

/**
 * value check
 */
public class ValueCheck {
    /**
     * check string is null of empty
     * @param value
     * @return
     */
    public static boolean NullOrEmpty(String value) {
        return (value == null || value.length() <= 0);
    }

    /**
     * check object is null or empty
     * @param value
     * @return
     */
    public static boolean NullOrEmpty(Object value)
    {
        return (value==null);
    }
}
