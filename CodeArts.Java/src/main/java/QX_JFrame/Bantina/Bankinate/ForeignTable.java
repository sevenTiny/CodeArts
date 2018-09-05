/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-01 17:30:34
 * Update:2017-09-01 17:30:34
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * Personal WebSit: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Bantina.Bankinate;

import java.lang.annotation.*;

@Documented
@Target({ElementType.FIELD})
@Inherited
@Retention(RetentionPolicy.RUNTIME)
public @interface ForeignTable {
    String TableName() default "";
    String ForeignKeyName() default "";
}
