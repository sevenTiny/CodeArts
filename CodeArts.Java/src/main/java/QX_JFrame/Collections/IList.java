package QX_JFrame.Collections;

import java.util.Iterator;

public interface IList<T> extends Iterable {
    void Add(Object elementData);
    void Update(Object value,int index);
    void Remove(int index);
    Object[] get();
    T get(int index);
    Iterator<Object> iterator();
}
