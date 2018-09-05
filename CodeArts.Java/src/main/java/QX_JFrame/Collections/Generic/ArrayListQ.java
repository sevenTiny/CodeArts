/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version: 5.0.0
 * Author: sevenTiny
 * Address: Earth
 * Create: 2017-10-16 17:37:52
 * Update: 2017-10-16 17:37:52
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Collections.Generic;

import QX_JFrame.Collections.IList;

import java.util.Iterator;
import java.util.RandomAccess;

public class ArrayListQ<T> implements IList<T>, RandomAccess, Cloneable, java.io.Serializable, Iterable {

    private transient Object[] elementData;

    private transient Object[] saveElementData;

    /**
     * actual length
     */
    public int Length = 0;

    /**
     * cursor
     */
    private int Index = 0;


    /**
     * Initial length and increase length
     */
    private static final int initAndIncreaseLength = 5;

    /**
     * constructor/Init generator
     */
    public ArrayListQ() {
        this(initAndIncreaseLength);
    }

    public ArrayListQ(int initialSize) {
        elementData = new Object[initialSize];
    }

    public void Add(Object value) {
        //if current size <= total use space
        if (this.Length + 1 <= this.elementData.length) {
            elementData[this.Index] = value;
            this.Length++;
            this.Index++;
        } else {
            ExtendArraySpace();     //if current size > total use space -> extend the space
            this.Add(value);        //recursive execute
        }
    }

    public void Update(Object value, int index) {
        this.RangeCheck(index);
        this.elementData[index] = value;
    }

    public void Remove(int index) {
        this.RangeCheck(index);
        this.elementData[index] = null;
    }

    public Object[] get() {
        return this.TrimArray();
    }

    public T get(int index) {
        this.RangeCheck(index);
        return (T) elementData[index];
    }

    private void ExtendArraySpace() {
        if (this.saveElementData == null)
            saveElementData = new Object[initAndIncreaseLength];
        this.saveElementData = this.elementData;                            //save data first
        this.elementData = new Object[this.elementData.length + initAndIncreaseLength];   //extend elementData
        this.CopyArray(this.saveElementData, this.elementData);              //copy array
    }

    /**
     * Copy sourceArray to toArray
     *
     * @param sourceArray sourceArray
     * @param toArray     toArray
     */
    private void CopyArray(Object[] sourceArray, Object[] toArray) {
        System.arraycopy(sourceArray, 0, toArray, 0, sourceArray.length);
    }

    private Object[] TrimArray() {
        int actualSize = 0;
        /**
         * calculate size
         */
        for (Object anElementData : this.elementData)
            if (anElementData != null)
                actualSize++;

        Object[] newArray = new Object[actualSize];
        for (int i = 0; i < this.Length; i++)
            if (this.elementData[i] != null)
                newArray[i] = this.elementData[i];

        return newArray;
    }

    private void RangeCheck(int index) {
        if (index >= this.Length)
            throw new ArrayIndexOutOfBoundsException("out of range of array");
    }

    @Override
    public Iterator<Object> iterator() {
        class iter implements Iterator<Object>     //方法内部类
        {
            private int cur = 0;

            @Override
            public boolean hasNext() {
                return cur != Length;
            }

            @Override
            public Object next() {
                Object c = elementData[cur];
                cur++;
                return c;
            }

            public void remove() {
                // do nothing
            }
        }
        return new iter();     //安装Iterable接口的约定，返回迭代器
    }
}
