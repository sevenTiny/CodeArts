package Arithmetic.Sort;

import Arithmetic.CommonData;
import org.junit.Test;

public class MergingTest {
    @Test
    public void test() {
        Merging.Sort(CommonData.OUT_OF_ORDER_ARRAY);

        for (int a : CommonData.OUT_OF_ORDER_ARRAY) {
            System.out.println(a);
        }
    }
}
