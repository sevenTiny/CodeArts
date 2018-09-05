/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2016-09-02 16:15:57
 * Update:2017-09-01 16:15:57
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * Personal WebSit: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Bantina;

import org.apache.commons.codec.binary.Base64;

import javax.crypto.KeyGenerator;
import javax.crypto.Mac;
import javax.crypto.SecretKey;
import javax.crypto.spec.SecretKeySpec;
import java.math.BigInteger;
import java.security.MessageDigest;

/**
 * Author:qixiao
 * Time:2016年9月2日22:59:03
 */
public final class Encrypt_Util_DG {

    /**
     * Private constructor, limiting the class cannot be instantiated
     */
    private Encrypt_Util_DG() {
        throw new Error("class cannot be instantiation");
    }
    /*使用 sun.misc.BASE64Encoder */

    /**
     * Base64编码
     *
     * @param str 待编码的字符串
     * @return 编码后的字符串
     */
    public static String Encrypt_Base64_sun(String str) {
        return new sun.misc.BASE64Encoder().encode(str.getBytes());
    }

    /**
     * Base64解码
     *
     * @param str 待解码的字符串
     * @return string 解码后的字符串
     */
    public static String Decrypt_Base64_sun(String str)throws Exception {
            return new String(new sun.misc.BASE64Decoder().decodeBuffer(str));
    }
    /*使用commons-codec.jar*/

    /**
     * Base64编码
     *
     * @param str 待编码的字符串
     * @return 编码后的字符串
     */
    public static String Encrypt_Base64_commons(String str) {
        return new String(Base64.encodeBase64(str.getBytes()));
    }

    /**
     * Base64解码
     *
     * @param str 待解码的字符串
     * @return string 解码后的字符串
     */
    public static String Decrypt_Base64_commons(String str) {
        return new String(Base64.decodeBase64(str.getBytes()));
    }

    
    /*下面三种加密算法是单向加密，即不可进行逆向解密的算法，在实际应用中常常会以此为基础进行扩展加密方式，来防止查库等暴力破解*/
    /**
     * MD5 杂凑算法 Message Digest algorithm 5 信息摘要算法 32 位
     *
     * @param str
     * @param bit 生成的位数
     * @return
     */
    public static String Encrypt_MD5(String str, int bit) throws Exception {
        //根据MD5算法生成MessageDigest对象
        MessageDigest MD5 = MessageDigest.getInstance("MD5");
        //使用bytes更新摘要
        MD5.update(str.getBytes());
        //通过执行诸如填充之类的最终操作完成哈希计算。
        byte b[] = MD5.digest();
        //生成具体的md5密码到buf数组
        int i;
        StringBuilder builder = new StringBuilder("");
        for (int offset = 0; offset < b.length; offset++) {
            i = b[offset];
            if (i < 0)
                i += 256;
            if (i < 16)
                builder.append("0");
            builder.append(Integer.toHexString(i));
        }
        switch (bit)
        {
            case 32:
                return builder.toString();
            case 16:
                return builder.toString().substring(8, 24);//MD5 16 位 是从MD5 32 位的结果中截取出来的一部分
            default:
                return builder.toString();
        }
    }

    /**
     * SHA 加密技术 Secure Hash Algorithm 安全散列算法
     * SHA 是一种数据加密算法，该算法经过加密专家多年来的发展和改进已日益完善，现在已成为公认的最安全的散列算法之一，并被广泛使用。该算法的思想是接收一段明文，然后以一种不可逆的方式将它转换成一段（通常更小）密文，也可以简单的理解为取一串输入码（称为预映射或信息），并把它们转化为长度较短、位数固定的输出序列即散列值（也称为信息摘要或信息认证代码）的过程。散列函数值可以说时对明文的一种“指纹”或是“摘要”所以对散列值的数字签名就可以视为对此明文的数字签名。
     *
     * @param str
     * @return
     */
    public static String Encrypt_SHA(String str)throws Exception {
        MessageDigest messageDigest = MessageDigest.getInstance("SHA");
        messageDigest.update(str.getBytes());
        return new BigInteger(messageDigest.digest()).toString(32);
    }

    /**
     * Hash Message Authentication Code，散列消息鉴别码
     *
     * @param str
     * @param key 用initMacKey方法构建一个秘钥进行加密
     * @return
     * @throws Exception
     */
    public static String Encrypt_HMAC(String str, String key) throws Exception {
        /* MAC算法可选以下多种算法 * HmacMD5 * HmacSHA1 * HmacSHA256 * HmacSHA384 * HmacSHA512 */
        SecretKey secretKey = new SecretKeySpec(Decrypt_Base64_commons(str).getBytes(), "HmacMD5");
        Mac mac = Mac.getInstance(secretKey.getAlgorithm());
        mac.init(secretKey);
        return new BigInteger(mac.doFinal(str.getBytes())).toString();
    }

    /**
     * 初始化HMAC密钥,构建一个秘钥
     *
     * @return
     * @throws Exception
     */
    public static String InitMacKey() throws Exception {
        KeyGenerator keyGenerator = KeyGenerator.getInstance("HmacMD5");
        SecretKey secretKey = keyGenerator.generateKey();
        return Encrypt_Base64_commons(secretKey.getEncoded().toString());
    }
}
