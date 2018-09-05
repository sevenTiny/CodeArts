/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-17 16:58:17
 * Update: 2017-10-17 16:58:17
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package DataStructure.Array;

public class Stack {

    private Object[] elementData;
    public int length = 0;
    private int space = 0;
    private int index = 0;

    private static final int DEFAULT_LENGTH = 5;

    /**
     * Initial method
     */
    public Stack() {
        elementData = new Object[DEFAULT_LENGTH];
        space=DEFAULT_LENGTH;
    }

    public Stack(int length) {
        elementData = new Object[length];
        space=length;
    }

    public Stack PUSH(Object obj) {
        if(index>=space)
            ExtendStack();
        elementData[index]=obj;
        index++;
        length++;
        return this;
    }

    public Object POP() {
        if (index>0){
            index--;
            length--;
            return elementData[index];
        }else{
            return null;
        }
    }

    private  void ExtendStack(){
        space+=DEFAULT_LENGTH;
        Object[] newObject = new Object[space];
        for (int i = 0; i < elementData.length; i++) {
            newObject[i]=elementData[i];
        }
        elementData=newObject;
    }
}
