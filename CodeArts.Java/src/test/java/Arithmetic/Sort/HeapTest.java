package Arithmetic.Sort;

import Arithmetic.CommonData;
import org.junit.Test;

public class HeapTest {
    @Test
    public void test() {
        Heap.Sort(CommonData.OUT_OF_ORDER_ARRAY);

        for (int a : CommonData.OUT_OF_ORDER_ARRAY) {
            System.out.println(a);
        }
    }
}
