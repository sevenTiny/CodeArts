package QX_JFrame.Bantina;

import org.apache.commons.lang.RandomStringUtils;

import java.util.Random;

/**
 * Created by Administrator on 2016/9/23.
 */
public final class String_Util_DG {
    /**
     * Private constructor, limiting the class cannot be instantiated
     */
    private String_Util_DG() {
        throw new Error("class cannot be instantiation");
    }

    /**
     * create a random string
     *
     * @param length
     * @return
     */
    public static String RandomString1(int length) {
        String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        StringBuffer sb = new StringBuffer();
        for (int i = 0; i < length; i++) {
            int number = random.nextInt(62);
            sb.append(str.charAt(number));
        }
        return sb.toString();
    }

    public static String RandomString2(int length) {
        return RandomStringUtils.randomAlphanumeric(length);
    }

    //大小写转换
}
