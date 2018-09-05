/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-04 14:29:22
 * Update:2017-09-04 14:29:22
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Bantina.Extends;

import java.util.UUID;

public final class TypeCast {

    public static <T> T ToType(Class<T> clazz,Object value) {
        String typeName = clazz.getTypeName();
        T t = clazz.cast(value);
        return t;
    }

    public static UUID ToUUID(Object value) {
        return UUID.fromString(value.toString());
    }
}
