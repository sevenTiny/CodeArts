/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2016-09-23 15:53:14
 * Update:2017-09-01 15:53:14
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * Personal WebSit: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Bantina;

import com.alibaba.fastjson.JSON;
import com.google.gson.GsonBuilder;
import org.jetbrains.annotations.Contract;
import org.jetbrains.annotations.NotNull;

/**
 * Created by qixiao on 2016/9/23.
 */
public final class Json_Util_DG {
    /**
     * Private constructor, limiting the class cannot be instantiated
     */
    private Json_Util_DG() {
        throw new Error("class cannot be instantiation");
    }

    /**
     * QiXiao Info
     * there is two method that can make Object transfer to Json and each method is different
     * 1:fastjson the serialized json is use LOWERSTR like {'id':1}
     * 2:Gson the serialized json is use Uppercase string   like {'Id':1}
     */

    /**
     * use fastjson serialize the T
     * the method depend on the jar package
     * <dependency>
     * <groupId>com.alibaba</groupId>
     * <artifactId>fastjson</artifactId>
     * <version>1.2.12</version>
     * </dependency>
     *
     * @param t
     * @param <T>
     * @return
     */
    @NotNull
    public static <T> String Serialize_fastjson(T t) {
        return JSON.toJSONString(t);
    }

    /**
     * deserialization the json string
     *
     * @param clazz
     * @param json
     * @param <T>
     * @return
     */
    @Contract("_, null -> null")
    public static <T> T DeSerialize_fastjson(Class<T> clazz, String json) {
        return JSON.parseObject(json, clazz);
    }

    /**
     * use Gson serialize the T
     * the method depend on the jar package
     * <dependency>
     * <groupId>com.google.code.gson</groupId>
     * <artifactId>gson</artifactId>
     * <version>2.6.2</version>
     * </dependency>
     *
     * @param t
     * @param <T>
     * @return
     */
    public static <T> String Serialize_Gson(T t) {
        return new GsonBuilder().create().toJson(t);
    }

    /**
     * deserialize use Gson
     *
     * @param clazz
     * @param json
     * @param <T>
     * @return
     */
    public static <T> T DeSerialize_Gson(Class<T> clazz, String json) {
        return new GsonBuilder().create().fromJson(json, clazz);    //init T and transfer json to T
    }
}
